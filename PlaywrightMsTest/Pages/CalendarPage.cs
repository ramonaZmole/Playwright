using Microsoft.Playwright;
using PlaywrightMsTest.Helpers;

namespace PlaywrightMsTest.Pages
{
    public class CalendarPage : BasePage
    {
        public async Task SelectDates()
        {
            var date = Browser.Page.Locator(".rbc-date-cell button ", new PageLocatorOptions { HasTextString = "17" }).First;
            await date.ClickAsync();
            await Browser.Page.Mouse.DownAsync();
            await Browser.Page.Mouse.MoveAsync(100, 200);
            await Browser.Page.Mouse.UpAsync();
        }
    }
}
