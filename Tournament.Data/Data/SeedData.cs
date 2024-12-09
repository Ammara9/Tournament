using System.Collections.Generic;
using System.Linq;
using Bogus;
using Tournament.Core.Entities;

namespace Tournament.Data.Data
{
    public class SeedData
    {
        public static async Task InitAsync(TournamentApiContext context)
        {
            if (context.TournamentDetails.Any())
                return;

            // Generate 3 tournaments with 5 games each
            var tournaments = GenerateTournaments(3);

            await context.TournamentDetails.AddRangeAsync(tournaments);
            await context.SaveChangesAsync();
        }

        private static IEnumerable<TournamentDetails> GenerateTournaments(int nrOfTournaments)
        {
            // Generate a fixed number of tournaments
            var faker = new Faker<TournamentDetails>()
                .RuleFor(t => t.Title, f => f.Lorem.Sentence(3))
                .RuleFor(t => t.StartDate, f => f.Date.Future())
                .RuleFor(t => t.Games, f => GenerateGames(5).ToList());

            return faker.Generate(nrOfTournaments);
        }

        private static ICollection<Game> GenerateGames(int nrOfGames)
        {
            // Generate a fixed number of games
            var faker = new Faker<Game>()
                .RuleFor(g => g.Title, f => f.Lorem.Word())
                .RuleFor(g => g.Time, f => f.Date.Soon());

            return faker.Generate(nrOfGames);
        }
    }
}
