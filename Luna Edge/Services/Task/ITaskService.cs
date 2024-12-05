using Luna_Edge.Model;
using System.Threading.Tasks;
namespace Luna_Edge.Services.Task
{
    public interface ITaskService
    {
        Task<UserTask> CreateTask(UserTask task, Guid userId);
        Task<IEnumerable<UserTask>> GetTasksByUserId(Guid userId);
        Task<UserTask> GetTaskById(Guid id, Guid userId);
        System.Threading.Tasks.Task UpdateTask(UserTask updatedTask);
        System.Threading.Tasks.Task DeleteTask(Guid id, Guid userId);
    }
}
