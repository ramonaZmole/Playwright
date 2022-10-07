using Microsoft.Playwright;

namespace PlaywrightMsTest.Pages
{
    public class CalendarPage : BasePage
    {
        public async Task SelectDates()
        {
            var date = Page.Locator(".rbc-date-cell button ", new PageLocatorOptions { HasTextString = "17" }).First;
            await date.ClickAsync();
            await Page.Mouse.DownAsync();
            await Page.Mouse.MoveAsync(100, 200);
            await Page.Mouse.UpAsync();
        }
    }
}
