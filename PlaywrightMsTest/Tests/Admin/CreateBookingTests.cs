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
            await Browser.GoTo(Constants.AdminUrl);

            await LoginPage.GetInstance().Login();
            await AdminHeaderPage.GetInstance().GoToMenu(Menu.Report);
            await ReportPage.GetInstance().SelectDates();
            //await ReportPage.GetInstance().Book();

            //var isErrorMessageDisplayed = await ReportPage.GetInstance().IsErrorMessageDisplayed();
            //isErrorMessageDisplayed.Should().BeTrue();

            //await ReportPage.GetInstance().InsertBookingDetails(_user, _room);
            //await ReportPage.GetInstance().Book();

            //var bookingName = $"{_user.FirstName} {_user.LastName}";
            //var displayed = await ReportPage.GetInstance().IsBookingDisplayed(bookingName, _createRoomOutput.roomName);
            //displayed.Should().BeTrue();
        }


        [TestCleanup]
        public override async Task After()
        {
            await base.After();
            await RequestContext.Result.DeleteAsync($"{ApiResource.Room}{_createRoomOutput.roomid}");
        }
    }
}
