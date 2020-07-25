using System;

namespace Vensha.Extensions {
    public static class DateTimeExtension {
        private static string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        public static string toHuman (this DateTime date) => $"{months[date.Month - 1]} {date.Day}. {date.Year}";
    }
}