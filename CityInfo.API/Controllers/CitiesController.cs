using System.Reflection.Metadata.Ecma335;
using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly CitiesStoreData _citiesStoreData;

        public CitiesController(CitiesStoreData citiesStoreData)
        {
            _citiesStoreData = citiesStoreData;
        }
        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            return Ok(_citiesStoreData.Cities);

        }
        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity(int id)
        {

            var city = _citiesStoreData.Cities.FirstOrDefault(city => city.Id == id);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }
    }
}
