using System.Collections.Generic;

namespace ToursDB;

public class Countries {
    public int Id;
    public string Name;
}

public class Tours {
    public int Id;
    public Decimal Price;
    public int Days;
    public string City;
    public Countries CountriesID;
}

public class Database {
    public List<Countries> Countries;
    public List<Tours> Tours;

    public Database() {
        Countries = new List<Countries> {
            new Countries {Id = 1, Name = "Турция"},
            new Countries {Id = 2, Name = "Таиланд"},
            new Countries {Id = 3, Name = "Тунис" },
        };

        Tours = new List<Tours> {
            new Tours {Id = 1, Days = 7, Price = 62000, City = "Гёйнюк", CountriesID = Countries[0]},
            new Tours {Id = 2, Days = 4, Price = 72000, City = "Сиде", CountriesID = Countries[0]},
            new Tours {Id = 3, Days = 9, Price = 96000, City = "Махмутлар", CountriesID = Countries[0]},
            new Tours {Id = 4, Days = 7, Price = 63000, City = "Пхукет", CountriesID = Countries[1]},
            new Tours {Id = 5, Days = 14, Price = 86000, City = "Краби", CountriesID = Countries[1]},
            new Tours {Id = 6, Days = 15, Price = 89000, City = "Краби", CountriesID = Countries[1]},
            new Tours {Id = 7, Days = 9, Price = 77000, City = "Джерба", CountriesID = Countries[2]},
            new Tours {Id = 8, Days = 21, Price = 91000, City = "Хаммамет", CountriesID = Countries[2]},
            new Tours {Id = 9, Days = 7, Price = 56000, City = "Джерба", CountriesID = Countries[2]},
        };
    }
}