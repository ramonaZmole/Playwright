namespace PlaywrightMsTest.Helpers.Model.ApiModels;

public class BookingDates
{
    public string checkin { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
    public string checkout { get; set; } = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
}

public class CreateBookingInput
{
    public BookingDates bookingdates { get; set; } = new();
    public bool depositpaid { get; set; } = Faker.Boolean.Random();
    public string firstname { get; set; } = Faker.Name.First();
    public string lastname { get; set; } = Faker.Name.Last();
    public int roomid { get; set; }
    public string email { get; set; } = "qqwwee@test.com";
    public string phone { get; set; } = "11111111111";

}