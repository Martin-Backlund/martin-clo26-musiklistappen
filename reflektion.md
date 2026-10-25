# Reflektion — Musiklistappen

**Namn:** Martin Bäcklund
**Datum:** 2026/10/25

---

## Vad var svårast?

Det svåraste var i början att få appen att hitta databasen. Jag råkade döpa om databasen till "musik.db.db. Jag hade också svårt att skilja på en metod som returnerar en lista och ett ensiklt objekt också att jag behövde loopa igenom resultatet från SearchSongs.

---

## Vad lärde du dig?

Jag lärde mig mycket mer om hur ett C# program kan prata med en databas med ADO.NET. Jag lärde mig också att läsa rader med 'reader.Read()' och göra om dem till objekt. 

---

## Hur använde du ADO.NET i praktiken — vad kändes naturligt och vad var förvirrande?

Tänk på de fem stegen: öppna anslutning, skapa kommando, skriv SQL, skicka in värden, kör.

Alla metoder följer samma fem steg, öppna anslutning, skapa kommando, skriv SQL, skicka in värden, kör. När första metoden GetAllSongs fungerade var resten nästan samma mönster vilket fick det att kännas mer naturligt. Det var förvirrande att hålla koll på kolumnernas ordning i reader.GetInt32(0) osv.

---

## Varför ska man använda parameteriserade queries?

Vad kan hända om man klistrar ihop SQL med strängar i stället? (Tänk på Bobby Tables.)

Om man klistar ihop SQL med det användaren skriver kan användaren skriva in egen SQL kod. Med parametrar som @sok skickas värdet separat och  behandlas som text inte som kod.

---

## Om du hade mer tid — vad hade du lagt till eller gjort annorlunda?

Jag hade gjort klart samtliga metoder och lagt in dessa i menyn. Jag planerar även att lägga in många fler låtar och album i databasen. Jag hade även som sagt tidigare sett till att skapa en metod som skriver ut låtar istället för att skriva om det varje gång.

---

## (VG) Varför delade du upp koden som du gjorde?

Resonera kring ditt designval: varför ligger databaskoden i `DbHandler` och inte i `Program.cs`? Vad hade blivit svårare om allt låg i samma fil?

All databaskod ligger i DbHandler.cs och menyn ligger i Program.cs. Då har varje fil ett ansvar då Program.cs pratar med användaren och DbHandler pratar med databasen. Om allt låg i samma fil hade allt blivit rörigt långt. På detta sätt blev det för mig lättare att hitta fel. Om jag vill byta databas eller ändra i någon av sql frågorna behöver jag bara ändra i Dbhandler.
