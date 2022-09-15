namespace PlaywrightNUnit.Tests;

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

        await page.GotoAsync("https://automationintesting.online/#/admin");
        await page.FillAsync("#username", "admin");
        await page.FillAsync("#password", "password");
        await page.ClickAsync("#doLogin");

        Assert.IsTrue(await page.Locator("#brandingLink").IsVisibleAsync());

        //await page.ScreenshotAsync(new PageScreenshotOptions
        //{
        //    Path = "screenshot.jpg"
        //});
    }
}