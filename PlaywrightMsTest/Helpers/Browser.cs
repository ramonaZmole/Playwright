using Microsoft.Playwright;

namespace PlaywrightMsTest.Helpers;

public static class Browser
{
    private static IBrowser? _browser;


    //  public Browser() => _page = Task.Run(InitializePlaywright);

    [ThreadStatic]
    public static IPage Page;


    public static async Task Dispose() => await _browser.CloseAsync();

    public static async Task InitializePlaywright()
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

        Page = await context.NewPageAsync();
        //   return await context.NewPageAsync();
    }



    public static async Task GoToAsync(string url) => await Page.GotoAsync(url);
}