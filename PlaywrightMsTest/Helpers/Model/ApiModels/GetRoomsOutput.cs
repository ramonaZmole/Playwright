namespace PlaywrightMsTest.Helpers.Model.ApiModels;

public class Room
{
    public int roomid { get; set; }
    public int roomName { get; set; }
    public string type { get; set; }
    public bool accessible { get; set; }
    public string image { get; set; }
    public string description { get; set; }
    public List<string> features { get; set; }
    public int roomPrice { get; set; }
}

public class GetRoomsOutput
{
    public List<Room> rooms { get; set; }
}