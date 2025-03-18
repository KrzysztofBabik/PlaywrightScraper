using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightScraper.Scraper
{
    public class MediaExpertScraper : ScraperBase
    {
        public async override Task ScrapeAsync(string url)
        {
            if (url == null) throw new InvalidOperationException("Page is not initialized!");

            await _page.GotoAsync(url);
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            var title = await _page.TextContentAsync("h1");

            var priceElement = await _page.QuerySelectorAsync(".whole");
            if (priceElement != null)
            {
                var priceText = await priceElement.TextContentAsync();
                var cleanedPriceText = priceText?.Replace("\u202F", "").Trim();

                if (decimal.TryParse(cleanedPriceText, out decimal price))
                {
                    Console.WriteLine($"Product: {title?.Trim()}, Price: {price}");
                }
                else
                {
                    throw new FormatException("The price format is invalid or the price could not be parsed.");
                }

            }
            else
            {
                throw new InvalidOperationException("The price of this item cannot be found!");
            }
        }
    }
}
