using Luna_Edge.Model;
using Luna_Edge.Services.Task;
using Luna_Edge.Services.UserService.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Luna_Edge.Controllers
{

    [ApiController]
    [Route("/tasks")]
    [Authorize]
    public class TaskController : Controller
    {

        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserTask task)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value); 
            task.UserId = userId; 

            var createdTask = await _taskService.CreateTask(task, userId);
            return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value); 

            try
            {
                var task = await _taskService.GetTaskById(id, userId);
                return Ok(task);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserTask updatedTask)
        {
            updatedTask.Id = id; 

            try
            {
                await _taskService.UpdateTask(updatedTask);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value); 
            try
            {
                await _taskService.DeleteTask(id, userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var tasks = await _taskService.GetTasksByUserId(userId);

            return Ok(tasks);
        }
    }
}
