using Microsoft.AspNetCore.Mvc;
using Services.Constracts;
using Tournament.Core.DTOs;
using Tournament.Core.Entities;

namespace Tournament.Presentation.Controller
{
    [Route("api/tournaments")]
    [ApiController]
    public class TournamentDetailsController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public TournamentDetailsController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        // GET: api/TournamentDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDetails>>> GetTournamentDetails(
            bool includeGames
        )
        {
            //this getting games together with tournament with repos
            var tournamentDtos = await serviceManager.TournamentService.GetTournamentDetailsAsync(
                includeGames
            );

            return Ok(tournamentDtos);
        }

        // GET: api/TournamentDetails/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TournamentDetailsDto>> GetTournamentDetails(int id) =>
            Ok(await serviceManager.TournamentService.GetTournamentAsync(id));

        // PUT: api/TournamentDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournamentDetails(int id, TournamentUpdateeDto dto)
        {
            var updateTournament = await serviceManager.TournamentService.UpdateTournamentAsync(
                id,
                dto
            );

            return Ok(updateTournament);
        }

        // POST: api/TournamentDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TournamentDetailsDto>> PostTournamentDetails(
            TournamentDetailsCreateDto dto
        )
        {
            var postTournament = await serviceManager.TournamentService.PostTournamentAsync(dto);
            return CreatedAtAction( //this use for 201
                nameof(GetTournamentDetails),
                new { id = postTournament.Id },
                postTournament
            );
        }

        // DELETE: api/TournamentDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournamentDetails(int id)
        {
            var result = await serviceManager.TournamentService.DeleteTournamentAsync(id);
            return Ok(result);
        }
    }
}
