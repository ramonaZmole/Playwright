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
    private string BookRoomButton1 = ".btn-outline-primary.book-room";
    private ILocator CancelBookingButton => _page.Locator(".btn-outline-danger");
    private ILocator Calendar => _page.Locator(".rbc-calendar");

    private ILocator SuccessMessage => _page.Locator(".col-sm-6.text-center > h3");
    private ILocator BookRoomButtons => _page.Locator(".openBooking");

    // private ILocator ErrorMessages => _page.Locator(".alert.alert-danger p");
    private string ErrorMessages = ".alert.alert-danger p";
    #endregion

    public HomePage(IPage page) => _page = page;


    public async Task ClickBookRoom()
    {
        await _page.RunAndWaitForResponseAsync(async () =>
        {
            await _page.WaitForSelectorAsync(BookRoomButton1);
            await _page.Locator(BookRoomButton1).ClickAsync();
            // await CreateButton.ClickAsync();
        }, x => x.Status is 200 or 400);
        //  await BookRoomButton.ClickAsync();
    }

    public async Task CancelBooking() => await CancelBookingButton.ClickAsync();

    internal async Task CompleteBookingDetails(UserModel userModel)
    {
        await FirstNameInput.FillAsync(userModel.FirstName);
        await LastNameInput.FillAsync(userModel.LastName);
        await EmailInput.FillAsync(userModel.Email);
        await PhoneInput.FillAsync(userModel.ContactPhone);

        SelectDates();
    }

    public async Task ClickBookThisRoom(string roomDescription)
    {
        var descriptions = await GetElements(Descriptions);
        var tt = descriptions.First(x => x.TextContentAsync().Result == roomDescription);

        var index = descriptions.IndexOf(descriptions.First(x => x.TextContentAsync().Result == roomDescription));
        var t = _page.Locator($":nth-match(:text('Book this room'), {index + 1})");
        //  var ttl = t[index];
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
        var end = _page.Locator(".rbc-date-cell button",
            new PageLocatorOptions { HasTextString = Constants.BookingEndDay });
        //var t = _page.Locator(".rbc-date-cell button",
        //    new PageLocatorOptions { HasTextString = Constants.BookingStartDay }).;


        await _page.Mouse.ClickAsync(2, 2);

        var tsel = _page.Locator(".rbc-date-cell button ", new PageLocatorOptions { HasTextString = Constants.BookingStartDay });
        //  await tsel.HoverAsync();
        await tsel.ClickAsync();
        await _page.Mouse.DownAsync();
        await _page.Mouse.MoveAsync(10, 10);
        await _page.Mouse.UpAsync();
        //await _page.DragAndDropAsync(".rbc-date-cell button ",
        //    $"//*[text()={Constants.BookingEndDay}] ");
    }

    public async Task<bool> IsSuccessMessageDisplayed()
    {
        //_successMessage;
        var t = await SuccessMessage.TextContentAsync();
        return t.Equals("Booking Successful!");
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
        var errorMessages = _page.Locator(ErrorMessages);
        //  WaitHelpers.ExplicitWait();
        var list = new List<string?>();
        // var t = await GetElements(ErrorMessages);

        for (var i = 0; i < await errorMessages.CountAsync(); i++)
        {
            list.Add(await errorMessages.Nth(i).TextContentAsync());
        }

        return list;
    }
}