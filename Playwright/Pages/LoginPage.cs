namespace PlaywrightNUnit.Pages;

public class LoginPage
{
    public IPage Page { get; }

    public LoginPage(IPage page) => Page = page;

    private ILocator UsernameInput => Page.Locator("#username");
    private ILocator PasswordInput => Page.Locator("#password");
    private ILocator LoginButton => Page.Locator("#doLogin");



    public async Task Login()
    {
        await UsernameInput.FillAsync("admin");
        await PasswordInput.FillAsync("password");
        //  await Page.RunAndWaitForNavigationAsync(async () => { await LoginButton.ClickAsync(); });


        await Page.RunAndWaitForResponseAsync(async () =>
        {
            await LoginButton.ClickAsync();
        }, x => x.Status == 200);


        //   await LoginButton.ClickAsync();
        //   var wait = Page.WaitForResponseAsync("**/login");

        //   await Page.WaitForSelectorAsync("#brandingLink");
        //   var response = await wait;
    }
}