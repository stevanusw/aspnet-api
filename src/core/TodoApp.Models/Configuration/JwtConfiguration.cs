namespace TodoApp.Models.Configuration
{
    public record JwtConfiguration
    {
        public string? Issuer { get; init; }
        public string? Audience { get; init; }
        public int ExpiresMins { get; init; }
        public string? Secret { get; init; }
    }
}
