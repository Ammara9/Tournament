namespace Tournament.Core.DTOs
{
    public record TournamentUpdateeDto : TournamentForManipulationDto
    {
        public int Id { get; init; }
    }
}
