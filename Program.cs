using System.Data.SqlClient;
using ToursDB;
using ToursDB.EF;
using static ToursDB.EF.ToursContext;


static void AdoNetDemo() {
    var connection = new SqlConnection(@"Server = DESKTOP-C2GVDHV; Database = ToursDB; Trusted_Connection = True;");
    connection.Open();


    Console.WriteLine(" 1. Вывести список туров, отсортированных по убыванию стоимости:");
    var command = connection.CreateCommand();
    command.CommandText = "SELECT * FROM Tours, Countries as c WHERE Tours.CountriesID = c.Id ORDER BY Price DESC";
    var reader = command.ExecuteReader();
    while (reader.Read()) {
        Console.WriteLine($"Цена {reader.GetSqlMoney(reader.GetOrdinal("Price"))}, Кол-во дней {reader.GetInt32(reader.GetOrdinal("Days"))}, " +
                          $"Город {reader.GetString(reader.GetOrdinal("City"))}, Страна {reader.GetString(reader.GetOrdinal("Name"))}");
    }
    reader.Close();


    Console.WriteLine("\n 2. Вывести список туров, отсортированных по возрастанию стоимости дня пребывания:");
    command.CommandText = "SELECT Price, Days, City, Name FROM Tours, Countries as C WHERE Tours.CountriesID = C.Id ORDER BY (SELECT Price / Days)";
    reader = command.ExecuteReader();
    while (reader.Read()) {
        Console.WriteLine($"Цена {reader.GetSqlMoney(reader.GetOrdinal("Price"))}, Кол-во дней {reader.GetInt32(reader.GetOrdinal("Days"))}, " +
                          $"Город {reader.GetString(reader.GetOrdinal("City"))}, Страна {reader.GetString(reader.GetOrdinal("Name"))}");
    }
    reader.Close();


    Console.WriteLine("\n 3. Вывести список туров, стоимость. \"выше среднего по базе \":");
    command.CommandText = "SELECT Price, Days, City, Name FROM Tours, Countries as C WHERE Tours.CountriesID = C.Id AND Price > (SELECT AVG(Price) FROM Tours)";
    reader = command.ExecuteReader();
    while (reader.Read()) {
        Console.WriteLine($"Цена {reader.GetSqlMoney(reader.GetOrdinal("Price"))}, Кол-во дней {reader.GetInt32(reader.GetOrdinal("Days"))}, " +
                          $"Город {reader.GetString(reader.GetOrdinal("City"))}, Страна {reader.GetString(reader.GetOrdinal("Name"))}");
    }
    reader.Close();


    Console.WriteLine("\n 4. Вывести пары (город, число туров), используя группировку (GroupBy, Count):");
    command.CommandText = "SELECT City, Count(*) as count FROM Tours GROUP BY City";
    reader = command.ExecuteReader();
    while (reader.Read()) {
        Console.WriteLine($"Город {reader.GetString(reader.GetOrdinal("City"))}, Число туров {reader.GetInt32(reader.GetOrdinal("count"))}");
    }
    reader.Close();


    Console.WriteLine("\n 5. Извлечь тур с максимальной ценой, уценить его в полтора раза. Результат сохранить:");
    Console.WriteLine("Before:");
    command.CommandText = "SELECT Price, Days, City, Name FROM Tours, Countries as C WHERE Tours.CountriesID = C.Id";
    reader = command.ExecuteReader();
    while (reader.Read()) {
        Console.WriteLine($"Цена {reader.GetSqlMoney(reader.GetOrdinal("Price"))}, Кол-во дней {reader.GetInt32(reader.GetOrdinal("Days"))}, " +
                          $"Город {reader.GetString(reader.GetOrdinal("City"))}, Страна {reader.GetString(reader.GetOrdinal("Name"))}");
    }
    reader.Close();

    command.CommandText = "UPDATE Tours SET Price = Price / 1.5 WHERE Id = (SELECT Id FROM Tours WHERE Price = (SELECT MAX(Price) FROM Tours))";
    reader = command.ExecuteReader();
    reader.Close();

    Console.WriteLine("\nAfter:");
    command.CommandText = "SELECT Price, Days, City, Name FROM Tours, Countries as C WHERE Tours.CountriesID = C.Id";
    reader = command.ExecuteReader();
    while (reader.Read()) {
        Console.WriteLine($"Цена {reader.GetSqlMoney(reader.GetOrdinal("Price"))}, Кол-во дней {reader.GetInt32(reader.GetOrdinal("Days"))}, " +
                          $"Город {reader.GetString(reader.GetOrdinal("City"))}, Страна {reader.GetString(reader.GetOrdinal("Name"))}");
    }
    reader.Close();


    Console.WriteLine("\n 6. Найти тур с минимальной ценой и удалить из базы:");
    Console.WriteLine("Before:");
    command.CommandText = "SELECT Price, Days, City, Name FROM Tours, Countries as C WHERE Tours.CountriesID = C.Id";
    reader = command.ExecuteReader();
    while (reader.Read()) {
        Console.WriteLine($"Цена {reader.GetSqlMoney(reader.GetOrdinal("Price"))}, Кол-во дней {reader.GetInt32(reader.GetOrdinal("Days"))}, " +
                          $"Город {reader.GetString(reader.GetOrdinal("City"))}, Страна {reader.GetString(reader.GetOrdinal("Name"))}");
    }
    reader.Close();

    command.CommandText = "DELETE Tours WHERE Price = (SELECT MIN(Price) FROM Tours)";
    reader = command.ExecuteReader();
    reader.Close();

    Console.WriteLine("\nAfter:");
    command.CommandText = "SELECT Price, Days, City, Name FROM Tours, Countries as C WHERE Tours.CountriesID = C.Id";
    reader = command.ExecuteReader();
    while (reader.Read()) {
        Console.WriteLine($"Цена {reader.GetSqlMoney(reader.GetOrdinal("Price"))}, Кол-во дней {reader.GetInt32(reader.GetOrdinal("Days"))}, " +
                          $"Город {reader.GetString(reader.GetOrdinal("City"))}, Страна {reader.GetString(reader.GetOrdinal("Name"))}");
    }
    reader.Close();


    Console.WriteLine("\n 7. Найти страну с наименьшим числом туров и добавить в неё новый:");
    Console.WriteLine("Before:");
    command.CommandText = "SELECT Price, Days, City, Name FROM Tours, Countries as C WHERE Tours.CountriesID = C.Id";
    reader = command.ExecuteReader();
    while (reader.Read()) {
        Console.WriteLine($"Цена {reader.GetSqlMoney(reader.GetOrdinal("Price"))}, Кол-во дней {reader.GetInt32(reader.GetOrdinal("Days"))}, " +
                          $"Город {reader.GetString(reader.GetOrdinal("City"))}, Страна {reader.GetString(reader.GetOrdinal("Name"))}");
    }
    reader.Close();

    command.CommandText = "INSERT INTO Tours VALUES  ((SELECT MAX(Id) FROM Tours) + 1, 56000, 7, 'Джерба', (SELECT TOP(1) Tours.CountriesID FROM Tours " +
                          "GROUP BY CountriesID ORDER BY COUNT(CountriesID)))";
    reader = command.ExecuteReader();
    reader.Close();

    Console.WriteLine("\nAfter:");
    command.CommandText = "SELECT Price, Days, City, Name FROM Tours, Countries as C WHERE Tours.CountriesID = C.Id";
    reader = command.ExecuteReader();
    while (reader.Read()) {
        Console.WriteLine($"Цена {reader.GetSqlMoney(reader.GetOrdinal("Price"))}, Кол-во дней {reader.GetInt32(reader.GetOrdinal("Days"))}, " +
                          $"Город {reader.GetString(reader.GetOrdinal("City"))}, Страна {reader.GetString(reader.GetOrdinal("Name"))}");
    }
    reader.Close();

    connection.Close();
}

