using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository _walkDifficultyRepository;
        private readonly IMapper _mapper;

        public WalkDifficultyController(IWalkDifficultyRepository walkDifficultyRepository, IMapper mapper)
        {
            _walkDifficultyRepository = walkDifficultyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficulties()
        {
            var WalkDifficulties = await _walkDifficultyRepository.GetAllWalkDifficultiesAsync();

            var WalkDifficultiesDTO = _mapper.Map<List<Models.DTOs.WalkDifficulty>>(WalkDifficulties);

            return Ok(WalkDifficultiesDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficulty")]
        public async Task<IActionResult> GetWalkDifficulty(Guid id)
        {
            var WalkDifficulty = await _walkDifficultyRepository.GetWalkDifficultyAsync(id);

            if (WalkDifficulty == null)
                return NotFound();

            var WalkDifficultyDTO = _mapper.Map<Models.DTOs.WalkDifficulty>(WalkDifficulty);

            return Ok(WalkDifficultyDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync(Models.DTOs.AddWalkDiddicultyRequest addWalkDiddicultyRequest)
        {
            // convert DTO to domain model
            var walkDifficulty = new Models.Domain.WalkDifficulty
            {
                Code = addWalkDiddicultyRequest.Code
            };

            // call repository
            walkDifficulty = await _walkDifficultyRepository.AddWalkDifficultyAsync(walkDifficulty);

            // convert domain to DTO
            var walkDifficultyDTO = _mapper.Map<Models.DTOs.WalkDifficulty>(walkDifficulty);

            return CreatedAtAction(nameof(GetWalkDifficulty), new { id = walkDifficultyDTO.Id }, walkDifficultyDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync(Guid id, Models.DTOs.UpdateWalkDifficultyRequest updateWalkDifficultyRequest)
        {
            // convert DTO to domain model
            var walkDifficulty = new Models.Domain.WalkDifficulty
            {
                Code = updateWalkDifficultyRequest.Code
            };

            // call repository to update
            walkDifficulty = await _walkDifficultyRepository.UpdateWalkDifficultyAsync(id, walkDifficulty);

            if (walkDifficulty == null)
                return NotFound();

            // convert domain to DTO
            var walkDifficultyDTO = _mapper.Map<Models.DTOs.WalkDifficulty>(walkDifficulty);

            // return response
            return Ok(walkDifficultyDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalksDifficulty(Guid id)
        {
            var walkDifficulty = await _walkDifficultyRepository.DeleteWalkDifficultyAsync(id);

            if (walkDifficulty == null)
                return NotFound();

            // convert back to DTO
            var walkDifficultyDTO = _mapper.Map<Models.DTOs.WalkDifficulty>(walkDifficulty);

            return Ok(walkDifficultyDTO);
        }
    }
}
