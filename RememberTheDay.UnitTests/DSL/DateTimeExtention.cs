using System;

namespace RememberTheDay.UnitTests.DSL
{
    public static class DateTimeExtention
    {
        public static DateTime JanuaryOf(this int day, int year)
        {
            return new DateTime(year, 1, day);
        }

        public static DateTime FebraryOf(this int day, int year)
        {
            return new DateTime(year, 2, day);
        }
        
        public static DateTime MarchOf(this int day, int year)
        {
            return new DateTime(year, 3, day);
        }
        
        public static DateTime DecemberOf(this int day, int year)
        {
            return new DateTime(year, 12, day);
        }
        
    }
}