using Microsoft.Playwright;
using PlaywrightMsTest.Helpers;
using PlaywrightMsTest.Helpers.Model;

namespace PlaywrightMsTest.Pages;

public class ReportPage : CalendarPage
{
    #region Selectors
    private ILocator FirstNameInput => Browser.Page.Locator("[name='firstname']");
    private ILocator LastNameInput => Browser.Page.Locator("[name='lastname']");
    private ILocator RoomDropdown => Browser.Page.Locator("#roomid");
    private ILocator DepositPaidDropdown => Browser.Page.Locator("#depositpaid");

    private ILocator BookButton => Browser.Page.Locator(".col-sm-12.text-right button:last-child");


    #endregion


    public async Task<bool> IsBookingDisplayed(string name, int roomName)
    {
        await Browser.Page.WaitForSelectorAsync($"[title='{name} - Room: {roomName}']");
        return await Browser.Page.Locator($"[title='{name} - Room: {roomName}']").Last.IsVisibleAsync();
    }

    public async Task InsertBookingDetails(User user, Room room)
    {
        await FirstNameInput.FillAsync(user.FirstName);
        await LastNameInput.FillAsync(user.LastName);
        await RoomDropdown.SelectOptionAsync(new SelectOptionValue { Label = room.RoomName });
        await DepositPaidDropdown.SelectOptionAsync("false");
    }

    public async Task Book() => await BookButton.ClickAsync();
}