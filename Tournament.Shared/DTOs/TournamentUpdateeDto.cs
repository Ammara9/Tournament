namespace Tournament.Shared.DTOs
{
    public record TournamentUpdateeDto : TournamentForManipulationDto
    {
        public int Id { get; init; }
    }

}
