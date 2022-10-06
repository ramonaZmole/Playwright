using Microsoft.Playwright;
using PlaywrightMsTest.Helpers;
using PlaywrightMsTest.Helpers.Model;

namespace PlaywrightMsTest.Pages;

public class HomePage
{
    private readonly IPage _page;

    #region Selectors

    private ILocator Descriptions => _page.Locator(".row.hotel-room-info p");

    private ILocator FirstNameInput => _page.Locator(".room-firstname");
    private ILocator LastNameInput => _page.Locator(".room-lastname");
    private ILocator EmailInput => _page.Locator(".room-email");
    private ILocator PhoneInput => _page.Locator(".room-phone");

    private ILocator BookRoomButton => _page.Locator(".btn-outline-primary.book-room");
    private ILocator CancelBookingButton => _page.Locator(".btn-outline-danger");
    private ILocator Calendar => _page.Locator(".rbc-calendar");
    private ILocator SuccessMessage => _page.Locator("text=Booking Successful!");

    private ILocator ErrorMessages => _page.Locator(".alert.alert-danger p");
    #endregion

    public HomePage(IPage page) => _page = page;


    public async Task BookRoom()
    {
        await _page.RunAndWaitForResponseAsync(async () =>
        {
            await BookRoomButton.WaitForAsync();
            await BookRoomButton.ClickAsync();
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
        var descriptions = await GetElements(Descriptions);
        var index = descriptions.IndexOf(descriptions.First(x => x.TextContentAsync().Result == roomDescription));
        var t = _page.Locator($":nth-match(:text('Book this room'), {index + 1})");
        await t.ClickAsync();
    }


    private async Task<List<ILocator>> GetElements(ILocator locator)
    {
        var elements = new List<ILocator>();
        for (var i = 0; i < await locator.CountAsync(); i++)
        {
            elements.Add(Descriptions.Nth(i));
        }

        return elements;
    }

    private async Task SelectDates()
    {
        //var actions = new Actions(Browser.WebDriver);

        //actions.ClickAndHold(Browser.WebDriver.FindElement(By.XPath($"//*[text()={Constants.BookingStartDay}] ")))
        //    .MoveByOffset(10, 10)
        //    .Release(Browser.WebDriver.FindElement(By.XPath($"//*[text()={Constants.BookingEndDay}] ")))
        //    .Build()
        //    .Perform();
        //var end = _page.Locator(".rbc-date-cell button",
        //    new PageLocatorOptions { HasTextString = Constants.BookingEndDay });
        //var t = _page.Locator(".rbc-date-cell button",
        //    new PageLocatorOptions { HasTextString = Constants.BookingStartDay }).;


        //   await _page.Mouse.ClickAsync(2, 2);

        var tsel = _page.Locator(".rbc-date-cell button ", new PageLocatorOptions { HasTextString = "17" }).First;
        await tsel.ClickAsync();
        //    await _page.Mouse.MoveAsync(200, 100);
        await _page.Mouse.DownAsync();
        await _page.Mouse.MoveAsync(100, 200);
        await _page.Mouse.UpAsync();
    }

    public async Task<bool> IsSuccessMessageDisplayed()
    {
        await SuccessMessage.WaitForAsync(new LocatorWaitForOptions
        {
            Timeout = 100,
            State = WaitForSelectorState.Attached
        });
        return await SuccessMessage.IsVisibleAsync();
    }


    public async Task<bool> IsBookingFormDisplayed()
    {
        return await FirstNameInput.IsVisibleAsync()
               && await LastNameInput.IsVisibleAsync()
               && await EmailInput.IsVisibleAsync()
               && await PhoneInput.IsVisibleAsync()
               && await BookRoomButton.IsVisibleAsync()
               && await CancelBookingButton.IsVisibleAsync();
    }

    public async Task<bool> IsCalendarDisplayed()
    {
        return await Calendar.IsVisibleAsync();
    }

    public async Task<List<string?>> GetErrorMessages()
    {
        await ErrorMessages.First.WaitForAsync(new LocatorWaitForOptions { Timeout = 100 });
        // await _page.WaitForSelectorAsync(ErrorMessages);
        // await _page.Locator(ErrorMessages).First.WaitForAsync(new LocatorWaitForOptions { Timeout = 100 });
        //       var errorMessages = _page.Locator(ErrorMessages);

        var list = new List<string?>();

        for (var i = 0; i < await ErrorMessages.CountAsync(); i++)
        {
            list.Add(await ErrorMessages.Nth(i).TextContentAsync());
        }

        return list;
    }
}