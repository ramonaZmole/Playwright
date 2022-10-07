using Microsoft.Playwright;
using PlaywrightMsTest.Helpers;
using PlaywrightMsTest.Helpers.Model;

namespace PlaywrightMsTest.Pages;

public class RoomsPage : BasePage
{

    #region Selectors

    private ILocator CreateButton => Page.Locator("#createRoom");
    private ILocator RoomNumberInput => Page.Locator("#roomName");
    private ILocator TypeDropdown => Page.Locator("#type");
    private ILocator AccessibleDropdown => Page.Locator("#accessible");
    private ILocator RoomPriceInput => Page.Locator("#roomPrice");
    private ILocator LastRoomDetails => Page.Locator(".container .row.detail");

    #endregion

    public async Task CreateRoom()
    {
        await Page.RunAndWaitForResponseAsync(async () =>
        {
            await CreateButton.Click();
        }, x => x.Status is 200 or 400);
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await Page.WaitForLoadStateAsync(LoadState.Load);
        await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
    }

    public async Task FillForm(Room createRoomModel)
    {
        await RoomNumberInput.FillAsync(createRoomModel.RoomName);
        await TypeDropdown.SelectOptionAsync(createRoomModel.Type);
        await AccessibleDropdown.SelectOptionAsync(createRoomModel.Accessible);
        await RoomPriceInput.FillAsync(createRoomModel.Price);
        if (string.IsNullOrEmpty(createRoomModel.RoomDetails)) return;

        await Page.Locator(".form-check-label", new PageLocatorOptions { HasTextString = createRoomModel.RoomDetails }).ClickAsync();
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
