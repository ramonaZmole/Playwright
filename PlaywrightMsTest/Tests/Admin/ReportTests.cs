using FluentAssertions;
using PlaywrightMsTest.Helpers;
using PlaywrightMsTest.Helpers.Model.ApiModels;

namespace PlaywrightMsTest.Tests.Admin;

[TestClass]
public class ReportTests : BaseTest
{
    private CreateRoomOutput _createRoomOutput;
    private CreateBookingInput _bookingInput;

    [TestInitialize]
    public override async Task Before()
    {
        await base.Before();
        _createRoomOutput = await RequestContext.CreateRoom();

        _bookingInput = new CreateBookingInput
        {
            roomid = _createRoomOutput.roomid
        };
        await RequestContext.CreateBooking(_bookingInput);
    }

    [TestMethod]
    public async Task WhenBookingARoomDatePeriodShouldBeDisplayedTest()
    {
        await Browser.GoTo(Constants.AdminUrl);

        await LoginPage.Login();
        await AdminHeaderPage.GoToMenu(Helpers.Model.MenuItems.Report);

        var bookingName = $"{_bookingInput.firstname} {_bookingInput.lastname}";
        ReportPage.IsBookingDisplayed(bookingName, _createRoomOutput.roomName).Result.Should().BeTrue();
    }


    [TestCleanup]
    public override async Task After()
    {
        await base.After();
        var t = await RequestContext.Result.DeleteAsync($"{ApiResource.Room}{_createRoomOutput.roomid}");
    }
}