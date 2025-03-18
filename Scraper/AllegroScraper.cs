using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightScraper.Scraper
{
    public class AllegroScraper : ScraperBase
    {
        public async override Task ScrapeAsync(string url)
        {
            if ( url == null) throw new InvalidOperationException("Page is not initialized!");

            await _page.GotoAsync(url);
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            var title = await _page.TextContentAsync("h1");
            var price = await _page.TextContentAsync(".m-price__amount");

            Console.WriteLine($"Product: {title?.Trim()}, Price: {price.Trim()}");
        }
    }
}
