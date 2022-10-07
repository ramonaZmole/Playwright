using Microsoft.Playwright;
using PlaywrightMsTest.Helpers.Model;

namespace PlaywrightMsTest.Pages;

public class AdminHeaderPage : BasePage
{
    private ILocator MenuItems => Page.Locator(".mr-auto li a");


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
                await Page.RunAndWaitForResponseAsync(async () =>
                {
                    await MenuItems.Nth(i).ClickAsync();
                }, x => x.Status == 200);
        }
    }
}