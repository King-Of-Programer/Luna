using Luna_Edge.Model;
using Luna_Edge.Repositiries;

namespace Luna_Edge.Services.Task
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<UserTask> CreateTask(UserTask task, Guid userId)
        {
            task.UserId = userId;
            await _taskRepository.AddAsync(task);
            return task;
        }

        public async Task<IEnumerable<UserTask>> GetTasksByUserId(Guid userId)
        {
            return await _taskRepository.GetAllByUserIdAsync(userId);
        }

        public async Task<UserTask> GetTaskById(Guid id, Guid userId)
        {
            var task = await _taskRepository.GetByIdAsync(id);

            if (task == null || task.UserId != userId)
            {
                throw new Exception("Task not found or access denied.");
            }

            return task;
        }

        public async System.Threading.Tasks.Task UpdateTask(UserTask updatedTask)
        {
            await _taskRepository.UpdateAsync(updatedTask);
        }

        public async System.Threading.Tasks.Task DeleteTask(Guid id, Guid userId)
        {
            var task = await GetTaskById(id, userId);

            if (task != null)
            {
                await _taskRepository.DeleteAsync(id);
            }
        }
    }
}