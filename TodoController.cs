using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using TodoApp.Models;
using System.Linq;

namespace TodoApp.Controllers
{
    public class TodoController : Controller
    {
        private readonly AppDbContext _context;

        public TodoController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View(_context.Todos.ToList());

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Todo todo)
        {
            if (ModelState.IsValid)
            {
                _context.Todos.Add(todo);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(todo);
        }


        public IActionResult Edit(int id)
        {
            var todo = _context.Todos.Find(id);
            return View(todo);
        }

        [HttpPost]
        public IActionResult Edit(Todo todo)
        {
            _context.Todos.Update(todo);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var todo = _context.Todos.Find(id);
            return View(todo);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var todo = _context.Todos.Find(id);
            _context.Todos.Remove(todo);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
