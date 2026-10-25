using Microsoft.Data.Sqlite;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MusikApp;

/// <summary>
/// ALL databaskod bor här. Program.cs ska aldrig innehålla SQL eller SqliteConnection.
///
/// Det här är ett skelett: metoderna finns, kommentarerna visar vad varje metod ska göra,
/// men koden är din. Alla metoder kastar NotImplementedException tills du har skrivit dem.
///
/// ADO.NET i fem steg (samma i varje metod som pratar med databasen):
///   1. Öppna en anslutning        -> new SqliteConnection(...) och connection.Open()
///   2. Skapa ett kommando         -> connection.CreateCommand()
///   3. Skriv SQL:en               -> command.CommandText = "..."
///   4. Skicka in värden säkert    -> command.Parameters.AddWithValue("@namn", värde)
///   5. Kör kommandot              -> ExecuteReader() för SELECT, ExecuteNonQuery() för INSERT/DELETE
///
/// Använd "using var" på anslutning och kommando så stängs de av sig själva.
/// Bygg SQL:en med parametrar (@titel), aldrig genom att klistra ihop strängar.
/// </summary>
public class DbHandler
{
    private string _connectionString;


    // Tips: spara bara connection string här. Den kommer från appsettings.json
    // via Program.cs (t.ex. "Data Source=musik.db").
    // Öppna sedan en NY anslutning inuti varje metod nedan.

    public DbHandler(string connectionString)
    {
        _connectionString = connectionString;

        // TODO: spara connectionString i ett fält som metoderna nedan kan använda.
        
    }

    // ---------------------------------------------------------------
    // G-nivå: du behöver minst tre av de fem här (välj själv vilka)
    // ---------------------------------------------------------------

    /// <summary>Hämtar alla låtar, sorterade på titel.</summary>
    public List<Song> GetAllSongs()
    {
        var songs = new List<Song>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "SELECT id,titel,langd,nummer,album_id FROM song ORDER BY titel";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            songs.Add(new Song
            {
                Id = reader.GetInt32(0),
                Titel = reader.GetString(1),
                Langd = reader.GetInt32(2),
                Nummer = reader.IsDBNull(3) ? null : reader.GetInt32(3),
                Albumid = reader.GetInt32(4)
            });
        }
        return songs;
        
        // TODO: SELECT från din låt-tabell med ORDER BY.
        // TODO: loopa med reader.Read() och gör ett Song-objekt av varje rad.
        // Tips: reader.GetInt32(0), reader.GetString(1) ... eller reader["Titel"].
        
    }

    /// <summary>Hämtar en specifik låt. Returnerar null om den inte finns.</summary>
    public Song? GetSongById(int id)
    {
       
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT id,titel,langd,nummer,album_id FROM song WHERE id = @id";
        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new Song
            {


                Id = reader.GetInt32(0),
                Titel = reader.GetString(1),
                Langd = reader.GetInt32(2),
                Nummer = reader.IsDBNull(3) ? null : reader.GetInt32(3),
                Albumid = reader.GetInt32(4)
            };
        }
        else return null;



        }
        
        
        // TODO: SELECT ... WHERE Id = @id (parameter!).
        // TODO: if (reader.Read()) { ... } annars returnera null.
        
    

    /// <summary>Söker låtar där titeln innehåller söktexten.</summary>
    public List<Song> SearchSongs(string searchText)
    {
        var songs = new List<Song>();
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT id,titel,langd,nummer,album_id FROM song WHERE titel LIKE @sok";
        command.Parameters.AddWithValue("@sok", "%" + searchText +"%");

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            songs.Add(new Song
            {
                Id = reader.GetInt32(0),
                Titel = reader.GetString(1),
                Langd = reader.GetInt32(2),
                Nummer = reader.IsDBNull(3) ? null : reader.GetInt32(3),
                Albumid = reader.GetInt32(4)
            });
        }
        return songs;
            // TODO: SELECT ... WHERE Titel LIKE @sok.
            // Tips: parametervärdet ska vara "%" + searchText + "%" så att LIKE matchar delar av titeln.
           
    }

    /// <summary>Lägger till en ny låt.</summary>
    public void AddSong(Song song)
    {

        // TODO: INSERT INTO ... VALUES (@titel, @langd, ...) med parametrar.
        // Tips: INSERT ger ingen data tillbaka, så använd ExecuteNonQuery().
        throw new NotImplementedException();
    }

    /// <summary>Tar bort en låt. Returnerar true om en rad faktiskt togs bort.</summary>
    public bool DeleteSong(int id)
    {
        // TODO: DELETE FROM ... WHERE Id = @id.
        // Tips: ExecuteNonQuery() returnerar antalet påverkade rader.
        // Menyn i Program.cs ska fråga "Är du säker? j/n" innan den här metoden anropas.
        throw new NotImplementedException();
    }

    // ---------------------------------------------------------------
    // VG-nivå: du behöver minst EN av dessa två (och reflektionen i README)
    // ---------------------------------------------------------------

    /// <summary>(VG, alternativ A) Låtar tillsammans med albumets namn. Kräver en JOIN.</summary>
    public List<(string SongTitle, string AlbumName)> GetSongsWithAlbum()
    {
        var resultat = new List<(string SongTitle, string AlbumName)>();
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        using var command = connection.CreateCommand();
        command.CommandText = "SELECT song.titel, album.titel FROM song JOIN album ON song.album_id = album.id ORDER BY album.titel, song.nummer";
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            resultat.Add((reader.GetString(0), reader.GetString(1)));
        }
        return resultat;
        // TODO: SELECT Låtar.Titel, Album.Namn FROM Låtar JOIN Album ON ... ORDER BY ...
        
    }

    /// <summary>(VG, alternativ B) Låtar som är längre än medellängden. Kräver en subquery.</summary>
    public List<Song> GetSongsLongerThanAverage()
    {
        // TODO: SELECT ... WHERE Längd > (SELECT AVG(Längd) FROM Låtar).
        throw new NotImplementedException();
    }
}
