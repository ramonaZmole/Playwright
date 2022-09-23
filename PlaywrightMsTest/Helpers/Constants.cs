namespace PlaywrightMsTest.Helpers;

public class Constants
{
    public static string Url = "https://automationintesting.online/";
    public static string AdminUrl = "https://automationintesting.online/#/admin";

    public static string Username = "admin";
    public static string Password = "password";

    public static string BookingStartDay = "10";
    public static string BookingEndDay = "13";

    public static string AlreadyBookedErrorMessage = "The room dates are either invalid or are already booked for one or more of the dates that you have selected.";
    public static List<string> FormErrorMessages = new()
    {
        "size must be between 3 and 30",
        "must not be null", "must not be empty",
        "size must be between 3 and 18", "must not be null",
        "Lastname should not be blank","Firstname should not be blank",
        "size must be between 11 and 21", "must not be empty"
    };
}