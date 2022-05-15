using System.Reflection;
using System.Text;

namespace TodoApp.Data.Extensions.Utilities
{
    internal static class QueryBuilder
    {
		// Codemaze
        internal static string CreateOrderByQuery<T>(string orderByQueryString)
        {
			var orderParams = orderByQueryString.Trim().Split(',');
			var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
			var orderQueryBuilder = new StringBuilder();

			foreach (var param in orderParams)
			{
				if (string.IsNullOrWhiteSpace(param))
					continue;

				var propertyFromQueryName = param.Split(" ")[0];
				var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

				if (objectProperty == null)
					continue;

				var direction = param.EndsWith(" desc") ? "desc" : "asc";

				orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction}, ");
			}

			var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

			return orderQuery;
		}
    }
}
