using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointOfInterestDto : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetPointsOfInterrest(int cityId)
        {
            var city = CitiesStoreData.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(city == null)
                return NotFound();

            return Ok(city.PointOfInterest);
        }

        [HttpGet("{pointsofinterestId}")]
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
    }
}
