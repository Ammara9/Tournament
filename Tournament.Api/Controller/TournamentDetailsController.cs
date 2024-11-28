using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tournament.Api.DTOs;
using Tournament.Core.Entities;
using Tournament.Data.Data;

namespace Tournament.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentDetailsController : ControllerBase
    {
        private readonly TournamentApiContext _context;

        public TournamentDetailsController(TournamentApiContext context)
        {
            _context = context;
        }

        // GET: api/TournamentDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDetails>>> GetTournamentDetails()
        {
            var tournaments = _context.TournamentDetails.Select(t => new TournamentDetailsDto
            {
                Id = t.Id,
                Title = t.Title,
                StartDate = t.StartDate,
            });
            return Ok(await tournaments.ToListAsync());
        }

        // GET: api/TournamentDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDetailsDto>> GetTournamentDetails(int id)
        {
            var tournamentDetails = await _context.TournamentDetails.FindAsync(id);

            if (tournamentDetails == null)
            {
                return NotFound();
            }

            var dto = new TournamentDetailsDto
            {
                Id = tournamentDetails.Id,
                Title = tournamentDetails.Title,
                StartDate = tournamentDetails.StartDate,
            };
            return Ok(dto);
        }

        //// PUT: api/TournamentDetails/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTournamentDetails(
        //    int id,
        //    TournamentDetails tournamentDetails
        //)
        //{
        //    if (id != tournamentDetails.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(tournamentDetails).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TournamentDetailsExists(id))
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

        //// POST: api/TournamentDetails
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<TournamentDetails>> PostTournamentDetails(
        //    TournamentDetails tournamentDetails
        //)
        //{
        //    _context.TournamentDetails.Add(tournamentDetails);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(
        //        "GetTournamentDetails",
        //        new { id = tournamentDetails.Id },
        //        tournamentDetails
        //    );
        //}

        //// DELETE: api/TournamentDetails/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTournamentDetails(int id)
        //{
        //    var tournamentDetails = await _context.TournamentDetails.FindAsync(id);
        //    if (tournamentDetails == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.TournamentDetails.Remove(tournamentDetails);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TournamentDetailsExists(int id)
        //{
        //    return _context.TournamentDetails.Any(e => e.Id == id);
        //}
    }
}
