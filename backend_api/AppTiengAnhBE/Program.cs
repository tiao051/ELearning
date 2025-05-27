using AppTiengAnhBE.Repositories.CategoriesRepo;
using AppTiengAnhBE.Repositories.LessonsRepo;
using AppTiengAnhBE.Repositories.UserRepo;
using AppTiengAnhBE.Services.CategoriesServices;
using AppTiengAnhBE.Services.LessonServices;
using AppTiengAnhBE.Services.UserServices;
using AppTiengAnhBE.Services.RemindersServices;
using System.Data;
using Npgsql;
using AppTiengAnhBE.Repositories.RemindersRepo;
using System.Text.Json;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IDbConnection>(sp =>
    new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IReminderRepository, ReminderRepository>();
builder.Services.AddScoped<IReminderService, ReminderService>();

// Thêm đoạn này để cấu hình Authentication với Google
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie() // Sử dụng cookie để lưu session user đăng nhập
.AddGoogle(googleOptions =>
{
    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];

    // Nếu bạn cần customize redirect URI (mặc định là /signin-google)
    // googleOptions.CallbackPath = "/signin-google";
});

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Bắt buộc phải có
app.UseAuthorization();

app.MapControllers();

// Thêm route test login Google (tùy chọn)
app.MapGet("/login-google", async (HttpContext context) =>
{
    await context.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties
    {
        RedirectUri = "/google-response"
    });
});

// Route nhận callback Google sau khi login
app.MapGet("/google-response", async (HttpContext context) =>
{
    var user = context.User;

    if (user?.Identity?.IsAuthenticated ?? false)
    {
        var email = user.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
        var name = user.FindFirst(c => c.Type == ClaimTypes.Name)?.Value;

        return Results.Ok(new { email, name });
    }

    return Results.Unauthorized();
});

app.Run();
