namespace Day14
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllText("input.txt").Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(line => line.Trim());

            var stringMask = string.Empty;
            var memory = new Dictionary<int, long>();

            foreach (var line in lines)
            {
                var tokens = line.Split("=").Select(value => value.Trim()).ToList();
                var key = tokens[0];
                var value = tokens[1];

                if (key == "mask")
                {
                    stringMask = value;
                }
                else
                {
                    var address = int.Parse(Regex.Replace(key, "[^0-9]", string.Empty));
                    var oldValue = long.Parse(value);
                    var newValue = StringMasker.Apply(stringMask, oldValue);
                    memory[address] = newValue;
                }
            }

            var sum = memory.Values.Sum();
            Console.WriteLine($"Result Part 1 - {sum}");
        }
    }

    public static class StringMasker
    {
        public static long Apply(string stringMask, long value)
        {
            var orMask = Convert.ToInt64(stringMask.Replace("X", "0"), 2);
            var andMask = Convert.ToInt64(stringMask.Replace("X", "1"), 2);
            var result = (value | orMask) & andMask;

            return result;
        }
    }
}
