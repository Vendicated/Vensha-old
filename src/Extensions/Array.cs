using System;
using System.Linq;

namespace Vensha.Extensions
{
    public static class ArrayExtension
    {
        private static Random _random = new Random();

        public static T[] shuffle<T>(this T[] arr) => arr.OrderBy(x => _random.Next()).ToArray();

        public static T get<T>(this T[] arr, int index) => arr.Length > index ? arr[index] : default(T);

        public static T random<T>(this T[] arr) => arr.shuffle().FirstOrDefault();
    }
}