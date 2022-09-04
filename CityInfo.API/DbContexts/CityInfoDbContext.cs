using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts
{
    public class CityInfoDbContext: DbContext
    {
        public DbSet<City> Cities { get; set; }

        public DbSet<PointOfInterest> PointOfInterests { get; set; }

        public CityInfoDbContext(DbContextOptions<CityInfoDbContext> options)
        :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>()
                .HasData(
                new City("Kairoun")
                {
                    Id = 1,
                    Description = "Description"
                },
                new City("Aryana")
                {
                    Id = 2,
                    Description = "Description"
                },
                 new City("Tunis")
                 {
                     Id = 3,
                     Description = "Description"
                 });
            modelBuilder.Entity<PointOfInterest>()
                .HasData(
                new PointOfInterest("Central Park CP1")
                {
                    Id = 1,
                    Description = "Description",
                    CityId = 1,
                },
                  new PointOfInterest("Central Park CP2")
                  {
                      Id = 2,
                      Description = "Description",
                      CityId = 1,
                  },
                new PointOfInterest("CP 3")
                {
                    Id = 3,
                    Description = "Description",
                    CityId = 2,
                },
                 new PointOfInterest("CP 4")
                 {
                     Id = 4,
                     Description = "Description",
                     CityId = 3,
                 });
        }

    }
}
