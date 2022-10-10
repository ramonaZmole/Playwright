using Microsoft.Playwright;
using PlaywrightMsTest.Helpers;

namespace PlaywrightMsTest.Pages;

public class LoginPage : BasePage
{
    private static ILocator UsernameInput => Browser.Page.Locator("#username");
    private static ILocator PasswordInput => Browser.Page.Locator("#password");
    private static ILocator LoginButton => Browser.Page.Locator("#doLogin");


    public async Task Login()
    {
        await UsernameInput.FillAsync(Constants.Username);
        await PasswordInput.FillAsync(Constants.Password);
        await Browser.Page.RunAndWaitForResponseAsync(async () =>
       {
           await LoginButton.ClickAsync();
       }, x => x.Status == 200);
    }

}