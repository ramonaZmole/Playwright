using Microsoft.Playwright;
using PlaywrightMsTest.Helpers.Model;

namespace PlaywrightMsTest.Pages;

public class RoomsPage
{

    private readonly IPage _page;

    #region Selectors

    //  private ILocator CreateButton => _page.Locator("#createRoom");
    private const string CreateButton = "#createRoom";
    private ILocator RoomNumberInput => _page.Locator("#roomName");
    private ILocator TypeDropDown => _page.Locator("#type");
    private ILocator AccessibleDropDown => _page.Locator("#accessible");
    private ILocator RoomPriceInput => _page.Locator("#roomPrice");
    private ILocator ErrorMessage => _page.Locator(".alert ");

    private const string LastRoomDetails = "#root > div:nth-child(2) div:nth-last-child(2) .row.detail div p";

    #endregion

    public RoomsPage(IPage page) => _page = page;


    public async Task CreateRoom()
    {
        await _page.RunAndWaitForResponseAsync(async () =>
        {
            await _page.WaitForSelectorAsync(CreateButton);
            await _page.Locator(CreateButton).ClickAsync();
        }, x => x.Status is 200 or 400);
    }

    public async Task FillForm(CreateRoomModel createRoomModel)
    {
        await RoomNumberInput.FillAsync(createRoomModel.RoomName);
        await TypeDropDown.SelectOptionAsync(createRoomModel.Type);
        await AccessibleDropDown.SelectOptionAsync(createRoomModel.Accessible);
        await RoomPriceInput.FillAsync(createRoomModel.Price);
        if (string.IsNullOrEmpty(createRoomModel.RoomDetails)) return;

        await _page.Locator(".form-check-label", new PageLocatorOptions { HasTextString = createRoomModel.RoomDetails }).ClickAsync();
    }

    public async Task<CreateRoomModel> GetLastCreatedRoomDetails()
    {
        await _page.WaitForSelectorAsync(LastRoomDetails);
        var lastRoomDetails = _page.Locator(LastRoomDetails);

        var roomDetails = new List<string?>();

        for (var i = 0; i < await lastRoomDetails.CountAsync(); i++)
        {
            roomDetails.Add(await lastRoomDetails.Nth(i).TextContentAsync());
        }

        return new CreateRoomModel
        {
            RoomName = roomDetails[0] ?? string.Empty,
            Type = roomDetails[1] ?? string.Empty,
            Accessible = roomDetails[2] ?? string.Empty,
            Price = roomDetails[3] ?? string.Empty,
            RoomDetails = roomDetails[4] ?? string.Empty
        };
    }

    public async Task<bool> IsErrorMessageDisplayed()
    {
        var errorMessage = await ErrorMessage.TextContentAsync();

        return await ErrorMessage.IsVisibleAsync() &&
               errorMessage.Contains("must be greater than or equal to 1")
               && errorMessage.Contains("Room name must be set");
    }
}