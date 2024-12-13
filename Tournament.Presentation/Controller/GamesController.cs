using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Constracts;
using Tournament.Core.Contracts;
using Tournament.Core.DTOs;
using Tournament.Data.Data;

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
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGame(int tournamentId)
        {
            var tournamentExist = await serviceManager.TournamentService.AnyAsync(tournamentId);
            if (!tournamentExist)
                return NotFound();

            var games = await serviceManager.GameService.GetGamesAsync(tournamentId);
            return Ok(games);
        }

        // GET: api/Games/5
        [HttpGet("{gameId}")]
        public async Task<IActionResult> GetGame(int tournamentId, int gameId)
        {
            var game = await serviceManager.GameService.GetGameAsync(tournamentId, gameId, false);

            if (game == null)
                return NotFound();

            return Ok(game);
        }

        //// PUT: api/Games/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutGame(int id, Game game)
        //{
        //    if (id != game.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(game).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!GameExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Games
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Game>> PostGame(Game game)
        //{
        //    _context.Game.Add(game);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetGame", new { id = game.Id }, game);
        //}

        //// DELETE: api/Games/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteGame(int id, int tournamentId)
        //{
        //    var tournamentExist = await _context.TournamentDetails.AnyAsync(t =>
        //        t.Id == tournamentId
        //    );
        //    if (!tournamentExist)
        //        return NotFound();

        //    var game = await _context.Games.FirstOrDefaultAsync(g =>
        //        g.Id.Equals(id) && g.TournamentDetailsId.Equals(tournamentId)
        //    );
        //    if (game == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Games.Remove(game);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

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
