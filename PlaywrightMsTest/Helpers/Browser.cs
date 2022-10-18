using Microsoft.Playwright;

namespace PlaywrightMsTest.Helpers;

public static class Browser
{
   // [ThreadStatic]
    private static IPage _page;
    //[ThreadStatic]
    private static IBrowser? _browser;

    public static async Task SetPage(IBrowserContext context)
    {
        _page = await context.NewPageAsync();
    }

    public static IPage GetPage()
    {
        if (_page == null)
        {
            throw new NullReferenceException("The Pager instance was not initialized. You should first call the method Start.");
        }
        return _page;
    }

    public static IBrowser GetBrowser()
    {
        if (_browser == null)
        {
            throw new NullReferenceException("The browser instance was not initialized. You should first call the method Start.");
        }
        return _browser;
    }

    private static async Task<IBrowserContext> SetBrowser()
    {
        var playwright = await Playwright.CreateAsync();

        _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true,
            Args = new[] { "--start-maximized" }
        });
        return await _browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = ViewportSize.NoViewport
        });

    }

    public static async Task StartBrowser()
    {
        var browserContext = await SetBrowser();
        await SetPage(browserContext);
    }

    public static async Task StopBrowser()
    {
        await GetBrowser().DisposeAsync();
    }

    public static async Task GoTo(string url) => await GetPage().GotoAsync(url);
}