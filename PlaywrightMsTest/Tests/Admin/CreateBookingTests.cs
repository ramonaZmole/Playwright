using FluentAssertions;
using PlaywrightMsTest.Helpers;
using PlaywrightMsTest.Helpers.Model;
using PlaywrightMsTest.Helpers.Model.ApiModels;
using PlaywrightMsTest.Pages;
using Room = PlaywrightMsTest.Helpers.Model.Room;

namespace PlaywrightMsTest.Tests.Admin
{
    [TestClass]
    public class CreateBookingTests : BaseTest
    {
        private CreateRoomOutput _createRoomOutput;
        private readonly User _user = new();
        private Room _room;

        private readonly LoginPage _loginPage = new();
        private readonly ReportPage _reportPage = new();
        private readonly AdminHeaderPage _adminHeaderPage = new();

        [TestInitialize]
        public override async Task Before()
        {
            await base.Before();
            _createRoomOutput = await RequestContext.CreateRoom();
            _room = new Room { RoomName = _createRoomOutput.roomName.ToString() };
        }

        [TestMethod]
        public async Task WhenBookingARoom_BookingShouldBeDisplayedTest()
        {
            await Browser.GoToAsync(Constants.AdminUrl);

            await _loginPage.Login();
            await _adminHeaderPage.GoToMenu(Menu.Report);
            await _reportPage.SelectDates();
            await _reportPage.Book();

            var isErrorMessageDisplayed = await _reportPage.IsErrorMessageDisplayed();
            isErrorMessageDisplayed.Should().BeTrue();

            await _reportPage.InsertBookingDetails(_user, _room);
            await _reportPage.Book();

            var bookingName = $"{_user.FirstName} {_user.LastName}";
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
}
