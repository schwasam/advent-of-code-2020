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
            var combinations = values.Select(value =>
            {
                var next = values.Where(x => value + x == expected).FirstOrDefault();
                return (value, next);
            }).ToList();
            var result = combinations.FirstOrDefault(x => x.next != default);

            return (result.value, result.next, result.value * result.next);
        }

        private static (int val1, int val2, int val3, int prod) SolvePart2(IList<int> values)
        {
            const int expected = 2020;

            for (int i = 0; i < values.Count(); i++)
            {
                for (int j = i + 1; j < values.Count(); j++)
                {
                    for (int k = j + 1; k < values.Count(); k++)
                    {
                        if (values[i] + values[j] + values[k] == expected)
                        {
                            return (values[i], values[j], values[k], values[i] * values[j] * values[k]);
                        }
                    }
                }
            }

            return (0, 0, 0, 0);
        }
    }
}
