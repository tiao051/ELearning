namespace AppTiengAnhBE.Models.SystemModel
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string full_name { get; set; }
        public int role_id { get; set; }
        public DateTime created_at { get; set; }
    }
}
