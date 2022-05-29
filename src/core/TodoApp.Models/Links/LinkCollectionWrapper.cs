namespace TodoApp.Models.Links
{
    public record LinkCollectionWrapper<T>(IEnumerable<T> Values, IEnumerable<Link> Links);
}
