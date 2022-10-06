using FluentAssertions;
using PlaywrightMsTest.Helpers;
using PlaywrightMsTest.Helpers.Model;
using PlaywrightMsTest.Helpers.Model.ApiModels;

namespace PlaywrightMsTest.Tests.Book;

[TestClass]
public class BookingFormTests : BaseTest
{
    private CreateRoomOutput _createRoomOutput;

    [TestInitialize]
    public override async Task Before()
    {
        await base.Before();

        _createRoomOutput = await RequestContext.CreateRoom();

        var bookingInput = new CreateBookingInput
        {
            roomid = _createRoomOutput.roomid
        };
        await RequestContext.CreateBooking(bookingInput);
    }

    [TestMethod]
    public async Task WhenBookingRoom_ErrorMessageShouldBeDisplayedTest()
    {
        await Browser.GoTo(Constants.Url);

        await HomePage.BookThisRoom(_createRoomOutput.description);
        await HomePage.BookRoom();
        var errorMessages = await HomePage.GetErrorMessages();
        errorMessages.Should().BeEquivalentTo(Constants.FormErrorMessages);

        await HomePage.InsertBookingDetails(new User());
        await HomePage.BookRoom();
        var alreadyBookedMessage = await HomePage.GetErrorMessages();
        alreadyBookedMessage.Should().BeEquivalentTo(Constants.AlreadyBookedErrorMessage);
        // HomePage.GetErrorMessages().Result[0].Should().Be(Constants.AlreadyBookedErrorMessage);
    }

    [TestCleanup]
    public override async Task After()
    {
        await base.After();
        await RequestContext.Result.DeleteAsync($"{ApiResource.Room}{_createRoomOutput.roomid}");
    }
}