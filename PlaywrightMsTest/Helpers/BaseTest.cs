using Microsoft.Playwright;
using PlaywrightMsTest.Pages;


[assembly: Parallelize(Workers = 4, Scope = ExecutionScope.MethodLevel)]
namespace PlaywrightMsTest.Helpers;

public class BaseTest
{
    public LoginPage LoginPage;
    public RoomsPage RoomsPage;

    public Browser Browser = new();


    [TestInitialize]
    public void Before()
    {
        InitializePages();
    }

    [TestCleanup]
    public void After() => Browser.Dispose();

    private void InitializePages()
    {
        LoginPage = new LoginPage(Browser.Page);
        RoomsPage = new RoomsPage(Browser.Page);
    }
}