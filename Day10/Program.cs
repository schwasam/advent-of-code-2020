namespace Day10
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.IO;

    class Program
    {
        static void Main(string[] args)
        {
            var numbers = File.ReadAllLines("input.txt").Select(Int32.Parse).ToList();
            var deltas = GetDeltas(numbers);

            var ones = deltas.Count(x => x == 1);
            var threes = deltas.Count(x => x == 3) + 1; // outlet, adapters and device

            Console.WriteLine($"Result Part 1 - {ones * threes}");
        }

        // Part 1
        public static IEnumerable<int> GetDeltas(IEnumerable<int> numbers)
        {
            var ordered = numbers.OrderBy(x => x).ToList();
            ordered.Insert(0, 0); // outlet is at 0 jolts

            var deltas = ordered.Select((value, index) =>
            {
                var next = ordered.Skip(index + 1).Take(1).FirstOrDefault();
                var delta = next - value;
                return delta;
            }).Where(x => x >= 0).ToList(); // the last delta is negative because of the default value

            return deltas;
        }
    }
}
