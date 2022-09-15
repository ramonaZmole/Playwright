using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightNUnit
{
    public class NUnitPlayWright : PageTest
    {
        [SetUp]
        public async Task Setup()
        {
            await Page.GotoAsync("https://automationintesting.online/#/admin");
        }

        [Test]
        public async Task Test2PageTestInherited()
        {
            await Page.FillAsync("#username", "admin");
            await Page.FillAsync("#password", "password");
            await Page.ClickAsync("#doLogin");

            await Expect(Page.Locator("#brandingLink")).ToBeVisibleAsync();

            await Page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = "screenshot.jpg"
            });
        }
    }
}