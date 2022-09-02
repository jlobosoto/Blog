using Blog.Application.TodoLists.Queries.ExportTodos;

namespace Blog.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
