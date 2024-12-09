﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Constracts;
using Tournament.Core.Contracts;
using Tournament.Core.DTOs;
using Tournament.Core.Entities;
using Tournament.Data.Data;

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

        //// PUT: api/TournamentDetails/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTournamentDetails(int id, TournamentUpdateeDto dto)
        //{
        //    if (id != dto.Id)
        //        return BadRequest();

        //    var existingTournament = await uow.TournamentRepository.GetTournamentDetailsAsync(id);
        //    if (existingTournament == null)
        //        return NotFound();

        //    _mapper.Map(dto, existingTournament);
        //    await uow.CompleteAsync();

        //    return Ok(_mapper.Map<TournamentDetailsDto>(existingTournament));

        //    //return NoContent();
        //}

        //// POST: api/TournamentDetails
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<TournamentDetailsDto>> PostTournamentDetails(
        //    TournamentDetailsCreateDto dto
        //)
        //{
        //    var tournament = _mapper.Map<TournamentDetails>(dto);
        //    uow.TournamentRepository.Add(tournament);
        //    await uow.CompleteAsync();

        //    var createdTournament = _mapper.Map<TournamentDetailsDto>(tournament);
        //    return CreatedAtAction(
        //        nameof(GetTournamentDetails),
        //        new { id = createdTournament.Id },
        //        createdTournament
        //    );
        //}

        //// DELETE: api/TournamentDetails/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTournamentDetails(int id)
        //{
        //    var tournamentDetails = await uow.TournamentRepository.GetTournamentDetailsAsync(id);
        //    if (tournamentDetails == null)
        //    {
        //        return NotFound();
        //    }

        //    uow.TournamentRepository.Remove(tournamentDetails);
        //    await uow.CompleteAsync();

        //    return NoContent();
        //}
    }
}
