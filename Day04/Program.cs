namespace Day04
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");

            var part1 = PassportParser.Parse(input).Count(x => x.IsValid());
            Console.WriteLine($"Result Part 1 - {part1}");

            var part2 = PassportParser.Parse(input).Count(x => x.IsValid2());
            Console.WriteLine($"Result Part 2 - {part2}");
        }

        class Passport
        {
            public int? BirthYear { get; set; }
            public int? IssueYear { get; set; }
            public int? ExpirationYear { get; set; }
            public string Height { get; set; }
            public string HairColor { get; set; }
            public string EyeColor { get; set; }
            public string PassportId { get; set; }
            public string CountryId { get; set; }

            public bool IsValid()
            {
                return
                    this.BirthYear.HasValue &&
                    this.IssueYear.HasValue &&
                    this.ExpirationYear.HasValue &&
                    !string.IsNullOrEmpty(this.Height) &&
                    !string.IsNullOrEmpty(this.HairColor) &&
                    !string.IsNullOrEmpty(this.EyeColor) &&
                    !string.IsNullOrEmpty(this.PassportId);
                // CountryId is not required
            }

            public bool IsValid2()
            {
                static bool isHeightInRange(string height)
                {
                    var regex = new Regex(@"^(?<number>[0-9]{2,3})(?<unit>(cm|in))$");
                    if (!regex.IsMatch(height))
                    {
                        return false;
                    }

                    var match = regex.Match(height);
                    var number = Int32.Parse(match.Groups["number"].Value);
                    var unit = match.Groups["unit"].Value;

                    var result = unit switch
                    {
                        "cm" when number >= 150 && number <= 193 => true,
                        "in" when number >= 59 && number <= 76 => true,
                        _ => false
                    };

                    return result;
                }

                return
                    (this.BirthYear.HasValue && this.BirthYear.Value >= 1920 && this.BirthYear.Value <= 2002) &&
                    (this.IssueYear.HasValue && this.IssueYear.Value >= 2010 && this.IssueYear.Value <= 2020) &&
                    (this.ExpirationYear.HasValue && this.ExpirationYear.Value >= 2020 && this.ExpirationYear.Value <= 2030) &&
                    (!string.IsNullOrEmpty(this.Height) && isHeightInRange(this.Height)) &&
                    (!string.IsNullOrEmpty(this.HairColor) && new Regex(@"^#[0-9a-f]{6}$").IsMatch(this.HairColor)) &&
                    (!string.IsNullOrEmpty(this.EyeColor) &&
                        new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" }.Contains(this.EyeColor)) &&
                    ((!string.IsNullOrEmpty(this.PassportId) && new Regex(@"^[0-9]{9}$").IsMatch(this.PassportId)));
                // CountryId is not required
            }
        }

        static class PassportParser
        {
            public static IEnumerable<Passport> Parse(string input)
            {
                // split on 2 LFs
                // last chunk contains empty line
                var chunks = input.Split("\n\n").Where(x => !string.IsNullOrEmpty(x) && !string.IsNullOrWhiteSpace(x));
                var passports = chunks.Select(x => ParseChunk(x));

                return passports;
            }

            private static Passport ParseChunk(string input)
            {
                var fields = input.Split(' ', '\n').Where(x => x.Contains(":"));
                var keyValuePairs = fields.Select(x => x.Split(':')).ToDictionary(x => x[0], x => x[1]);

                var passport = new Passport();
                passport.BirthYear = keyValuePairs.TryGetValue("byr", out var birthYear) ? Int32.Parse(birthYear) : (int?)null;
                passport.IssueYear = keyValuePairs.TryGetValue("iyr", out var issueYear) ? Int32.Parse(issueYear) : (int?)null;
                passport.ExpirationYear = keyValuePairs.TryGetValue("eyr", out var expirationYear) ? Int32.Parse(expirationYear) : (int?)null;
                passport.Height = keyValuePairs.TryGetValue("hgt", out var height) ? height : null;
                passport.HairColor = keyValuePairs.TryGetValue("hcl", out var hairColor) ? hairColor : null;
                passport.EyeColor = keyValuePairs.TryGetValue("ecl", out var eyeColor) ? eyeColor : null;
                passport.PassportId = keyValuePairs.TryGetValue("pid", out var passportId) ? passportId : null;
                passport.CountryId = keyValuePairs.TryGetValue("cid", out var countryId) ? countryId : null;

                return passport;
            }
        }
    }
}
