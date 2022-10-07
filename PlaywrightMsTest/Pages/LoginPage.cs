using Microsoft.Playwright;
using PlaywrightMsTest.Helpers;

namespace PlaywrightMsTest.Pages;

public class LoginPage : BasePage
{
    private static ILocator UsernameInput => Page.Locator("#username");
    private static ILocator PasswordInput => Page.Locator("#password");
    private static ILocator LoginButton => Page.Locator("#doLogin");


    public async Task Login()
    {
        await UsernameInput.FillAsync(Constants.Username);
        await PasswordInput.FillAsync(Constants.Password);
        await Page.RunAndWaitForResponseAsync(async () =>
       {
           await LoginButton.ClickAsync();
       }, x => x.Status == 200);
    }

}