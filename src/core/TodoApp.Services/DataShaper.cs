using System.Dynamic;
using System.Reflection;
using TodoApp.Contracts.Services;

namespace TodoApp.Services;

// Codemaze
public class DataShaper<T> : IDataShaper<T> where T : class
{
	public PropertyInfo[] Properties { get; set; }

	public DataShaper()
	{
		Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
	}

	public IEnumerable<ExpandoObject> Shape(IEnumerable<T> entities, string? fieldsString)
	{
		var requiredProperties = GetRequiredProperties(fieldsString);

		return FetchData(entities, requiredProperties);
	}

	public ExpandoObject Shape(T ExpandoObject, string? fieldsString)
	{
		var requiredProperties = GetRequiredProperties(fieldsString);

		return FetchDataForExpandoObject(ExpandoObject, requiredProperties);
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

	private IEnumerable<ExpandoObject> FetchData(IEnumerable<T> entities, IEnumerable<PropertyInfo> requiredProperties)
	{
		var shapedData = new List<ExpandoObject>();

		foreach (var ExpandoObject in entities)
		{
			var shapedObject = FetchDataForExpandoObject(ExpandoObject, requiredProperties);
			shapedData.Add(shapedObject);
		}

		return shapedData;
	}

	private ExpandoObject FetchDataForExpandoObject(T ExpandoObject, IEnumerable<PropertyInfo> requiredProperties)
	{
		var shapedObject = new ExpandoObject();

		foreach (var property in requiredProperties)
		{
			var objectPropertyValue = property.GetValue(ExpandoObject);
			shapedObject.TryAdd(property.Name, objectPropertyValue);
		}

		return shapedObject;
	}
}
