using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Windows.Globalization.DateTimeFormatting;

namespace MainWindow.Models
{
    public class ReleaseDate
    {
        public ReleaseDate(int day, int month, int year)
        {
            Day = day;
            Month = month;
            Year = year;
            DateTime Date = new DateTime(Year, Month, Day);
        }

        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public void ReleaseDateCheck()
        {

        }

    }
}
