using System;

namespace Vensha.Extensions {
    public static class IntExtension {

        public static string toOrdinal (this int num) => num.ToString () + getIndicator (num);

        public static string getIndicator (this int num) {
            num = Math.Abs (num);
            int cent = num % 100;
            if (cent >= 10 && cent <= 20) return "th";
            int dec = num % 10;
            if (dec == 1) return "st";
            if (dec == 2) return "nd";
            if (dec == 3) return "rd";
            return "th";
        }
    }
}