using FluentAssertions;
using PlaywrightMsTest.Helpers;
using PlaywrightMsTest.Helpers.Model;

namespace PlaywrightMsTest.Tests.Admin;

[TestClass]
public class CreateRoomTests : BaseTest
{
    private readonly CreateRoomModel _roomModel = new();


    [TestMethod]
    public async Task WhenCreatingARoom_RoomShouldBeSavedTes()
    {
        await Browser.GoTo(Constants.AdminUrl);
        await LoginPage.Login();

        await RoomsPage.CreateRoom();
        RoomsPage.IsErrorMessageDisplayed().Result.Should().BeTrue();

        await RoomsPage.FillForm(_roomModel);
        await RoomsPage.CreateRoom();
        var roomDetails = await RoomsPage.GetLastCreatedRoomDetails();
        roomDetails.Should().BeEquivalentTo(_roomModel);
        //_roomsPage.GetLastCreatedRoomDetails().Result.Should().BeEquivalentTo(_roomModel); not working
    }

    [TestMethod]
    public async Task WhenCreatingRoomWithNoRoomDetails_NoFeaturesShouldBeDisplayedTest()
    {
        _roomModel.RoomDetails = string.Empty;

        await Browser.GoTo(Constants.AdminUrl);
        await LoginPage.Login();

        await RoomsPage.FillForm(_roomModel);
        await RoomsPage.CreateRoom();
        //  _roomsPage.GetLastCreatedRoomDetails().Result.RoomDetails.Should().Be("No features added to the room");

        var roomDetails = await RoomsPage.GetLastCreatedRoomDetails();
        roomDetails.RoomDetails.Should().Be("No features added to the room");
    }
}