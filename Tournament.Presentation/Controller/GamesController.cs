﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Constracts;
using Tournament.Core.DTOs;
using Tournament.Core.Entities;

namespace Tournament.Presentation.Controller
{
    [Route("api/tournaments/{tournamentId}/games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IServiceManager serviceManager;

        public GamesController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult> GetGames(
            int tournamentId,
            int pageNumber = 1,
            int pageSize = 20
        )
        {
            // Maximal pageSize-gräns
            if (pageSize > 100)
                pageSize = 100;

            // Kontrollera om den angivna sidan är större än 0
            if (pageNumber < 1)
                pageNumber = 1;

            // Anropa tjänsten för att hämta paginerade spel och metadata
            var result = await serviceManager.GameService.GetPaginatedGamesAsync(
                tournamentId,
                pageNumber,
                pageSize
            );

            // Skicka tillbaka data och metadata
            return Ok(result);
        }

        // GET: api/Games/5
        [HttpGet("{gameId}")]
        public async Task<IActionResult> GetGames(int tournamentId, int gameId)
        {
            var game = await serviceManager.GameService.GetGameAsync(tournamentId, gameId, false);

            if (game == null)
                return NotFound();

            return Ok(game);
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{gameId}")]
        public async Task<IActionResult> PutGame(int tournamentId, int gameId, Game game)
        {
            var updateGame = await serviceManager.GameService.UpdateGameAsync(
                tournamentId,
                gameId,
                game
            );
            return Ok(updateGame);
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(GameUpdateDto dto)
        {
            var postGame = await serviceManager.GameService.PostGameAsync(dto);

            return CreatedAtAction(
                nameof(GetGames),
                new
                { /*tournamentId = dto.TournamentDetailsId,*/
                    gameId = postGame.Id,
                },
                postGame
            );
        }

        // DELETE: api/tournaments/{tournamentId}/games/{gameId}
        [HttpDelete("{gameId}")]
        public async Task<IActionResult> DeleteGame(int tournamentId, int gameId)
        {
            //// Kontrollera om turneringen finns
            //var tournamentExist = await serviceManager.TournamentService.AnyAsync(tournamentId);
            //if (!tournamentExist)
            //    return NotFound(new { message = "Tournament not found." });

            var result = await serviceManager.GameService.DeleteGameAsync(tournamentId, gameId);
            if (!result)
                return NotFound(new { message = "Game not found." });

            return NoContent();
        }

        //[HttpPatch("{id:int}")]
        //public async Task<ActionResult> PatchGame(
        //    int tournamentId,
        //    int id,
        //    JsonPatchDocument<GameUpdateDto> patchDocument
        //)
        //{
        //    if (patchDocument is null)
        //        return BadRequest("No patch document");

        //    var tournamentExist = await _context.TournamentDetails.AnyAsync(t =>
        //        t.Id == tournamentId
        //    );
        //    if (!tournamentExist)
        //        return NotFound();

        //    var gameToPatch = await _context.Games.FirstOrDefaultAsync(g =>
        //        g.Id.Equals(id) && g.TournamentDetailsId.Equals(tournamentId)
        //    );
        //    if (gameToPatch == null)
        //        return NotFound();

        //    var dto = _mapper.Map<GameUpdateDto>(gameToPatch);
        //    patchDocument.ApplyTo(dto, ModelState);
        //    TryValidateModel(dto);

        //    if (!ModelState.IsValid)
        //        return UnprocessableEntity();

        //    _mapper.Map(dto, gameToPatch);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool GameExists(int id)
        //{
        //    return _context.Game.Any(e => e.Id == id);
        //}
    }
}
