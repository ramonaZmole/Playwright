using Microsoft.Playwright;

namespace PlaywrightMsTest.Helpers;

public class Browser
{
    private static IBrowser? _browser;

    public static async Task Dispose() => await _browser.CloseAsync();

    public static async Task<IPage> InitializePlaywright()
    {
        var playwright = await Playwright.CreateAsync();


        _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true,
            Args = new[] { "--start-maximized" }
        });
        var context = await _browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = ViewportSize.NoViewport
        });

        return await context.NewPageAsync();
    }

    // public async Task GoToAsync(string url) => await Page.GotoAsync(url);
}