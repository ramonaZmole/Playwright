using FluentAssertions;
using PlaywrightMsTest.Helpers;
using PlaywrightMsTest.Helpers.Model;
using PlaywrightMsTest.Helpers.Model.ApiModels;

namespace PlaywrightMsTest.Tests.Book;

[TestClass]
public class BookingTests : BaseTest
{
    private CreateRoomOutput _createRoomResponse;

    [TestInitialize]
    public override async Task Before()
    {
        await base.Before();

        _createRoomResponse = await RequestContext.CreateRoom();
    }

    [TestMethod]
    public async Task WhenBookingRoomSuccessMessageShouldBeDisplayedTest()
    {
        await Browser.GoTo(Constants.Url);

        await HomePage.ClickBookThisRoom(_createRoomResponse.description);
        await HomePage.CompleteBookingDetails(new UserModel());
        await HomePage.ClickBookRoom();
        HomePage.IsSuccessMessageDisplayed().Result.Should().BeTrue();
    }

    [TestMethod]
    public async Task WhenCancellingBookingFormShouldNotBeDisplayedTest()
    {
        await Browser.GoTo(Constants.Url);

        await HomePage.ClickBookThisRoom(_createRoomResponse.description);
        await HomePage.CompleteBookingDetails(new UserModel());
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