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

    [TestMethod, Ignore]
    public async Task WhenBookingRoomErrorMessageShouldBeDisplayedTest()
    {
        await Browser.GoTo(Constants.Url);

        await HomePage.ClickBookThisRoom(_createRoomOutput.description);
        await HomePage.ClickBookRoom();
        var t = await HomePage.GetErrorMessages();
        t.Should().BeEquivalentTo(Constants.FormErrorMessages);

        await HomePage.CompleteBookingDetails(new UserModel());
        await HomePage.ClickBookRoom();
        HomePage.GetErrorMessages().Result[0].Should().Be(Constants.AlreadyBookedErrorMessage);
    }

    [TestCleanup]
    public override async Task After()
    {
        await base.After();
        await RequestContext.Result.DeleteAsync($"{ApiResource.Room}{_createRoomOutput.roomid}");
    }
}