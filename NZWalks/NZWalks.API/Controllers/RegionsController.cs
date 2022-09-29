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
        public async Task<IActionResult> GetAllRegions()
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
    }
}
