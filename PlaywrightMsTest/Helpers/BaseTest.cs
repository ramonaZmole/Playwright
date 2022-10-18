using Microsoft.Playwright;
using PlaywrightMsTest.Helpers.Model.ApiModels;
using PlaywrightMsTest.Pages;


[assembly: Parallelize(Workers = 4, Scope = ExecutionScope.MethodLevel)]
namespace PlaywrightMsTest.Helpers;

public class BaseTest
{
    //public LoginPage LoginPage;
    //public RoomsPage RoomsPage;
    //public AdminHeaderPage AdminHeaderPage;
    //public ReportPage ReportPage;
    //public HomePage HomePage;

  //  public Browser Browser = new();

    public Task<IAPIRequestContext> RequestContext = ApiHelpers.GetRequestContext();

    public TestContext TestContext { get; set; }

    [TestInitialize]
    public virtual async Task Before()
    {
        await Browser.StartBrowser();
      //  InitializePages();
        RequestContext = ApiHelpers.GetRequestContext(new Dictionary<string, string>
        {
            { "cookie", $"token={await GetLoginToken()}" }
        });
    }

    [TestCleanup]
    public virtual async Task After()
    {
        if (TestContext.CurrentTestOutcome.Equals(UnitTestOutcome.Failed))
        {
            var screenshotsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");

            var screenshotsPath = Path.Combine(screenshotsFolder, $"{TestContext.TestName}_{DateTime.Now:yyyyMMddHHmm}.png");
            await Browser.GetPage().ScreenshotAsync(new PageScreenshotOptions { Path = screenshotsPath, FullPage = true });
            TestContext.AddResultFile(screenshotsPath);
        }
        await Browser.StopBrowser();
    }

    private void InitializePages()
    {
        //LoginPage = new LoginPage();
        //RoomsPage = new RoomsPage();
        //AdminHeaderPage = new AdminHeaderPage();
        //ReportPage = new ReportPage();
        //HomePage = new HomePage();
    }

    private async Task<string> GetLoginToken()
    {
        var response = await RequestContext.Result.PostAsync(ApiResource.Login, new APIRequestContextOptions { DataObject = new LoginInput() });
        var cookie = response.Headers.FirstOrDefault(x => x.Key.Contains("cookie")).Value;
        return cookie.Split("=")[1].Split(";")[0];
    }

}