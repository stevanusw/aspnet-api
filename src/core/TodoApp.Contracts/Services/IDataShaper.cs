using System.Dynamic;
using TodoApp.Models.Dtos;

namespace TodoApp.Contracts.Services;

// Codemaze
public interface IDataShaper<T>
{
	IEnumerable<ShapedDto> Shape(IEnumerable<T> entities, string? fieldsString);
	ShapedDto Shape(T entity, string? fieldsString);
}
