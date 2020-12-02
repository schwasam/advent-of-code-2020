namespace Day02.Tests
{
    using System;
    using Xunit;

    public class Part2Tests
    {
        [Theory]
        [InlineData("1-3 a: abcde")]
        public void When_IsValid_Returns_IsValid(string input)
        {
            var isValid = PolicyValidator.IsValid2(input);
            Assert.True(isValid);
        }

        [Theory]
        [InlineData("1-3 b: cdefg")]
        [InlineData("2-9 c: ccccccccc")]
        public void When_NotValid_Returns_NotValid(string input)
        {
            var isValid = PolicyValidator.IsValid2(input);
            Assert.False(isValid);
        }
    }
}
