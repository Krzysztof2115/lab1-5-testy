using lab5_testy.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab5_testy.Controllers
{
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskManager _taskManager;

        public TaskController(TaskManager taskManager)
        {
            _taskManager = taskManager;
        }
        [HttpGet]
        [Route("all")]
        public IActionResult GetAllTasks()
        {
            var tasks = _taskManager.GetTasks();
            return Ok(tasks);
        }
        [HttpGet("{id}")]
        public IActionResult GetTaskById([FromRoute] int id)
        {
            var task = _taskManager.GetTaskById(id);
            return Ok(task);
        }
        [HttpPost]
        [Route("created")]
        public IActionResult CreateTask([FromBody] Models.Task task)
        {
            _taskManager.AddTask(task);
            return Created($"api/task/{task.Id}", null);
        }

    }
}
