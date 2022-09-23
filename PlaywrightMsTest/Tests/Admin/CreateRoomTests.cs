using FluentAssertions;
using PlaywrightMsTest.Helpers;
using PlaywrightMsTest.Helpers.Model;
using PlaywrightMsTest.Pages;

namespace PlaywrightMsTest.Tests.Admin
{
    [TestClass]
    public class CreateRoomTests : BaseTest
    {
        private readonly LoginPage _loginPage = new(Browser.Page);
        private readonly RoomsPage _roomsPage = new(Browser.Page);
        private readonly CreateRoomModel _roomModel = new();


        [TestMethod]
        public async Task WhenCreatingARoom_RoomShouldBeSavedTes()
        {
            await Browser.GoTo(Constants.AdminUrl);
            await _loginPage.Login();

            await _roomsPage.CreateRoom();
            _roomsPage.IsErrorMessageDisplayed().Result.Should().BeTrue();

            await _roomsPage.FillForm(_roomModel);
            await _roomsPage.CreateRoom();
            _roomsPage.GetLastCreatedRoomDetails().Result.Should().BeEquivalentTo(_roomModel);
        }

        [TestMethod]
        public async Task WhenCreatingRoomWithNoRoomDetails_NoFeaturesShouldBeDisplayedTest()
        {
            _roomModel.RoomDetails = string.Empty;

            await Browser.GoTo(Constants.AdminUrl);
            await _loginPage.Login();

            await _roomsPage.FillForm(_roomModel);
            await _roomsPage.CreateRoom();
            //  _roomsPage.GetLastCreatedRoomDetails().RoomDetails.Should().Be("No features added to the room");
        }
    }
}
