using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
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
        await LoginButton.ClickAsync();
    }

}