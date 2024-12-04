namespace Tournament.Shared.DTOs
{
    public record TournamentDetailsDto
    {
        public int Id { get; init; }
        public string? Title { get; init; }

        public DateTime StartDate { get; init; }
        public IEnumerable<GameDto>? Games { get; init; }
    }
}
