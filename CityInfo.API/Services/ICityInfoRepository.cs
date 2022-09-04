using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();

        Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(string? name, string? searchQuery, int pageNumber, int pageSize);
        Task<City?> GetCityAsync(int cityId, bool isIncludePointOfInterest);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterrestAsync(int cityId);
        Task<PointOfInterest?> GetPointOfInterestAsync(int cityId, int pointOfInterestId);
        Task<bool> CityExistAsync(int cityId);
        Task AddPointOfInterestForCity(int City, PointOfInterest pointOfInterest);
        Task<bool> SaveChangesAsync();
        Task<bool> CityNameMatchesCityId(string? cityName, int cityId);

        void DeletePointOfInterestForCity(PointOfInterest pointOfInterest);
    }
}
