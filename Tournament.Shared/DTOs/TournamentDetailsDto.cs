namespace Tournament.Api.DTOs
{
    public record TournamentDetailsDto
    {
        public int Id { get; init; }
        public string? Title { get; init; }

        public DateTime StartDate { get; init; }
    }
}
