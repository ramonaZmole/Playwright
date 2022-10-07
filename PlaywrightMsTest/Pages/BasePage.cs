using Microsoft.Playwright;
using PlaywrightMsTest.Helpers;

namespace PlaywrightMsTest.Pages;

public class BasePage
{
    private ILocator ErrorMessages => _page.Locator(".alert.alert-danger p");

    private readonly IPage _page;

    public BasePage(IPage page) => _page = page;


    public async Task<List<string?>> GetErrorMessages() => await ErrorMessages.GetLocatorsText();

    public async Task<bool> IsErrorMessageDisplayed()
    {
        await ErrorMessages.WaitForLocator();
        return await ErrorMessages.First.IsVisibleAsync();
    }
}
