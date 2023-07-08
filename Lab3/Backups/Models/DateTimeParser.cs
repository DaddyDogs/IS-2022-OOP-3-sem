using System.Globalization;

namespace Backups.Models;

public class DateTimeParser
{
    public static string GetDateTime()
    {
        string dateTime = DateTime.Now.ToString("G");
        Thread.Sleep(1000);
        return dateTime.Replace('/', '_').Replace(':', '_').Replace(' ', '-');
    }
}