using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ToursDB.EF {
    internal class ToursContext : DbContext {
        public DbSet<contCountry> contCountries { get; set; }
        public DbSet<contTour> contTours { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(@"Server = DESKTOP-C2GVDHV; Database = ToursDB; Trusted_Connection = True;");
            base.OnConfiguring(optionsBuilder);
        }

        public void CreateDbIfNotExist() {
            this.Database.EnsureCreated();
        }

        public void DropDB() {
            this.Database.EnsureDeleted();
        }




        public class contCountry {
            public int Id { get; set; }
            public string? Title { get; set; }
        }

        public class contTour {
            public int Id { get; set; }
            public Decimal Price { get; set; }
            public int Days { get; set; }
            public string? City { get; set; }
            public contCountry CountriesID { get; set; }
        }




        public void AddCountries() {
            contCountries.AddRange(entities: new contCountry[]
            {
                new contCountry { Title = "Турция" },
                new contCountry { Title = "Таиланд" },
                new contCountry { Title = "Тунис" }
            });
        }

        public void AddTours() {
            contTours.AddRange(entities: new contTour[]
            {
                new contTour { Price = 62000, Days = 7, City = "Гёйнюк", CountriesID = contCountries.First(t => t.Id == 1)},
                new contTour { Price = 72000, Days = 4, City = "Сиде", CountriesID = contCountries.First(t => t.Id == 1)},
                new contTour { Price = 96000, Days = 9, City = "Махмутлар", CountriesID = contCountries.First(t => t.Id == 1)},
                new contTour { Price = 63000, Days = 7, City = "Пхукет", CountriesID = contCountries.First(t => t.Id == 2)},
                new contTour { Price = 86000, Days = 14, City = "Краби", CountriesID = contCountries.First(t => t.Id == 2)},
                new contTour { Price = 89000, Days = 15, City = "Краби", CountriesID = contCountries.First(t => t.Id == 2)},
                new contTour { Price = 77000, Days = 9, City = "Джерба", CountriesID = contCountries.First(t => t.Id == 3)},
                new contTour { Price = 91000, Days = 21, City = "Хаммамет", CountriesID = contCountries.First(t => t.Id == 3)},
                new contTour { Price = 56000, Days = 7, City = "Джерба", CountriesID = contCountries.First(t => t.Id == 3)}
            });
        }

    }
}
