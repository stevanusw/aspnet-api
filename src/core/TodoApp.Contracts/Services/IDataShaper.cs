using System.Dynamic;

namespace TodoApp.Contracts.Services;

// Codemaze
public interface IDataShaper<T>
{
	IEnumerable<ExpandoObject> Shape(IEnumerable<T> entities, string? fieldsString);
	ExpandoObject Shape(T entity, string? fieldsString);
}
