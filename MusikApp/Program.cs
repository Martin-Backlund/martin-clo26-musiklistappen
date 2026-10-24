using Microsoft.Extensions.Configuration;

// Läser inställningar från appsettings.json (samma sätt som i hero_databas).
// Databasens sökväg ligger där, så du slipper skriva den i koden.
var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var connectionString = config.GetConnectionString("Default")
    ?? throw new InvalidOperationException("Hittar ingen connection string 'Default' i appsettings.json.");

// Börja här. Den här raden visar att inställningarna lästes in, ta bort den när du vill.
Console.WriteLine($"Musiklistappen: börja med att få menyn att fungera!  (databas: {connectionString})");

// Tips på ordning (se UPPGIFT.md):
//   1. Rita upp huvudmenyn (alternativ numrerade 1-N) och läs in ett val med Console.ReadLine().
//      Skriv ut "Här kommer en lista senare" tills databasen är kopplad.
//   2. Skapa ett DbHandler-objekt: new DbHandler(connectionString)
//   3. Anropa DbHandlers metoder från menyn. Här ska ALDRIG SQL stå.
//   4. Avsluta snyggt när användaren väljer "Avsluta".

bool körAppen = true;

while (körAppen)
{
    Console.WriteLine("======================Musiklistappen==========================");
    Console.WriteLine("Välj alternativ:");
    Console.WriteLine("1) Lista alla låtar");
    Console.WriteLine("2) Välj en låt");
    Console.WriteLine("3) Avsluta");
    if (!int.TryParse(Console.ReadLine(), out int val))
    {
        Console.WriteLine("Skriv endast nummer");
        continue;
    }
    switch (val)
        {
        case 1:
            Console.WriteLine("Skriver ut alla låtar.......");
            //metod
            break;
        case 2:
            Console.WriteLine("Väljer den låten.........");
            break;
        case 3:
            {
                körAppen = false;
                break;
            }
    }
    
        

}
