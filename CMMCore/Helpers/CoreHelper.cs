namespace Gotchi.Core.Helpers;

public static class CoreHelper
{
    public static string NewId() => Guid.NewGuid().ToString();

    public static int NumberOfMinutesPassed(DateTime since) => NumberOfMinutesPassed(since, DateTime.UtcNow);
    public static int NumberOfMinutesPassed(DateTime since, DateTime until)
    {
        int minutes = (int)until.Subtract(since).TotalMinutes;
        return (minutes <= 0) ? 0 : minutes;
    }

    public static int NumberOfHoursPassed(DateTime since) => NumberOfHoursPassed(since, DateTime.UtcNow);
    public static int NumberOfHoursPassed(DateTime since, DateTime until) 
    {
        int hours = (int) until.Subtract(since).TotalHours;
        return (hours <= 0) ? 0 : hours;
    }

    public static int NumberOfDaysPassed(DateTime since) => NumberOfDaysPassed(since, DateTime.UtcNow);
    public static int NumberOfDaysPassed(DateTime since, DateTime until)
    {
        int days = (int)until.Subtract(since).TotalDays;
        return (days <= 0) ? 0 : days;
    }

    public static string? CleanUserName(string? userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            return "";

        return userName.Trim();
    }

}
