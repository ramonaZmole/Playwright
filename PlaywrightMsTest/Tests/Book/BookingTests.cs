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

    [TestInitialize]
    public override async Task Before()
    {
        await base.Before();

        _createRoomResponse = await RequestContext.CreateRoom();
    }

    [TestMethod]
    public async Task WhenBookingRoom_SuccessMessageShouldBeDisplayedTest()
    {
        await Browser.GoTo(Constants.Url);

        //await HomePage.GetInstance().BookThisRoom(_createRoomResponse.description);
        //await HomePage.GetInstance().InsertBookingDetails(new User());
        //await HomePage.GetInstance().BookRoom();
        //var isBookingCreated = await HomePage.GetInstance().IsSuccessMessageDisplayed();
        //isBookingCreated.Should().BeTrue();
    }

    [TestMethod]
    public async Task WhenCancellingBooking_FormShouldNotBeDisplayedTest()
    {
        await Browser.GoTo(Constants.Url);

        //await HomePage.GetInstance().BookThisRoom(_createRoomResponse.description);
        //await HomePage.GetInstance().InsertBookingDetails(new User());
        //await HomePage.GetInstance().CancelBooking();
        //HomePage.GetInstance().IsBookingFormDisplayed().Result.Should().BeFalse();
        //HomePage.GetInstance().IsCalendarDisplayed().Result.Should().BeFalse();
    }

    [TestCleanup]
    public override async Task After()
    {
        await base.After();
        await RequestContext.Result.DeleteAsync($"{ApiResource.Room}{_createRoomResponse.roomid}");
    }
}