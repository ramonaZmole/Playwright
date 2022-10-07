using FluentAssertions;
using Newtonsoft.Json;
using PlaywrightMsTest.Helpers;
using PlaywrightMsTest.Helpers.Model.ApiModels;
using PlaywrightMsTest.Pages;

namespace PlaywrightMsTest.Tests.Admin;

[TestClass]
public class CreateRoomTests : BaseTest
{
    private readonly Helpers.Model.Room _roomModel = new();

    private readonly LoginPage _loginPage = new();
    private readonly RoomsPage _roomsPage = new();


    [TestMethod]
    public async Task WhenCreatingARoom_RoomShouldBeSavedTes()
    {
        await GoToAsync(Constants.AdminUrl);
        await _loginPage.Login();

        await _roomsPage.CreateRoom();
        var isErrorDisplayed = await _roomsPage.IsErrorMessageDisplayed();
        isErrorDisplayed.Should().BeTrue();

        var errorMessages = await _roomsPage.GetErrorMessages();
        errorMessages.Should().Contain("must be greater than or equal to 1");
        errorMessages.Should().Contain("Room name must be set");

        await _roomsPage.FillForm(_roomModel);
        await _roomsPage.CreateRoom();
        var roomDetails = await _roomsPage.GetLastCreatedRoomDetails();
        roomDetails.Should().BeEquivalentTo(_roomModel);
        //_roomsPage.GetLastCreatedRoomDetails().Result.Should().BeEquivalentTo(_roomModel); not working
    }

    [TestMethod]
    public async Task WhenCreatingRoomWithNoRoomDetails_NoFeaturesShouldBeDisplayedTest()
    {
        _roomModel.RoomDetails = string.Empty;

        await GoToAsync(Constants.AdminUrl);
        await _loginPage.Login();

        await _roomsPage.FillForm(_roomModel);
        await _roomsPage.CreateRoom();
        // RoomsPage.GetLastCreatedRoomDetails().Result.RoomDetails.Should().Be("No features added to the room");

        var roomDetails = await _roomsPage.GetLastCreatedRoomDetails();
        roomDetails.RoomDetails.Should().Be("No features added to the room");
    }


    [TestCleanup]
    public override async Task After()
    {
        await base.After();
        var response = await RequestContext.Result.GetAsync(ApiResource.Room).Result.JsonAsync();
        var rooms = JsonConvert.DeserializeObject<GetRoomsOutput>(response.ToString());

        var id = rooms.rooms.First(x => x.roomName == int.Parse(_roomModel.RoomName)).roomid;
        await RequestContext.Result.DeleteAsync($"{ApiResource.Room}{id}");
    }
}