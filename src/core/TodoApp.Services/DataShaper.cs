using System.Dynamic;
using System.Reflection;
using TodoApp.Contracts.Services;
using TodoApp.Models.Dtos;

namespace TodoApp.Services;

// Codemaze
public class DataShaper<T> : IDataShaper<T> where T : class
{
	public PropertyInfo[] Properties { get; set; }

	public DataShaper()
	{
		Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
	}

	public IEnumerable<ShapedDto> Shape(IEnumerable<T> entities, string? fieldsString)
	{
		var requiredProperties = GetRequiredProperties(fieldsString);

		return FetchData(entities, requiredProperties);
	}

	public ShapedDto Shape(T entity, string? fieldsString)
	{
		var requiredProperties = GetRequiredProperties(fieldsString);

		return FetchDataForEntity(entity, requiredProperties);
	}

	private IEnumerable<PropertyInfo> GetRequiredProperties(string? fieldsString)
	{
		var requiredProperties = new List<PropertyInfo>();

		if (!string.IsNullOrWhiteSpace(fieldsString))
		{
			var fields = fieldsString.Split(',', StringSplitOptions.RemoveEmptyEntries);

			foreach (var field in fields)
			{
				var property = Properties
					.FirstOrDefault(pi => pi.Name.Equals(field.Trim(), StringComparison.InvariantCultureIgnoreCase));

				if (property == null)
					continue;

				requiredProperties.Add(property);
			}
		}
		else
		{
			requiredProperties = Properties.ToList();
		}

		return requiredProperties;
	}

	private IEnumerable<ShapedDto> FetchData(IEnumerable<T> entities, IEnumerable<PropertyInfo> requiredProperties)
	{
		var shapedData = new List<ShapedDto>();

		foreach (var entity in entities)
		{
			var shapedObject = FetchDataForEntity(entity, requiredProperties);
			shapedData.Add(shapedObject);
		}

		return shapedData;
	}

	private ShapedDto FetchDataForEntity(T entity, IEnumerable<PropertyInfo> requiredProperties)
	{
		var shapedObject = new ExpandoObject();

		foreach (var property in requiredProperties)
		{
			var objectPropertyValue = property.GetValue(entity);
			shapedObject.TryAdd(property.Name, objectPropertyValue);
		}

		var objectProperty = entity.GetType().GetProperty("Id");
		var id = (int)objectProperty!.GetValue(entity)!;

		var shapedDto = new ShapedDto(id, shapedObject);

		return shapedDto;
	}
}
