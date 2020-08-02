using System.Collections.Generic;
using System.Linq;
using System;

namespace Vensha.Helpers
{
    public class UniqueRandom
    {
        private int _lastNum;
        private Random _random = new Random();

        /// <summary>
        /// Gets an int between floor (inclusive) and floor (exclusive) or null if the difference of floor and ceiling is smaller than 2
        /// </summary>
        public int? Next(int floor, int ceiling)
        {
            if (Math.Abs(floor - ceiling) < 2) return null;

            int num = _random.Next(floor, ceiling);
            if (num == _lastNum) return Next(floor, ceiling);
            _lastNum = num;
            return num;
        }
        /// <summary>
        /// Gets an int between 0 and the ceiling (exclusive) or null if the ceiling is smaller than 2
        /// </summary>
        public int? Next(int ceiling)
        {
            return Next(0, ceiling);
        }
        /// <summary>
        /// Gets a random IEnumerable index
        /// </summary>
        public int? Next<T>(IEnumerable<T> arr)
        {
            int length = arr.Count();
            if (length < 2) return null;

            return Next(0, length);
        }
    }
}