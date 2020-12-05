namespace Day05
{
    using System;
    using System.IO;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            var content = File.ReadAllLines("input.txt");
            var boardingPasses = content.Select(x => new BoardingPass(x)).ToList();
            var seatIds = boardingPasses.Select(x => x.SeatId).ToList();

            Console.WriteLine($"Result Part 1 - {seatIds.Max()}");

            var seatId = Enumerable.Range(0, 1023).Single(seatId =>
                seatIds.Contains(seatId - 1) &&
                !seatIds.Contains(seatId) &&
                seatIds.Contains(seatId + 1)
            );
            Console.WriteLine($"Result Part 2 - {seatId}");
        }
    }

    public class BoardingPass
    {
        public BoardingPass(string input)
        {
            this.SeatId = ToInt32(string.Join("", input.Select(x => ToBinaryString(x))));
        }

        public int SeatId { get; private set; } = -1;

        private static string ToBinaryString(char c)
        {
            var binaryChar = c switch
            {
                'F' => "0",
                'B' => "1",
                'L' => "0",
                'R' => "1",
                _ => throw new NotImplementedException()
            };

            return binaryChar;
        }

        private static int ToInt32(string binaryString)
        {
            return Convert.ToInt32(binaryString, 2);
        }
    }
}
