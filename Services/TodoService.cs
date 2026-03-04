using TodoListApi.Models;

namespace TodoListApi.Services;

public class TodoService
{
    private readonly List<TodoItem> _items = new();
    private int _nextId = 1;
    public List<TodoItem> GetAll() => _items;
    public TodoItem? GetById(int id) => _items.FirstOrDefault(x => x.Id == id);

    public TodoItem Create(string title)
    {
        var item = new TodoItem(Id = _nextId++, Title = title, IsCompleted = false);
        _items.Add(item);
        return item;
    }

    public bool Update(int id, string title, bool isCompleted)
    {
        var item = GetById(id);
        if (item is null)
            return false;
        
        item.Title = title;
        item.IsCompleted = isCompleted;
        return true;
    }

    public bool Delete(int id)
    {
        var item = GetById(id);
        return item != null && _items.Remove(item);
    }
}