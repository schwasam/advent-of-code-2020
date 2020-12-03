namespace Day03
{
    using System;
    using System.IO;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllText("input.txt");
            var walker = new AreaWalker(lines);

            // Part 1
            var trees = walker.WalkToBottom(0, 0, 3, 1);

            Console.WriteLine($"Result Part 1 - {trees}");

            // Part 2
            var trees1 = walker.WalkToBottom(0, 0, 1, 1);
            var trees2 = walker.WalkToBottom(0, 0, 3, 1);
            var trees3 = walker.WalkToBottom(0, 0, 5, 1);
            var trees4 = walker.WalkToBottom(0, 0, 7, 1);
            var trees5 = walker.WalkToBottom(0, 0, 1, 2);

            Console.WriteLine($"Result Part 2 - trees1 {trees1}");
            Console.WriteLine($"Result Part 2 - trees2 {trees2}");
            Console.WriteLine($"Result Part 2 - trees3 {trees3}");
            Console.WriteLine($"Result Part 2 - trees4 {trees4}");
            Console.WriteLine($"Result Part 2 - trees5 {trees5}");
            Console.WriteLine($"Result Part 2 - {trees1 * trees2 * trees3 * trees4 * trees5}");
        }
    }
}
