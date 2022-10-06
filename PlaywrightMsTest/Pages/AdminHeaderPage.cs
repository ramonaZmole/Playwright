using Microsoft.Playwright;
using PlaywrightMsTest.Helpers;
using PlaywrightMsTest.Helpers.Model;

namespace PlaywrightMsTest.Pages;

public class AdminHeaderPage
{
    private readonly IPage _page;


    private ILocator MenuItems => _page.Locator(".mr-auto li a");

    public AdminHeaderPage(IPage page) => _page = page;

    public async Task GoToMenu(Menu menuItem)
    {
        //1
        //  var menus = await MenuItems.GetElements();
        //  var resul = menus.AsQueryable();
        ////  await resul.FirstOrDefaultAsync(x => x.TextContentAsync().Equals(menuItem.ToString()));

        //   await menus.FirstOrDefault(x =>
        //   {
        //       var result = x.TextContentAsync().Result;
        //       return result != null && result.Equals(menuItem.ToString());
        //   })?.ClickAsync()!;

        //2
        for (var i = 0; i < await MenuItems.CountAsync(); i++)
        {
            var text = await MenuItems.Nth(i).TextContentAsync();

            if (text == menuItem.ToString())
                await _page.RunAndWaitForResponseAsync(async () =>
                {
                    await MenuItems.Nth(i).ClickAsync();
                }, x => x.Status == 200);
        }
    }
}