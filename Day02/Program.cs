namespace Day02
{
    using System;
    using System.IO;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            var items = File.ReadAllLines("input.txt");

            var part1 = items.Where(PolicyValidator.IsValid).ToList();
            Console.WriteLine($"Result Part 1 - {part1.Count}");

            var part2 = items.Where(PolicyValidator.IsValid2).ToList();
            Console.WriteLine($"Result Part 2 - {part2.Count}");
        }
    }
}
