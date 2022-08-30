using CityInfo.API.LocalMailService;
using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointOfInterestDtoController : ControllerBase
    {
        private readonly ILogger<PointOfInterestDtoController> _logger;
        private readonly IMailService _mailService;
        private readonly CitiesStoreData _citiesStoreData;


        public PointOfInterestDtoController(ILogger<PointOfInterestDtoController>? logger , IMailService mailService ,CitiesStoreData citiesStoreData)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService;
            _citiesStoreData = citiesStoreData;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetPointsOfInterrest(int cityId)
        {
            try
            {
                throw new Exception("Exeption sample");
                var city = _citiesStoreData.Cities.FirstOrDefault(c => c.Id == cityId);
                if (city == null)
                {
                    _logger.LogInformation("the city id {0} not found", cityId);
                    return NotFound();
                }

                return Ok(city.PointOfInterest);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("City with {0} not found", cityId, ex);
                return StatusCode(500, "A problem happened while handle your request");
            }
        }

        [HttpGet("{pointsofinterestId}",Name= "GetPointOfInterest")]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointOfInterest(int cityId, int pointsofinterestId)
        {
            var city = _citiesStoreData.Cities.FirstOrDefault(c => c.Id == cityId);
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
            var city = _citiesStoreData.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            };
            var maxPoniOfInterest = _citiesStoreData.Cities.SelectMany(c => c.PointOfInterest).Max(p => p.Id);
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
        [HttpPut("{pointOfInterestid}")]
        public ActionResult<PointOfInterestDto> UpdatePonitOfInterest(int cityId,int pointOfInterestId,
            PointOfInterestOfUpdateDto pointOfInterest)
        {
            var city = _citiesStoreData.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var updatedPointOfInterest = city.PointOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);

            if (updatedPointOfInterest == null)
            {
                return NotFound();
            }

            updatedPointOfInterest.Name = pointOfInterest.Name;
            updatedPointOfInterest.Description = pointOfInterest.Description;

            return NoContent();
        }

        [HttpPatch("{pointOfInterestId}")]
        public ActionResult<PointOfInterestDto> PartiallyUpdatePointOfInterrest(int cityId, int pointOfInterestId, JsonPatchDocument<PointOfInterestOfUpdateDto> patchDocument)
        {
            var city = _citiesStoreData.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var updatedPointOfInterest = city.PointOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);

            if (updatedPointOfInterest == null)
            {
                return NotFound();
            }

            var pointOfInterestToPach = new PointOfInterestOfUpdateDto()
            {
                Name = updatedPointOfInterest.Name,
                Description = updatedPointOfInterest.Description
            };

            patchDocument.ApplyTo(pointOfInterestToPach, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            updatedPointOfInterest.Name = pointOfInterestToPach.Name;
            updatedPointOfInterest.Description = pointOfInterestToPach.Description;

            return NoContent();

        }

        [HttpDelete("{pointOfInterestId}")]
        public ActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            var city = _citiesStoreData.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var updatedPointOfInterest = city.PointOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);

            if (updatedPointOfInterest == null)
            {
                return NotFound();
            }

            city.PointOfInterest.Remove(updatedPointOfInterest);
            _mailService.Send("Point of interest deleted", $"Point of interest {updatedPointOfInterest.Name} with id {updatedPointOfInterest.Id} was deleted");
            return NoContent();
        }
    }
}
