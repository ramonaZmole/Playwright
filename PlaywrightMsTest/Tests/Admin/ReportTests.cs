using FluentAssertions;
using PlaywrightMsTest.Helpers;
using PlaywrightMsTest.Helpers.Model.ApiModels;
using PlaywrightMsTest.Pages;

namespace PlaywrightMsTest.Tests.Admin;

[TestClass]
public class ReportTests : BaseTest
{
    private CreateRoomOutput _createRoomOutput;
    private CreateBookingInput _bookingInput;


    private readonly LoginPage _loginPage = new();
    private readonly AdminHeaderPage _adminHeaderPage = new();
    private readonly ReportPage _reportPage = new();

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
    public async Task WhenViewingReports_BookedRoomsShouldBeDisplayedTest()
    {
        await GoToAsync(Constants.AdminUrl);

        await _loginPage.Login();
        await _adminHeaderPage.GoToMenu(Helpers.Model.Menu.Report);

        var bookingName = $"{_bookingInput.firstname} {_bookingInput.lastname}";
        var displayed = await _reportPage.IsBookingDisplayed(bookingName, _createRoomOutput.roomName);
        displayed.Should().BeTrue();
    }


    [TestCleanup]
    public override async Task After()
    {
        await base.After();
        await RequestContext.Result.DeleteAsync($"{ApiResource.Room}{_createRoomOutput.roomid}");
    }
}