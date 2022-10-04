using FluentAssertions;
using Newtonsoft.Json;
using PlaywrightMsTest.Helpers;
using PlaywrightMsTest.Helpers.Model;
using PlaywrightMsTest.Helpers.Model.ApiModels;

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
        // RoomsPage.GetLastCreatedRoomDetails().Result.RoomDetails.Should().Be("No features added to the room");

        var roomDetails = await RoomsPage.GetLastCreatedRoomDetails();
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