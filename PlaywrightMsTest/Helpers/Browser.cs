using Microsoft.Playwright;

namespace PlaywrightMsTest.Helpers;

public class Browser
{
    private readonly Task<IPage> _page;
    private IBrowser? _browser;

    public Browser() => _page = Task.Run(InitializePlaywright);

    public IPage Page => _page.Result;

    public async Task Dispose() => await _browser.CloseAsync();

    private async Task<IPage> InitializePlaywright()
    {
        var playwright = await Playwright.CreateAsync();
        _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true,
          //  SlowMo = 100
        });
        return await _browser.NewPageAsync();
    }

    public async Task GoTo(string url) => await Page.GotoAsync(url);
}