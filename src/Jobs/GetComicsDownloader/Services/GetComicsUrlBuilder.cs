using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetComicsDownloader.Services
{
    public class GetComicsUrlBuilder
    {
        public static List<DateTime> GetAllWednesdaysBetweenDates(DateTime beginDate, DateTime endDate)
        {
            List<DateTime> datesToDownload = new List<DateTime>();
            DateTime mostRecentWednesday = endDate.Date;

            while (mostRecentWednesday.DayOfWeek != DayOfWeek.Wednesday)
            {
                mostRecentWednesday = mostRecentWednesday.AddDays(-1);
            }

            for (var date = mostRecentWednesday; date > beginDate; date = date.AddDays(-7))
            {
                datesToDownload.Add(date);
            }

            datesToDownload.Reverse();
            return datesToDownload;
        }

        public static string GenerateUrlFromDate(DateTime date)
        {
            return String.Format("https://getcomics.org/other-comics/{0}-{1}-{2}-weekly-pack/", date.Year, string.Format("{0:00}", date.Month), string.Format("{0:00}", date.Day));
        }
    }
}
