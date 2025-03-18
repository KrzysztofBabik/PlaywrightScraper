using PlaywrightScraper.Scraper;

public class Program 
{
    static async Task Main(string[] args)
    {
        var allegroScraper = new AllegroScraper();
        await allegroScraper.InitializeAsync();
        await allegroScraper.ScrapeAsync("https://allegro.pl/oferta/1-opona-letnia-225-45r17-94y-nokian-powerproof-1-17005845528");
        await allegroScraper.CloseAsync();
    }
}