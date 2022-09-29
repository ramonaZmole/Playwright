using Microsoft.Playwright;
using PlaywrightMsTest.Helpers;

namespace PlaywrightMsTest.Pages;

public class ReportPage
{
    private readonly IPage _page;

    public ReportPage(IPage page) => _page = page;

    public async Task<bool> IsBookingDisplayed(string name, int roomName)
    {
        //  PageHelpers.ScrollDownToView(1000);
        var t = _page.Locator("title", new PageLocatorOptions { HasTextString = $"{name} - Room: {roomName}" }).First;
        return await _page.Locator("title", new PageLocatorOptions { HasTextString = $"{name} - Room: {roomName}" }).First
            .IsVisibleAsync();
    }
}