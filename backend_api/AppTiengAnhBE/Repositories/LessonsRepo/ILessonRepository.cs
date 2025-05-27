using AppTiengAnhBE.Models;

namespace AppTiengAnhBE.Repositories.LessonsRepo
{
    public interface ILessonRepository
    {
        Task<IEnumerable<Lesson>> GetAllLessonsAsync();
        Task<Lesson> GetLessonByIdAsync(int id);
        Task<int> CreateLessonAsync(Lesson lesson);
        Task<int> UpdateLessonAsync(Lesson lesson);
        Task<int> DeleteLessonAsync(int id);
    }
}
