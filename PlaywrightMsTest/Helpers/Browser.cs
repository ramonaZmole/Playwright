using Microsoft.Playwright;

namespace PlaywrightMsTest.Helpers
{
    public class Browser : IDisposable
    {
        private readonly Task<IPage> _page;
        private IBrowser? _browser;

        public Browser() => _page = Task.Run(InitializePlaywright);

        public IPage Page => _page.Result;

     //   public static readonly B = new();

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


        public void Dispose() => _browser?.CloseAsync();


    }
}
