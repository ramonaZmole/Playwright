using Microsoft.Playwright;

namespace PlaywrightNUnit
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            //arrange

            //act

            //assert
        }

        [Test]
        public async Task Test1()
        {
            using var playwright = await Playwright.CreateAsync();

            await using var browser = await playwright.Chromium.LaunchAsync();

            var page = await browser.NewPageAsync();

            await page.GotoAsync("https://automationintesting.online/");
            await page.ClickAsync(".row.hotel-room-info [type='button']");
        }
    }
}