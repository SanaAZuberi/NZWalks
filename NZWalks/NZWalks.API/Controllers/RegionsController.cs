using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    //Decorate with APIController attribute to tell our application that this is APIController
    [ApiController]
    //following the url of our application this will be the end point and this will map to the RegionsController
    //this will auto populate the controller name here and it will look like [Route("Regions")]
    [Route("[controller]")] 
    public class RegionsController : Controller
    {
        private readonly IRegionRepository _regionsRepository;
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository regionsRepository, IMapper mapper)
        {
            _regionsRepository = regionsRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllRegionsAsync()
        {
            // Synchronous call
            // var regions = _regionsRepository.GetAll();
            var regions = await _regionsRepository.GetAllAsync();

            var regionsDTO = _mapper.Map<List<Models.DTOs.Region>>(regions);

            return Ok(regionsDTO);

            /*
            //return DTO regions
            var regionsDTO = new List<Models.DTOs.Region>();

            regions.ToList().ForEach(region =>
            {
                var regionDTO = new Models.DTOs.Region()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    Area = region.Area,
                    Lattitude = region.Lattitude,
                    Longitude = region.Longitude,
                    Population = region.Population
                };

                regionsDTO.Add(regionDTO);
            });

            return Ok(regionsDTO);
            */

            /*
            Static List
            var regions = new List<Region>()
            {
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Wellington",
                    Code = "WLG",
                    Area = 227755,
                    Lattitude = -1.2354,
                    Longitude = 299.88,
                    Population = 500000
                },
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name = "Auckland",
                    Code = "AUCK",
                    Area = 458683,
                    Lattitude = -133.2354,
                    Longitude = 99.88,
                    Population = 2000000
                }
            };
            
            return Ok(regions);
            */
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await _regionsRepository.GetAsync(id);

            if (region == null)
                return NotFound();

            var regionDTO = _mapper.Map<Models.DTOs.Region>(region);

            return Ok(regionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(Models.DTOs.AddRegionRequest addRegion)
        {
            // convert Request to domain model
            var region = new Models.Domain.Region()
            {
                Code = addRegion.Code,
                Area = addRegion.Area,
                Lattitude = addRegion.Lattitude,
                Longitude = addRegion.Longitude,
                Name = addRegion.Name,
                Population = addRegion.Population,
            };

            // Pass details to Repository
            region = await _regionsRepository.AddAsync(region);

            // Convert back to DTO
            var regionDTO = new Models.DTOs.Region
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                Area = region.Area,
                Lattitude = region.Lattitude,
                Longitude = region.Longitude,
                Population = region.Population,
            };

            // it needs an action name where this resource could be found
            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            // delete region from db
            var region = await _regionsRepository.DeleteAsync(id);

            // if not found return
            if (region == null)
                return NotFound();

            // if found convert back to DTO
            //var regionDTO = _mapper.Map<Models.DTOs.Region>(region);
            var regionDTO = new Models.DTOs.Region()
            {
                Code = region.Code,
                Area = region.Area,
                Lattitude = region.Lattitude,
                Longitude = region.Longitude,
                Name = region.Name,
                Population = region.Population,
            };

            // return OK response
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] Models.DTOs.UpdateRegionRequest updateRegion)
        {
            // convert DTO to doamain model
            var region = new Models.Domain.Region()
            {
                Code = updateRegion.Code,
                Area = updateRegion.Area,
                Lattitude = updateRegion.Lattitude,
                Longitude = updateRegion.Longitude,
                Name = updateRegion.Name,
                Population = updateRegion.Population,
            };

            // Update region using repository
            region = await _regionsRepository.UpdateAsync(id, region);

            // If null then NotFound
            if (region == null)
                return NotFound();

            // Convert domain back to DTO
            var regionDTO = new Models.DTOs.Region()
            {
                Code = region.Code,
                Area = region.Area,
                Lattitude = region.Lattitude,
                Longitude = region.Longitude,
                Name = region.Name,
                Population = region.Population,
            };

            // Return OK response
            return Ok(regionDTO);
        }

    }
}
