using PlaywrightScraper.DataBase;
using PlaywrightScraper.Scraper;

public class Program 
{
    static async Task Main(string[] args)
    {
        var sqLiteHelper = new SQLiteHelper();

        var mediaExpertScraper = new MediaExpertScraper();
        await mediaExpertScraper.InitializeAsync();
        await mediaExpertScraper.ScrapeAsync("https://www.mediaexpert.pl/telewizory-i-rtv/telewizory/telewizor-samsung-qe55q74d-55-qled-4k-120hz-tizen-tv-dolby-atmos-hdmi-2-1");
        await mediaExpertScraper.CloseAsync();
    }
}