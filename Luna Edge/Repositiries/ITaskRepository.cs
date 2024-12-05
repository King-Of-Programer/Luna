using Luna_Edge.Model;

namespace Luna_Edge.Repositiries
{
    public interface ITaskRepository
    {
        Task<UserTask> GetByIdAsync(Guid id); 
        Task<IEnumerable<UserTask>> GetAllByUserIdAsync(Guid userId);
        Task AddAsync(UserTask task); 
        Task UpdateAsync(UserTask task); 
        Task DeleteAsync(Guid id);
    }
}
