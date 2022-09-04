using AutoMapper;

namespace CityInfo.API.Profiles
{
    public class PointOfInterestProfile: Profile
    {
        public PointOfInterestProfile()
        {
           
            CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>();
            CreateMap<Models.PointOfInterestOfCreationDto, Entities.PointOfInterest>();
            CreateMap<Models.PointOfInterestOfUpdateDto, Entities.PointOfInterest>();
            CreateMap<Entities.PointOfInterest, Models.PointOfInterestOfUpdateDto>();
        }
    }
}
