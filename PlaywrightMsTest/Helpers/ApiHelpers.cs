using Microsoft.Playwright;
using Newtonsoft.Json;
using PlaywrightMsTest.Helpers.Model.ApiModels;

namespace PlaywrightMsTest.Helpers
{
    internal static class ApiHelpers
    {
        public static async Task<IAPIRequestContext> GetRequestContext(Dictionary<string, string>? headerDictionary = null)
        {
            var playwright = await Playwright.CreateAsync();

            return await playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions
            {
                BaseURL = Constants.Url,
                ExtraHTTPHeaders = headerDictionary
            });
        }

        public static async Task<CreateRoomOutput> CreateRoom(this Task<IAPIRequestContext> requestContext)
        {
            var roomResponse = await requestContext.Result.PostAsync(ApiResource.Room, new APIRequestContextOptions { DataObject = new CreateRoomInput() }).Result.JsonAsync();
            return JsonConvert.DeserializeObject<CreateRoomOutput>(roomResponse.ToString());
        }


        public static async Task CreateBooking(this Task<IAPIRequestContext> requestContext, CreateBookingInput createBookingInput)
        {
            createBookingInput.bookingdates.checkin = createBookingInput.bookingdates.checkin.Remove(8, 2).Insert(8, Constants.BookingStartDay);
            createBookingInput.bookingdates.checkout = createBookingInput.bookingdates.checkout.Remove(8, 2).Insert(8, Constants.BookingEndDay);
            await requestContext.Result.PostAsync(ApiResource.Booking, new APIRequestContextOptions { DataObject = createBookingInput });
        }
    }
}
