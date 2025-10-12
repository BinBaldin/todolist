using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using todolist.Data;
using todolist.EfCore;
using todolist.models;

namespace todolist.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Todocontroller : ControllerBase
    {
        private readonly TodoDbContext dbContext;

        public Todocontroller(TodoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult getTaskByID(int id)
        {
            var task = dbContext.Assignments.Find(id);
            if (task is null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpGet]
        public IActionResult getAllTasks()
        {
            return Ok(dbContext.Assignments.ToList());
        }
        [HttpPost]
        public IActionResult AddTasks(AddTaskTDb newTaskDB)
        {
            var newTask = new Assignment()
            {
                Title = newTaskDB.Title,
                isDone = newTaskDB.isDone
            };

            dbContext.Assignments.Add(newTask);
            dbContext.SaveChanges();
            return Ok(newTask);
        }
        [HttpPut]
        public IActionResult UpdateTasks(int id)
        {
            var task = dbContext.Assignments.Find(id);
            if (task is null)
            {
                return NotFound();
            }
            task.isDone = true;
            dbContext.SaveChanges();
            return Ok(task);
        }
        [HttpDelete]
        public IActionResult DeleteTasks([FromQuery] string title)
        {
            var tasks = dbContext.Assignments
                                 .Where(a => a.Title.Contains(title))
                                 .ToList();

            if (!tasks.Any())
            {
                return NotFound();
            }

            dbContext.Assignments.RemoveRange(tasks);
            dbContext.SaveChanges();
            return Ok(new { deletedCount = tasks.Count });
        }
        [HttpGet]
        [Route("search")]
        public IActionResult GetTasksByTitle([FromQuery] string title)
        {
            var tasks = dbContext.Assignments
                                 .Where(a => a.Title.Contains(title))
                                 .ToList();

            if (!tasks.Any())
                return NotFound();

            return Ok(tasks);
        }

    }
}
