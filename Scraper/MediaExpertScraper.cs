using Microsoft.Playwright;
using PlaywrightScraper.DataBase;
using PlaywrightScraper.Notifier;
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
        SQLiteHelper _db;
        EmailNotifier _emailNotifier;
        public MediaExpertScraper()
        {
            _db = new SQLiteHelper();
            _emailNotifier = new EmailNotifier();

        }
        public async override Task ScrapeAsync(string url)
        {
            if (url == null) throw new InvalidOperationException("Page is not initialized!");

            await _page.GotoAsync(url);
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            var product = await _page.TextContentAsync("h1");

            var priceElement = await _page.QuerySelectorAsync(".whole");
            if (priceElement != null)
            {
                var priceText = await priceElement.TextContentAsync();
                var cleanedPriceText = priceText?.Replace("\u202F", "").Trim();

                if (decimal.TryParse(cleanedPriceText, out decimal price))
                {
                    Console.WriteLine($"Product: {product?.Trim()}, Price: {price}");
                    var lowestKnownPrice = _db.GetLowestPrice(product);
                    if (price <= lowestKnownPrice)
                    {
                        _emailNotifier.SendEmail($"This is the lowest known price for this product!", $"Price of product: {product}\n dropped to {price}PLN!");
                    }
                    _db.SavePrice(product, price.ToString());
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
