using Newtonsoft.Json;
using todo_mvc_application.Models;

namespace todo_mvc_application.Repositories
{
    public class TodoItemRepository
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "todoitems.json");

        public List<TodoItem> GetAll()
        {
            if (!File.Exists(_filePath))
            {
                return new List<TodoItem>();
            }

            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<TodoItem>>(json)!;
        }

        public void SaveTodoItems(List<TodoItem> todoItems)
        {
            var json = JsonConvert.SerializeObject(todoItems, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }

        public void AddTodoITem(TodoItem todoItem)
        {
            var items = GetAll();
            todoItem.Id = items.Any() ? items.Max(i => i.Id) + 1 : 1;
            items.Add(todoItem);
            SaveTodoItems(items);
        }

        public void UpdateTodoItem(TodoItem todoItem)
        {
            var items = GetAll();
            var existingItem = items.FindIndex(i => i.Id == todoItem.Id);
            if (existingItem != -1)
            {
                items[existingItem] = todoItem;
                SaveTodoItems(items);
            }
        }

        public void DeleteTodoItem(int id)
        {
            var items = GetAll();
            var itemToRemove = items.FirstOrDefault(i => i.Id == id);
            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);
                SaveTodoItems(items);
            }
        }
    }
}