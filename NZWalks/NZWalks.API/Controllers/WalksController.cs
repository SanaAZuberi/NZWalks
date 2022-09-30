using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalksReposiory _walksReposiory;
        private readonly IMapper _mapper;

        // constructor injection
        public WalksController(IWalksReposiory walksReposiory, IMapper mapper)
        {
            _walksReposiory = walksReposiory;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            // Fetch data from db - domain walks
            var walks = await _walksReposiory.GetAllWalksAsync();

            // convert domain walks to DTO
            var walksDTO = _mapper.Map<List<Models.DTOs.Walk>>(walks);

            // return Response
            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            // Get Walk domain object from db
            var walk = await _walksReposiory.GetWalkAsync(id);

            // convert to DTO object
            var walkDTO = _mapper.Map<Models.DTOs.Walk>(walk);

            // return response
            return Ok(walkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTOs.AddWalkRequest addWalkRequest)
        {
            // Convert DTO to domain
            var walk = new Models.Domain.Walk
            {
                Name = addWalkRequest.Name,
                Length = addWalkRequest.Length,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId,
            };

            // Pass domain obj to Repository to persist this
            walk = await _walksReposiory.AddWalkAsync(walk);

            // Convert the Domain obj back to DTO
            //we can do manually also
            var walkDTO = _mapper.Map<Models.DTOs.Walk>(walk);

            // Send DTO respnse back to Client
            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] Models.DTOs.UpdateWalkRequest updateWalkRequest)
        {
            // convert DTO to domain object
            var walk = new Models.Domain.Walk
            {
                Name = updateWalkRequest.Name,
                Length = updateWalkRequest.Length,
                RegionId = updateWalkRequest.RegionId,
                WalkDifficultyId = updateWalkRequest.WalkDifficultyId,
            };

            // Pass details to Repository 
            walk = await _walksReposiory.UpdateWalkAsync(id, walk);


            // If null return NotFound()
            if (walk == null)
                return NotFound();

            // Convert Domain back to DTO
            var walkDTO = _mapper.Map<Models.DTOs.Walk>(walk);

            //Return Response
            return Ok(walkDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            // call Repository to delete walk
            var walk = await _walksReposiory.DeleteWalkAsync(id);

            if (walk == null)
                return NotFound();

            var walkDTO = _mapper.Map<Models.DTOs.Walk>(walk);

            return Ok(walkDTO);
        }
    }
}
