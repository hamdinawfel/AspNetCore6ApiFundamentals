using CityInfo.API.Controllers;

namespace CityInfo.API.Models
{
    public class CitiesStoreData
    {
        public List<CityDto> Cities { get; set; }
        //public static CitiesStoreData Current { get; } = new CitiesStoreData();

        public CitiesStoreData()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "New York",
                    Description = "Some description",
                    PointOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Central Park",
                            Description = "The most import place in New York city",
                        },
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Central Park",
                            Description = "The most import place in New York city",
                        }
                    }
                    
                    
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Kairoun",
                    Description = "Some description",
                    PointOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Central Park",
                            Description = "The most import place in New York city",
                        }
                    }
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "New York",
                    Description = "Some description",
                    PointOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Central Park",
                            Description = "The most import place in New York city",
                        }
                    }
                }
            };
        }
    }
}
