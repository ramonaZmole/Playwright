using FluentAssertions;
using PlaywrightMsTest.Helpers;
using PlaywrightMsTest.Helpers.Model;
using PlaywrightMsTest.Helpers.Model.ApiModels;
using PlaywrightMsTest.Pages;

namespace PlaywrightMsTest.Tests.Book;

[TestClass]
public class BookingTests : BaseTest
{
    private CreateRoomOutput _createRoomResponse;

    private readonly HomePage HomePage = new();


    [TestInitialize]
    public override async Task Before()
    {
        await base.Before();

        _createRoomResponse = await RequestContext.CreateRoom();
    }

    [TestMethod]
    public async Task WhenBookingRoom_SuccessMessageShouldBeDisplayedTest()
    {
        await GoToAsync(Constants.Url);

        await HomePage.BookThisRoom(_createRoomResponse.description);
        await HomePage.InsertBookingDetails(new User());
        await HomePage.BookRoom();
        var isBookingCreated = await HomePage.IsSuccessMessageDisplayed();
        isBookingCreated.Should().BeTrue();
    }

    [TestMethod]
    public async Task WhenCancellingBooking_FormShouldNotBeDisplayedTest()
    {
        await GoToAsync(Constants.Url);

        await HomePage.BookThisRoom(_createRoomResponse.description);
        await HomePage.InsertBookingDetails(new User());
        await HomePage.CancelBooking();
        HomePage.IsBookingFormDisplayed().Result.Should().BeFalse();
        HomePage.IsCalendarDisplayed().Result.Should().BeFalse();
    }

    [TestCleanup]
    public override async Task After()
    {
        await base.After();
        await RequestContext.Result.DeleteAsync($"{ApiResource.Room}{_createRoomResponse.roomid}");
    }
}