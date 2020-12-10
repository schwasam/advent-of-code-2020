
namespace Day09
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            var numbers = File.ReadAllLines("input.txt").Select(Int64.Parse).ToList();

            var result1 = CodeBreaker.FindAnomaly(numbers, 25);
            Console.WriteLine($"Result Part 1 - {result1}");

            var result2 = CodeBreaker.FindMinMax(numbers, result1.Value);
            Console.WriteLine($"Result Part 2 - {result2.Min} + {result2.Max} => {result2.Min + result2.Max}");
        }
    }

    static class CodeBreaker
    {
        public static (long Index, long Value) FindAnomaly(List<long> numbers, int preambleSize)
        {
            for (var index = preambleSize + 1; index < numbers.Count; index++)
            {
                var preamble = numbers.Skip(index - preambleSize).Take(preambleSize).ToList(); // sliding window
                var target = numbers[index];
                var pairs = (from a in preamble
                             from b in preamble
                             where (a != b && a + b == target)
                             select (a, b));
                if (!pairs.Any()) return (index, target);
            }

            return (-1, -1);
        }

        public static (long Min, long Max) FindMinMax(List<long> numbers, long target)
        {
            var windowStart = 0;
            var windowEnd = 1;

            while (windowEnd < numbers.Count)
            {
                if (windowStart >= windowEnd) windowEnd += 1;

                var window = numbers.Skip(windowStart).Take(windowEnd - windowStart).ToList();
                var sum = window.Sum();

                if (sum == target) return (window.Min(), window.Max());
                if (sum < target) windowEnd += 1;
                if (sum > target) windowStart += 1;
            }

            return (-1, -1);
        }
    }
}
