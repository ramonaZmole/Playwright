using Microsoft.Playwright;

namespace PlaywrightMsTest.Pages;

public class ReportPage
{
    private readonly IPage _page;

    public ReportPage(IPage page) => _page = page;

    public async Task<bool> IsBookingDisplayed(string name, int roomName)
    {
        await _page.WaitForSelectorAsync($"[title='{name} - Room: {roomName}']");
        return await _page.Locator($"[title='{name} - Room: {roomName}']").Last.IsVisibleAsync();
    }
}