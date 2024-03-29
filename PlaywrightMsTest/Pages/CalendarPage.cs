﻿using Microsoft.Playwright;

namespace PlaywrightMsTest.Pages
{
    public class CalendarPage : BasePage
    {
        private readonly IPage _page;

        public CalendarPage(IPage page) : base(page) => _page = page;


        public async Task SelectDates()
        {
            var date = _page.Locator(".rbc-date-cell button ", new PageLocatorOptions { HasTextString = "17" }).First;
            await date.ClickAsync();
            await _page.Mouse.DownAsync();
            await _page.Mouse.MoveAsync(100, 200);
            await _page.Mouse.UpAsync();
        }
    }
}
