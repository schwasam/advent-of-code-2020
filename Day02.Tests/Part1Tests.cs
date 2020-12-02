namespace Day02.Tests
{
    using System;
    using Xunit;

    public class Part1Tests
    {
        [Theory]
        [InlineData("1-3 a: abcde")]
        [InlineData("2-9 c: ccccccccc")]
        public void When_IsValid_Returns_IsValid(string input)
        {
            var isValid = PolicyValidator.IsValid(input);
            Assert.True(isValid);
        }

        [Theory]
        [InlineData("1-3 b: cdefg")]
        public void When_NotValid_Returns_NotValid(string input)
        {
            var isValid = PolicyValidator.IsValid(input);
            Assert.False(isValid);
        }
    }
}
