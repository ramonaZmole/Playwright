namespace PlaywrightMsTest.Helpers.Model;

public class CreateRoomModel
{
    public string RoomName { get; set; } = Faker.RandomNumber.Next(0, 1000).ToString();
    public string Type { get; set; } = GetRoomType();
    public string Accessible { get; set; } = Faker.Boolean.Random().ToString().ToLower();
    public string Price { get; set; } = Faker.RandomNumber.Next(0, 999).ToString();
    public string RoomDetails { get; set; } = GetRoomDetails();


    private static string GetRoomDetails()
    {
        var roomDetails = new List<string> { "WiFi", "TV", "Radio", "Refreshments", "Safe", "Views" };

        return roomDetails[Faker.RandomNumber.Next(roomDetails.Count - 1)];
    }

    private static string GetRoomType()
    {
        var roomType = new List<string> { "Single", "Twin", "Double", "Family", "Suite" };

        return roomType[Faker.RandomNumber.Next(roomType.Count - 1)];
    }
}