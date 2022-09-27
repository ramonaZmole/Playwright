using Microsoft.Playwright;
using PlaywrightMsTest.Pages;


[assembly: Parallelize(Workers = 4, Scope = ExecutionScope.MethodLevel)]
namespace PlaywrightMsTest.Helpers;

public class BaseTest
{
    private IPage Page;

    public LoginPage LoginPage;
    public RoomsPage RoomsPage;


    [TestInitialize]
    public async Task Before()
    {
        Page = await Browser.InitializePlaywright(new BrowserTypeLaunchOptions
        {
            Headless = false
        });
        InitializePages();
    }


    [TestCleanup]
    public void After() => Browser.Dispose();


    public async Task GoTo(string url) => await Page.GotoAsync(url);


    private void InitializePages()
    {
        LoginPage = new LoginPage(Page);
        RoomsPage = new RoomsPage(Page);
    }
}