using Microsoft.Playwright;
using PlaywrightMsTest.Helpers;
using PlaywrightMsTest.Helpers.Model;

namespace PlaywrightMsTest.Pages;

public class RoomsPage : BasePage
{

    #region Selectors

    private ILocator CreateButton => Browser.Page.Locator("#createRoom");
    private ILocator RoomNumberInput => Browser.Page.Locator("#roomName");
    private ILocator TypeDropdown => Browser.Page.Locator("#type");
    private ILocator AccessibleDropdown => Browser.Page.Locator("#accessible");
    private ILocator RoomPriceInput => Browser.Page.Locator("#roomPrice");
    private ILocator LastRoomDetails => Browser.Page.Locator(".container .row.detail");

    #endregion

    public async Task CreateRoom()
    {
        await Browser.Page.RunAndWaitForResponseAsync(async () =>
        {
            await CreateButton.Click();
        }, x => x.Status is 200 or 400);
        await Browser.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await Browser.Page.WaitForLoadStateAsync(LoadState.Load);
        await Browser.Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
    }

    public async Task FillForm(Room createRoomModel)
    {
        await RoomNumberInput.FillAsync(createRoomModel.RoomName);
        await TypeDropdown.SelectOptionAsync(createRoomModel.Type);
        await AccessibleDropdown.SelectOptionAsync(createRoomModel.Accessible);
        await RoomPriceInput.FillAsync(createRoomModel.Price);
        if (string.IsNullOrEmpty(createRoomModel.RoomDetails)) return;

        await Browser.Page.Locator(".form-check-label", new PageLocatorOptions { HasTextString = createRoomModel.RoomDetails }).ClickAsync();
    }

    public async Task<Room> GetLastCreatedRoomDetails()
    {
        await LastRoomDetails.WaitForLocator(WaitForSelectorState.Visible);
        await LastRoomDetails.First.WaitForAsync();

        var lastRoomDetails = LastRoomDetails.Last.Locator("p");
        var roomDetails = await lastRoomDetails.GetLocatorsText();

        return new Room
        {
            RoomName = roomDetails[0] ?? string.Empty,
            Type = roomDetails[1] ?? string.Empty,
            Accessible = roomDetails[2] ?? string.Empty,
            Price = roomDetails[3] ?? string.Empty,
            RoomDetails = roomDetails[4] ?? string.Empty
        };
    }
}
