using Microsoft.Playwright;

namespace PlaywrightMsTest.Helpers;

public static class Browser
{
    //[ThreadStatic]
    //public static IBrowser WebDriver;

    //public static void InitializeDriver(bool headless = false)
    //{
    //    WebDriver = headless == false ? Task.Run(() => GetBrowserAsync(headless: false)).Result
    //        : Task.Run(() => GetBrowserAsync(headless: true)).Result;
    //}

    //private static async Task<IBrowser> GetBrowserAsync(bool headless = false)
    //{
    //    var playwright = await Playwright.CreateAsync();

    //    IBrowser browser;

    //    if (headless)
    //        browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
    //    else
    //        browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false, SlowMo = 1000 });
    //    return browser;
    //}

    //public static async Task Dispose() => await WebDriver.CloseAsync();


    private static IBrowser? _browser;

    public static void Dispose() => _browser?.CloseAsync();

    public static async Task<IPage> InitializePlaywright(BrowserTypeLaunchOptions browserTypeLaunchOptions)
    {
        var playwright = await Playwright.CreateAsync();
        _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = browserTypeLaunchOptions.Headless
        });
        return await _browser.NewPageAsync();
    }

}