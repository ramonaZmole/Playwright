using Microsoft.Playwright;
using PlaywrightMsTest.Helpers.Model.ApiModels;
using PlaywrightMsTest.Pages;


//[assembly: Parallelize(Workers = 4, Scope = ExecutionScope.MethodLevel)]
namespace PlaywrightMsTest.Helpers;

public class BaseTest
{
    public LoginPage LoginPage;
    public RoomsPage RoomsPage;
    public AdminHeaderPage AdminHeaderPage;
    public ReportPage ReportPage;

    public Browser Browser = new();

    public Task<IAPIRequestContext> RequestContext = ApiHelpers.GetRequestContext();


    [TestInitialize]
    public virtual async Task Before()
    {
        InitializePages();
        RequestContext = ApiHelpers.GetRequestContext(new Dictionary<string, string>
        {
            { "cookie", $"token={await GetLoginToken()}" }
        });
    }

    [TestCleanup]
    public virtual async Task After() => await Browser.Dispose();

    private void InitializePages()
    {
        LoginPage = new LoginPage(Browser.Page);
        RoomsPage = new RoomsPage(Browser.Page);
        AdminHeaderPage = new AdminHeaderPage(Browser.Page);
        ReportPage = new ReportPage(Browser.Page);
    }

    private async Task<string> GetLoginToken()
    {
        var response = await RequestContext.Result.PostAsync(ApiResource.Login, new APIRequestContextOptions { DataObject = new LoginInput() });
        var cookie = response.Headers.FirstOrDefault(x => x.Key.Contains("cookie")).Value;
        return cookie.Split("=")[1].Split(";")[0];
    }

}