using Microsoft.Playwright;
using PlaywrightMsTest.Helpers;
using PlaywrightMsTest.Helpers.Model;

namespace PlaywrightMsTest.Pages;

public class RoomsPage : WebPage<RoomsPage>
{

   // private readonly IPage _page;

    #region Selectors

    private ILocator CreateButton => _page.Locator("#createRoom");
    private ILocator RoomNumberInput => _page.Locator("#roomName");
    private ILocator TypeDropdown => _page.Locator("#type");
    private ILocator AccessibleDropdown => _page.Locator("#accessible");
    private ILocator RoomPriceInput => _page.Locator("#roomPrice");
    private ILocator LastRoomDetails => _page.Locator(".container .row.detail");

    #endregion

    //  public RoomsPage(IPage page) : base(page) => _page = page;


    public async Task CreateRoom()
    {
        await _page.RunAndWaitForResponseAsync(async () =>
        {
            await CreateButton.Click();
        }, x => x.Status is 200 or 400);
        await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await _page.WaitForLoadStateAsync(LoadState.Load);
        await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
    }

    public async Task FillForm(Room createRoomModel)
    {
        await RoomNumberInput.FillAsync(createRoomModel.RoomName);
        await TypeDropdown.SelectOptionAsync(createRoomModel.Type);
        await AccessibleDropdown.SelectOptionAsync(createRoomModel.Accessible);
        await RoomPriceInput.FillAsync(createRoomModel.Price);
        if (string.IsNullOrEmpty(createRoomModel.RoomDetails)) return;

        await _page.Locator(".form-check-label", new PageLocatorOptions { HasTextString = createRoomModel.RoomDetails }).ClickAsync();
    }

    public async Task<Room> GetLastCreatedRoomDetails()
    {
        await LastRoomDetails.WaitForLocator(WaitForSelectorState.Visible);
        await LastRoomDetails.First.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Attached, Timeout = 200 });

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
