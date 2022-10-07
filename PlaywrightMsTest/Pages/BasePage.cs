using Microsoft.Playwright;
using PlaywrightMsTest.Helpers;

namespace PlaywrightMsTest.Pages;

public class BasePage
{
    private static ILocator ErrorMessages => Page.Locator(".alert.alert-danger p");

    protected static IPage Page;

    public void SetPage(IPage page) => Page = page;

    public async Task<List<string?>> GetErrorMessages() => await ErrorMessages.GetLocatorsText();

    public async Task<bool> IsErrorMessageDisplayed()
    {
        await ErrorMessages.WaitForLocator();
        return await ErrorMessages.First.IsVisibleAsync();
    }
}
