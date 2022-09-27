using Microsoft.Playwright;

namespace PlaywrightMsTest.Helpers;

public class Browser
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


    private readonly Task<IPage> _page;
    private IBrowser? _browser;

    public Browser() => _page = Task.Run(InitializePlaywright);

    public IPage Page => _page.Result;

    public void Dispose() => _browser?.CloseAsync();

    private async Task<IPage> InitializePlaywright()
    {
        //Playwright
        var playwright = await Playwright.CreateAsync();
        //Browser
        _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });
        //Page
        return await _browser.NewPageAsync();
    }

    public async Task GoTo(string url) => await Page.GotoAsync(url);

}