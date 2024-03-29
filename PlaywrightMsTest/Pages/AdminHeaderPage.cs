﻿using Microsoft.Playwright;
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
        for (var i = 0; i < await MenuItems.CountAsync(); i++)
        {
            var text = await MenuItems.Nth(i).TextContentAsync();

            if (text == menuItem.ToString())
                await _page.RunAndWaitForResponseAsync(async () =>
                {
                    await MenuItems.Nth(i).ClickAsync();
                }, x => x.Status == 200);
        }

        //2
        //var menus = await MenuItems.GetElements();

        //foreach (var menu in menus)
        //{
        //    if (await menu.TextContentAsync() != menuItem.ToString()) continue;
        //    await menu.ClickAsync();
        //    await _page.WaitForResponseAsync(x => x.Status == 200);
        //}
    }
}