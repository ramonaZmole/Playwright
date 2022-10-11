using Microsoft.Playwright;
using PlaywrightMsTest.Helpers;

namespace PlaywrightMsTest.Pages;

public class LoginPage
{
    private readonly IPage _page;

    private ILocator UsernameInput => _page.Locator("#username");
    private ILocator PasswordInput => _page.Locator("#password");
    private ILocator LoginButton => _page.Locator("#doLogin");


    public LoginPage(IPage page) => _page = page;


    public async Task Login()
    {
        await UsernameInput.FillAsync(Constants.Username);
        await PasswordInput.FillAsync(Constants.Password);
        await _page.RunAndWaitForResponseAsync(async () =>
       {
           await LoginButton.ClickAsync();
       }, x => x.Status == 200);
    }

}