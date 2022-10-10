using Microsoft.Playwright;
using PlaywrightMsTest.Helpers.Model.ApiModels;
using PlaywrightMsTest.Pages;


//[assembly: Parallelize(Workers = 4, Scope = ExecutionScope.MethodLevel)]
namespace PlaywrightMsTest.Helpers;

public class BaseTest
{
    public Task<IAPIRequestContext> RequestContext = ApiHelpers.GetRequestContext();

    public TestContext TestContext { get; set; }
    //   private Browser b;



    [TestInitialize]
    public virtual async Task Before()
    {
        await Browser.InitializePlaywright();
        //   BasePage = new BasePage();
        //   BasePage.SetPage(Page);
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
            await Browser.Page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotsPath, FullPage = true });
            TestContext.AddResultFile(screenshotsPath);
        }
        await Browser.Dispose();
    }

    private async Task<string> GetLoginToken()
    {
        var response = await RequestContext.Result.PostAsync(ApiResource.Login, new APIRequestContextOptions { DataObject = new LoginInput() });
        var cookie = response.Headers.FirstOrDefault(x => x.Key.Contains("cookie")).Value;
        return cookie.Split("=")[1].Split(";")[0];
    }

    public async Task GoToAsync(string url)
    {
        await Browser.Page.GotoAsync(url);
    }

}