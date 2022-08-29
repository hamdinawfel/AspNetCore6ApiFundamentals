using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointOfInterestDtoController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetPointsOfInterrest(int cityId)
        {
            var city = CitiesStoreData.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(city == null)
                return NotFound();

            return Ok(city.PointOfInterest);
        }

        [HttpGet("{pointsofinterestId}",Name= "GetPointOfInterest")]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointOfInterest(int cityId, int pointsofinterestId)
        {
            var city = CitiesStoreData.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
                return NotFound();
            var pointOfInterest = city.PointOfInterest.FirstOrDefault(p => p.Id == pointsofinterestId);
            if (pointOfInterest == null)
                return NotFound();

            return Ok(pointOfInterest);
        }
        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(int cityId, PointOfInterestOfCreationDto pointOfInterest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var city = CitiesStoreData.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            };
            var maxPoniOfInterest = CitiesStoreData.Current.Cities.SelectMany(c => c.PointOfInterest).Max(p => p.Id);
            var finalPointOfinterest = new PointOfInterestDto()
            {
                Id = ++maxPoniOfInterest,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointOfInterest.Add(finalPointOfinterest);

            return CreatedAtRoute(
                "GetPointOfInterest",
                new
                {
                    cityId = cityId,
                    pointOfInterest = finalPointOfinterest.Id
                },
                finalPointOfinterest);
        }
    }
}
