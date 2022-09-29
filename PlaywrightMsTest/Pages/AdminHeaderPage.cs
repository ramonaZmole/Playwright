using Microsoft.Playwright;
using PlaywrightMsTest.Helpers.Model;

namespace PlaywrightMsTest.Pages;

public class AdminHeaderPage
{
    private readonly IPage _page;


    private ILocator MenuItems => _page.Locator(".mr-auto li a");

    public AdminHeaderPage(IPage page) => _page = page;

    public async Task GoToMenu(MenuItems menuItem)
    {
        for (var i = 0; i < await MenuItems.CountAsync(); i++)
        {
            var text = await MenuItems.Nth(i).TextContentAsync();
            if (text == menuItem.ToString())
                await MenuItems.Nth(i).ClickAsync();
            //     break;
        }
    }
}