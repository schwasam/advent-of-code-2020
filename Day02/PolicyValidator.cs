namespace Day02
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    public static class PolicyValidator
    {
        public static bool IsValid(string input)
        {
            var (digit1, digit2, letter, password) = PolicyValidator.Parse(input);

            var count = password.Count(x => x == letter);
            var isValid = count >= digit1 && count <= digit2;

            return isValid;
        }

        public static bool IsValid2(string input)
        {
            var (digit1, digit2, letter, password) = PolicyValidator.Parse(input);

            var isPos1 = password[digit1 - 1] == letter;
            var isPos2 = password[digit2 - 1] == letter;
            var isValid = isPos1 ^ isPos2;

            return isValid;
        }

        public static (int Digit1, int Digit2, char Letter, string Password) Parse(string input)
        {
            // e.g. 1-2 a: abc
            var regex = new Regex(@"^(?<digit1>\d+)-(?<digit2>\d+) (?<letter>\w): (?<pwd>\w+)$");
            var match = regex.Match(input);

            var digit1 = Int32.Parse(match.Groups["digit1"].Value);
            var digit2 = Int32.Parse(match.Groups["digit2"].Value);
            var letter = match.Groups["letter"].Value[0];
            var pwd = match.Groups["pwd"].Value;

            return (digit1, digit2, letter, pwd);
        }
    }
}
