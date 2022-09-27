using Microsoft.Playwright;
using PlaywrightMsTest.Pages;


//[assembly: Parallelize(Workers = 4, Scope = ExecutionScope.MethodLevel)]
namespace PlaywrightMsTest.Helpers;

public class BaseTest
{
    private static IPage _page;


    public LoginPage LoginPage;
    public RoomsPage RoomsPage;


    [TestInitialize]
    public async Task Before()
    {
        Browser.InitializeDriver(true);
        _page = await Browser.WebDriver.NewPageAsync();
        //  _page = await Browser.InitializePage();
        //   await page.SetViewportSizeAsync(1920, 1080);
        InitializePages();
    }


    [TestCleanup]
    public async Task After() => await Browser.Dispose();


    public static async Task GoTo(string url) => await _page.GotoAsync(url);


    private void InitializePages()
    {
        LoginPage = new LoginPage(_page);
        RoomsPage = new RoomsPage(_page);
    }
}