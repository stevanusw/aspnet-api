namespace TodoApp.Models.Parameters
{
    public record TaskParameters : RequestParameters
    {
        public override string? OrderBy { get; init; } = "CreateDateUtc DESC";
    }
}
