namespace TodoApp.Models.Parameters
{
    public record TodoParameters(bool? IsCompleted) : RequestParameters
    {
        public override string? OrderBy { get; init; } = "CreateDateUtc DESC";
    }
}
