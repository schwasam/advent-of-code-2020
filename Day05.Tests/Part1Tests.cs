namespace Day05.Tests
{
    using System;
    using Xunit;

    public class Part1Tests
    {
        [Theory]
        [InlineData("BFFFBBFRRR", 567)]
        [InlineData("FFFBBBFRRR", 119)]
        [InlineData("BBFFBBFRLL", 820)]
        public void When_Code_Returns_BoardingPass_With_SeatId(string code, int seatId)
        {
            var boardingPass = new BoardingPass(code);
            Assert.Equal(boardingPass.SeatId, seatId);
        }
    }
}
