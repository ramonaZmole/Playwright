using Microsoft.Playwright.NUnit;

namespace PlaywrightNUnit.Tests;

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
        var usernameInput = Page.Locator("#username");

        await usernameInput.FillAsync("admin");
        await Page.FillAsync("#password", "password");

        var loginButton = Page.Locator("button", new PageLocatorOptions { HasTextString = "Login" });
        await loginButton.ClickAsync();
        //  await Page.ClickAsync("#doLogin");
        await Page.WaitForSelectorAsync("#brandingLink");

        await Expect(Page.Locator("#brandingLink")).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions{Timeout = 10});

        await Page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = "screenshot2.jpg"
        });
    }
}