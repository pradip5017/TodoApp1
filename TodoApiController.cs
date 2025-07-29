using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using TodoApp.Models;
using System.Linq;

namespace TodoApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoApiController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TodoApiController(AppDbContext context)
        {
            _context = context;
        }

      [HttpGet]
public IActionResult GetAll()
{
    var todos = _context.Todos.ToList();
    return Ok(todos);
}


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var todo = _context.Todos.Find(id);
            if (todo == null) return NotFound();
            return Ok(todo);
        }

       [HttpPost]
public IActionResult Create([FromBody] Todo todo)
{
    
    todo.Id = 0;

    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    _context.Todos.Add(todo);
    _context.SaveChanges();

    return CreatedAtAction(nameof(Get), new { id = todo.Id }, todo);
}


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Todo todo)
        {
            var existing = _context.Todos.Find(id);
            if (existing == null) return NotFound();

            existing.Title = todo.Title;
            existing.Description = todo.Description;
            existing.IsCompleted = todo.IsCompleted;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var todo = _context.Todos.Find(id);
            if (todo == null) return NotFound();

            _context.Todos.Remove(todo);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