static void LinqDemo() {
    Database db = new Database();


    Console.WriteLine(" 1. Вывести список туров, отсортированных по убыванию стоимости:");
    foreach (Tours t in db.Tours.OrderByDescending(t => t.Price))
        Console.WriteLine($"{t.Price} {t.Days} {t.City} {t.CountriesID.Name}");



    Console.WriteLine("\n 2. Вывести список туров, отсортированных по возрастанию стоимости дня пребывания:");
    foreach (Tours t in db.Tours.OrderBy(t => Convert.ToDouble(t.Price) / Convert.ToDouble(t.Days)))
        Console.WriteLine($"{t.Price} {t.Days} {t.City} {t.CountriesID.Name}");



    Console.WriteLine("\n 3. Вывести список туров, стоимостью \"выше среднего по базе\":");
    Decimal avgPrice = db.Tours.Average(t => t.Price);
    foreach (Tours t in db.Tours.Where(t => t.Price > avgPrice))
        Console.WriteLine($"{t.Price} {t.Days} {t.City} {t.CountriesID.Name}");



    Console.WriteLine("\n 4. Вывести пары [город, число туров], используя группировку (GroupBy, Count):");
    var cities = db.Tours.GroupBy(t => t.City);
    foreach (var c in cities)
        Console.WriteLine($"{c.Key} {db.Tours.Where(t => t.City == c.Key).Count()}");



    Console.WriteLine("\n 5. Извлечь тур с макисмальной ценой, уценить его в полтора раза. Результат сохранить:");
    Console.WriteLine("Before:");
    foreach (Tours t in db.Tours)
        Console.WriteLine($"{t.Price} {t.Days} {t.City} {t.CountriesID.Name}");

    Decimal maxPrice = db.Tours.Max(t1 => t1.Price);
    foreach (Tours t in db.Tours.Where(t => t.Price == maxPrice))
        t.Price = Convert.ToDecimal(Convert.ToDouble(maxPrice) / 1.5);

    Console.WriteLine("\nAfter:");
    foreach (Tours t in db.Tours)
        Console.WriteLine($"{t.Price} {t.Days} {t.City} {t.CountriesID.Name}");



    Console.WriteLine("\n 6. Найти тур с минимальной ценой и удалить из базы:");
    Console.WriteLine("Before:");
    foreach (Tours t in db.Tours)
        Console.WriteLine($"{t.Price} {t.Days} {t.City} {t.CountriesID.Name}");

    Decimal min = db.Tours.Min(t => t.Price);
    foreach (Tours t in db.Tours.Where(t => t.Price == min)) {
        db.Tours.Remove(t);
        break;
    }

    Console.WriteLine("\nAfter:");
    foreach (Tours t in db.Tours)
        Console.WriteLine($"{t.Price} {t.Days} {t.City} {t.CountriesID.Name}");



    Console.WriteLine("\n 7. Найти страну с наименьшим числом туров и добавить в неё новый:");
    Console.WriteLine("Before:");
    foreach (Tours t in db.Tours)
        Console.WriteLine($"{t.Price} {t.Days} {t.City} {t.CountriesID.Name}");

    var countries = db.Tours.GroupBy(t => t.CountriesID.Name);
    int count = 4;
    string needCountry = " ";
    string town = " ";
    foreach (var c in countries) {
        int help = db.Tours.Where(t => t.CountriesID.Name == c.Key).Count();
        if (help < count) {
            count = help;
            foreach (Tours t in db.Tours.Where(t => t.CountriesID.Name == c.Key)) {
                needCountry = t.CountriesID.Name;
                town = t.City;
                break;
            }
        }
    }
    int id = db.Tours.Max(t => t.Id);
    foreach (Countries c in db.Countries.Where(c => c.Name == needCountry))
        db.Tours.Add(new Tours { Id = id + 1, Days = 10, Price = 54000, City = town, CountriesID = c });

    Console.WriteLine("\nAfter:");
    foreach (Tours t in db.Tours)
        Console.WriteLine($"{t.Price} {t.Days} {t.City} {t.CountriesID.Name}");
}

