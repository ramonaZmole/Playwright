using Microsoft.Playwright.NUnit;
using PlaywrightNUnit.Pages;

namespace PlaywrightNUnit.Tests;

public class Pom
{
    //private IPage _page;

    [SetUp]
    public Task Setup()
    {
        return Task.CompletedTask;
    }

    [Test]
    public async Task Test3Pom()
    {
        using var playwright = await Playwright.CreateAsync();

        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            //  Headless = false
        });
        var _page = await browser.NewPageAsync();

        var loginPage = new LoginPage(_page);

        await _page.GotoAsync("https://automationintesting.online/#/admin");
        await loginPage.Login();

        Assert.IsTrue(await _page.Locator("#brandingLink").IsVisibleAsync());

        //await _page.ScreenshotAsync(new PageScreenshotOptions
        //{
        //    Path = "screenshot3.jpg"
        //});
    }
}