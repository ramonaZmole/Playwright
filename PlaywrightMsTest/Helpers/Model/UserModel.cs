namespace PlaywrightMsTest.Helpers.Model;

public class UserModel
{
    public string FirstName { get; set; } = Faker.Name.First();
    public string LastName { get; set; } = Faker.Name.Last();
    public string Email { get; set; } = Faker.Internet.Email();
    public string ContactPhone { get; set; } = "89123749157";
}