using System.Dynamic;

namespace TodoApp.Models.Links
{
    public record LinkResponse
    {
        public bool HasLinks { get; init; }
        public IEnumerable<ExpandoObject>? ShapedDtos { get; init; }
        public LinkCollectionWrapper<ExpandoObject>? LinkedDtos { get; init; }
    }
}