static void EFDemo() {
    ToursContext db = new ToursContext();
    db.DropDB();
    db.CreateDbIfNotExist();
    db.AddCountries();
    db.SaveChanges();
    db.AddTours();
    db.SaveChanges();



    Console.WriteLine(" 1. Вывести список туров, отсортированных по убыванию стоимости:");
    foreach (contTour t in db.contTours.OrderByDescending(t => t.Price)) {
        Console.WriteLine($"{t.Price} {t.Days} {t.City} {t.CountriesID.Title}");
    }



    Console.WriteLine("\n 2. Вывести список туров, отсортированных по возрастанию стоимости дня пребывания:");
    foreach (contTour t in db.contTours.OrderBy(t => Convert.ToDouble(t.Price) / Convert.ToDouble(t.Days)))
        Console.WriteLine($"{t.Price} {t.Days} {t.City} {t.CountriesID.Title}");



    Console.WriteLine("\n 3. Вывести список туров, стоимостью \"выше среднего по базе\":");
    Decimal avgPrice = db.contTours.Average(t => t.Price);
    foreach (contTour t in db.contTours.Where(t => t.Price > avgPrice))
        Console.WriteLine($"{t.Price} {t.Days} {t.City} {t.CountriesID.Title}");



    Console.WriteLine("\n 4. Вывести пары [город, число туров], используя группировку (GroupBy, Count):");
    var cities = db.contTours.GroupBy(t => t.City);
    foreach (var c in cities.Select(x => new { City = x.Key, Count = db.contTours.Where(t => t.City == x.Key).Count()}))
        Console.WriteLine($"{c.City} {c.Count}");



    Console.WriteLine("\n 5. Извлечь тур с макисмальной ценой, уценить его в полтора раза. Результат сохранить:");
    Console.WriteLine("Before:");
    foreach (contTour t in db.contTours)
        Console.WriteLine($"{t.Price} {t.Days} {t.City} {t.CountriesID.Title}");

    Decimal maxPrice = db.contTours.Max(t => t.Price);
    foreach (contTour t in db.contTours.Where(t => t.Price == maxPrice))
        t.Price = Convert.ToDecimal(Convert.ToDouble(maxPrice) / 1.5);
    db.SaveChanges();

    Console.WriteLine("\nAfter:");
    foreach (contTour t in db.contTours)
        Console.WriteLine($"{t.Price} {t.Days} {t.City} {t.CountriesID.Title}");



    Console.WriteLine("\n 6. Найти тур с минимальной ценой и удалить из базы:");
    Console.WriteLine("Before:");
    foreach (contTour t in db.contTours)
        Console.WriteLine($"{t.Price} {t.Days} {t.City} {t.CountriesID.Title}");

    Decimal min = db.contTours.Min(t => t.Price);
    foreach (contTour t in db.contTours.Where(t => t.Price == min)) {
        db.contTours.Remove(t);
        break;
    }
    db.SaveChanges();

    Console.WriteLine("\nAfter:");
    foreach (contTour t in db.contTours)
        Console.WriteLine($"{t.Price} {t.Days} {t.City} {t.CountriesID.Title}");



    Console.WriteLine("\n 7. Найти страну с наименьшим числом туров и добавить в неё новый:");
    Console.WriteLine("Before:");
    foreach (contTour t in db.contTours)
        Console.WriteLine($"{t.Price} {t.Days} {t.City} {t.CountriesID.Title}");

    var countries = db.contTours.GroupBy(t => t.CountriesID.Title);
    int count = 4;
    string needCountry = " ";
    string town = " ";
    foreach (var c in countries.Select(x => new { Country = x.Key })) {
        ToursContext db1 = new ToursContext();
        int help = db1.contTours.Where(t => t.CountriesID.Title == c.Country).Count();
        if (help < count) {
            count = help;
            foreach (contTour t in db1.contTours.Where(t => t.CountriesID.Title == c.Country)) {
                needCountry = t.CountriesID.Title;
                town = t.City;
                break;
            }
        }
        db1.Dispose();
    }
    int id = db.contTours.Max(t => t.Id);
    foreach (contCountry c in db.contCountries.Where(c => c.Title == needCountry))
        db.contTours.Add(new contTour { Id = id + 1, Days = 10, Price = 54000, City = town, CountriesID = c });
    db.SaveChanges();

    Console.WriteLine("\nAfter:");
    foreach (contTour t in db.contTours)
        Console.WriteLine($"{t.Price} {t.Days} {t.City} {t.CountriesID.Title}");
}

//AdoNetDemo();

//LinqDemo();

EFDemo();