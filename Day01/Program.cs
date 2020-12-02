using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day01
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = File.ReadAllLines("input.txt").Select(x => Int32.Parse(x)).ToList();

            var result1 = SolvePart1(numbers);
            Console.WriteLine($"Result Part 1 - {result1.val1} * {result1.val2} => {result1.prod}");

            var result2 = SolvePart2(numbers);
            Console.WriteLine($"Result Part 2 - {result2.val1} * {result2.val2} * {result2.val3} => {result2.prod}");
        }

        private static (int val1, int val2, int prod) SolvePart1(IEnumerable<int> values)
        {
            const int expected = 2020;
            var result = (from first in values
                          from second in values.Skip(1)
                          where first + second == expected
                          select (first, second, first * second)
            ).FirstOrDefault();

            return result;
        }

        private static (int val1, int val2, int val3, int prod) SolvePart2(IList<int> values)
        {
            const int expected = 2020;
            var result = (from first in values
                          from second in values.Skip(1)
                          from third in values.Skip(2)
                          where first + second + third == expected
                          select (first, second, third, first * second * third)
            ).FirstOrDefault();

            return result;
        }
    }
}
