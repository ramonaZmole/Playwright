using Microsoft.Playwright;

namespace PlaywrightNUnit
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1RawPlaywright()
        {
            using var playwright = await Playwright.CreateAsync();

            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                // Headless = false
            });

            var page = await browser.NewPageAsync();

            await page.GotoAsync("https://automationintesting.online/");
            await page.ClickAsync(".row.hotel-room-info [type='button']");
            await page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = "screenshot.jpg"
            });
        }
    }
}