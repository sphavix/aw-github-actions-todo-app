using Microsoft.AspNetCore.Mvc;
using todo_mvc_application.Models;
using todo_mvc_application.Repositories;

namespace todo_mvc_application.Controllers
{
    public class TodosController : Controller
    {
        private readonly TodoItemRepository _repository = new TodoItemRepository();

        public IActionResult Index()
        {
            var items = _repository.GetAll();
            return View(items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TodoItem item)
        {
            if (ModelState.IsValid)
            {
                _repository.AddTodoITem(item);
                return RedirectToAction(nameof(Index));
            }

            return View(item);
        }

        public IActionResult Edit(int id)
        {
            var item = _repository.GetAll().FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost]
        public IActionResult Edit(TodoItem item)
        {
            if (ModelState.IsValid)
            {
                _repository.UpdateTodoItem(item);
                return RedirectToAction(nameof(Index));
            }

            return View(item);
        }

        public IActionResult Delete(int id)
        {
        var item = _repository.GetAll().FirstOrDefault(x => x.Id == id);
        if (item == null)
            return NotFound();

        return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _repository.DeleteTodoItem(id);
            return RedirectToAction(nameof(Index));
        }
    }
}