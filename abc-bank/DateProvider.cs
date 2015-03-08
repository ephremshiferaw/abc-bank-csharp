using System;

namespace abc_bank
{
    public class DateProvider
    {
        private static DateProvider _instance;

        public static DateProvider Instance
        {
            get { return _instance ?? (_instance = new DateProvider()); }
        }

        public int DaysSince(DateTime pastDate)
        {
            return (DateTime.Now - pastDate).Days;
        }

        public DateTime Now
        {
            get { return DateTime.Now; }
        }
    }
}
