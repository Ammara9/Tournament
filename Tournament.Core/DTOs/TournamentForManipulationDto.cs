namespace Tournament.Core.DTOs
{
    public record TournamentForManipulationDto
    {
        public string? Title { get; init; }
        public DateTime StartDate { get; init; }
    }
}
