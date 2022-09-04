using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    [Authorize]

    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        const int maxCitiesPageSize = 20;

        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointOfInterestDto>>> GetCities(string? name, string? searchQuery,int pageNumber=1, int pageSize=10 )
        {
            if (pageSize > maxCitiesPageSize)
            {
                pageSize = maxCitiesPageSize;
            }


            var (cityEntities, paginationMetadata) = await _cityInfoRepository
               .GetCitiesAsync(name, searchQuery, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<CityWithoutPointOfInterestDto>>(cityEntities));
        }

  
        [HttpGet("{cityId}")]
        public async Task<ActionResult> GetCity(int cityId, bool isIncludePointOfInterest)
        {
            var city = await _cityInfoRepository.GetCityAsync(cityId, isIncludePointOfInterest);
            if (city == null)
            {
                return NotFound();
            }
            if (isIncludePointOfInterest)
            {
                return Ok(_mapper.Map<CityDto>(city));

            }

            return Ok(_mapper.Map<CityWithoutPointOfInterestDto>(city));

        }
    }
}
