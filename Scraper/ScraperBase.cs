using System;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace PlaywrightScraper.Scraper
{
    public abstract class ScraperBase
    {
        protected IBrowser? _browser;
        protected IBrowserContext? _context;
        protected IPage? _page;

        public async Task InitializeAsync()
        {
            var playwright = await Playwright.CreateAsync();
            _browser = await playwright.Chromium.LaunchAsync(
                new BrowserTypeLaunchOptions { 
                    Headless = true 
                });
            _context = await _browser.NewContextAsync();
            _page = await _context.NewPageAsync();
        }

        public async Task CloseAsync()
        {
            if (_browser != null) await _browser.CloseAsync();
        }

        public abstract Task ScrapeAsync(string url);
    }
}
