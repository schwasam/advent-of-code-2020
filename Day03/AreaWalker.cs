namespace Day03
{
    using System;
    using System.Linq;

    public class AreaWalker
    {
        public int[][] MapTiles { get; }

        public int MapWidth => MapTiles[0].Length;

        public int MapHeight => MapTiles.Length;

        public AreaWalker(string input)
        {
            var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries); // split on LF
            this.MapTiles = lines.Select(line => line.Select(x => x == '#' ? 1 : 0).ToArray()).ToArray();
        }

        public long WalkToBottom(int startX, int startY, int right, int down)
        {
            var trees = 0L;

            var x = startX;
            for (int y = startY; y < this.MapHeight; y += down)
            {
                trees += this.MapTiles[y][x];
                x = (x + right) % this.MapWidth;
            }

            return trees;
        }
    }
}
