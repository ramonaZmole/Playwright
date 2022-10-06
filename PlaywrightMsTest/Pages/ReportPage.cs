using Microsoft.Playwright;
using PlaywrightMsTest.Helpers.Model;

namespace PlaywrightMsTest.Pages;

public class ReportPage : CalendarPage
{
    #region Selectors
    private ILocator FirstNameInput => _page.Locator("[name='firstname']");
    private ILocator LastNameInput => _page.Locator("[name='lastname']");
    private ILocator RoomDropdown => _page.Locator("#roomid");
    private ILocator DepositPaidDropdown => _page.Locator("#depositpaid");

    private ILocator BookButton => _page.Locator(".col-sm-12.text-right button:last-child");


    #endregion



    private readonly IPage _page;

    public ReportPage(IPage page) : base(page) => _page = page;

    public async Task<bool> IsBookingDisplayed(string name, int roomName)
    {
        await _page.WaitForSelectorAsync($"[title='{name} - Room: {roomName}']");
        return await _page.Locator($"[title='{name} - Room: {roomName}']").Last.IsVisibleAsync();
    }

    public async Task BookRoom(User user, Room room)
    {
        await SelectDates();

        await FirstNameInput.FillAsync(user.FirstName);
        await LastNameInput.FillAsync(user.LastName);
        await RoomDropdown.SelectOptionAsync(new SelectOptionValue { Label = room.RoomName });
        await DepositPaidDropdown.SelectOptionAsync("false");
        await BookButton.ClickAsync();
    }
}