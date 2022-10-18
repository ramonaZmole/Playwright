using Microsoft.Playwright;
using PlaywrightMsTest.Helpers;
using PlaywrightMsTest.Helpers.Model;

namespace PlaywrightMsTest.Pages;

public class HomePage : CalendarPage
{
  //  private readonly IPage _page;

    #region Selectors

    private ILocator Descriptions => _page.Locator(".row.hotel-room-info p");
    private ILocator BookThisRoomButtons => _page.Locator(".row.hotel-room-info button");

    private ILocator FirstNameInput => _page.Locator(".room-firstname");
    private ILocator LastNameInput => _page.Locator(".room-lastname");
    private ILocator EmailInput => _page.Locator(".room-email");
    private ILocator PhoneInput => _page.Locator(".room-phone");

    private ILocator BookRoomButton => _page.Locator(".btn-outline-primary.book-room");
    private ILocator CancelBookingButton => _page.Locator(".btn-outline-danger");
    private ILocator Calendar => _page.Locator(".rbc-calendar");
    private ILocator SuccessMessage => _page.Locator("text=Booking Successful!");

    #endregion

  //  public HomePage(IPage page) : base(page) => _page = page;


    public async Task BookRoom()
    {
        await _page.RunAndWaitForResponseAsync(async () =>
        {
            await BookRoomButton.Click();
        }, x => x.Status is 200 or 400 or 201 or 409);
    }

    public async Task CancelBooking() => await CancelBookingButton.ClickAsync();

    internal async Task InsertBookingDetails(User user)
    {
        await FirstNameInput.FillAsync(user.FirstName);
        await LastNameInput.FillAsync(user.LastName);
        await EmailInput.FillAsync(user.Email);
        await PhoneInput.FillAsync(user.ContactPhone);

        await SelectDates();
    }



    public async Task BookThisRoom(string roomDescription)
    {
        var descriptions = await Descriptions.GetElements();
        var bookButtons = await BookThisRoomButtons.GetElements();

        var index = descriptions.IndexOf(descriptions.First(x => x.TextContentAsync().Result == roomDescription));
        await bookButtons[index].ClickAsync();
        // await _page.Locator($":nth-match(:text('Book this room'), {index + 1})").ClickAsync();
    }

    public async Task<bool> IsSuccessMessageDisplayed() => await SuccessMessage.IsVisible();

    public async Task<bool> IsBookingFormDisplayed()
    {
        return await FirstNameInput.IsVisibleAsync()
               && await LastNameInput.IsVisibleAsync()
               && await EmailInput.IsVisibleAsync()
               && await PhoneInput.IsVisibleAsync()
               && await BookRoomButton.IsVisibleAsync()
               && await CancelBookingButton.IsVisibleAsync();
    }

    public async Task<bool> IsCalendarDisplayed() => await Calendar.IsVisibleAsync();

}