using Microsoft.Extensions.Configuration;
using MusikApp;
using System.Security.Cryptography;

// Läser inställningar från appsettings.json (samma sätt som i hero_databas).
// Databasens sökväg ligger där, så du slipper skriva den i koden.
var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var connectionString = config.GetConnectionString("Default")
    ?? throw new InvalidOperationException("Hittar ingen connection string 'Default' i appsettings.json.");

// Börja här. Den här raden visar att inställningarna lästes in, ta bort den när du vill.


// Tips på ordning (se UPPGIFT.md):
//   1. Rita upp huvudmenyn (alternativ numrerade 1-N) och läs in ett val med Console.ReadLine().
//      Skriv ut "Här kommer en lista senare" tills databasen är kopplad.
//   2. Skapa ett DbHandler-objekt: new DbHandler(connectionString)
//   3. Anropa DbHandlers metoder från menyn. Här ska ALDRIG SQL stå.
//   4. Avsluta snyggt när användaren väljer "Avsluta".
var db = new DbHandler(connectionString);

bool körAppen = true;

while (körAppen)
{
    Console.WriteLine("======================Musiklistappen==========================");
    Console.WriteLine("Välj alternativ:");
    Console.WriteLine("1) Lista alla låtar");
    Console.WriteLine("2) Välj en låt");
    Console.WriteLine("3) Sök efter låt");
    Console.WriteLine("4) Avsluta");
    Console.WriteLine("==============================================================");
    if (!int.TryParse(Console.ReadLine(), out int val))
    {
        Console.WriteLine("Skriv endast nummer");
        continue;
    }
    switch (val)
        {
        case 1:
            foreach (var song in db.GetAllSongs())
                {
                Console.WriteLine($"{song.Id}. {song.Titel} ({song.Langd / 60}: {song.Langd % 60:D2})");
            }
            break;
        case 2:
            Console.WriteLine("Vilken låt vill du välja? (skriv ID)");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Skriv endast siffror");
                continue;
            }
            var song2 = db.GetSongById(id);
            if (song2 == null) Console.WriteLine("Finns ingen låt med det id:t");
            else
                Console.WriteLine($"{song2.Titel}, {song2.Langd / 60}:{song2.Langd % 60:D2}, spår {song2.Nummer}");
            break;
        case 3:
            Console.WriteLine("Skriv in titel:");
            var titel = Console.ReadLine() ??"";
            var träffar = db.SearchSongs(titel);

            if (träffar.Count == 0) Console.WriteLine("Inga låtar hittades");
            Console.WriteLine($"{träffar.Count} Resultat hittades:");
            foreach (var song in träffar)
            {
                
                Console.WriteLine($"Id: ({song.Id}). {song.Titel}");
                
            }
            break;
        case 4:
            {
                körAppen = false;
                break;
            }
        default: { Console.WriteLine("Ogiltigt val"); break; } 
            
    }
    
        

}
