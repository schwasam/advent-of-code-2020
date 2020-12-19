namespace Day16
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        private static TicketParser parser;
        private static int[] myTicket;
        private static int[][] nearbyTickets;

        public static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");

            var part1 = SolvePart1(input);
            Console.WriteLine($"Result Part 1 - {part1}");

            var part2 = SolvePart2(input);
            Console.WriteLine($"Result Part 2 - {part2}");
        }

        private static int SolvePart1(string input)
        {
            Parse(input);

            return nearbyTickets.Sum(ticket => parser.FindInvalid(ticket).Sum());
        }

        private static long SolvePart2(string input)
        {
            Parse(input);

            var ticketRules = nearbyTickets.Where(parser.IsValid).Select(ticket => parser.ListRules(ticket)).ToList();
            var fieldRules = new Dictionary<int, List<string>>();

            // TODO: better solution than loops? LINQ?
            foreach (var rules in ticketRules)
            {
                for (var fieldIndex = 0; fieldIndex < rules.Length; fieldIndex++)
                {
                    if (fieldRules.ContainsKey(fieldIndex))
                    {
                        fieldRules[fieldIndex] = fieldRules[fieldIndex].Intersect(rules[fieldIndex]).ToList();
                    }
                    else
                    {
                        fieldRules.Add(fieldIndex, rules[fieldIndex].ToList());
                    }
                }
            }

            // assumption: there is one field where only one rule matches, no guessing required
            // start there and remove that rule from the other fields
            // TODO: better solution than loops? LINQ?
            var solution = fieldRules.Where(pair => pair.Value.Count == 1).ToDictionary(pair => pair.Key, pair => pair.Value.First());
            while (fieldRules.Any())
            {
                foreach (var foundField in solution.Values)
                {
                    foreach (var field in fieldRules.Keys) fieldRules[field].Remove(foundField);
                }
                var keysToRemove = fieldRules.Where(pair => pair.Value.Count == 0).Select(pair => pair.Key);
                foreach (var key in keysToRemove) fieldRules.Remove(key);
                foreach (var (key, value) in fieldRules.Where(pair => pair.Value.Count == 1)) solution.Add(key, value.First());
            }
            // foreach (var key in solution.Keys) Console.WriteLine(key + ": " + solution[key]);
            var solutionIndices = solution.Where(pair => pair.Value.StartsWith("departure")).Select(pair => pair.Key)
                .ToArray();

            var answer = solutionIndices.Select(index => myTicket[index]).Aggregate(1L, (left, right) => left * right);

            return (answer);
        }

        private static void Parse(string input)
        {
            var parts = input.Replace("\r", "").Split("\n\n");

            var rules = parts[0];
            parser = new TicketParser(rules);

            myTicket = parts[1].Split("\n", StringSplitOptions.RemoveEmptyEntries)
              .Skip(1)
              .First()
              .Split(",")
              .Select(Int32.Parse).ToArray();

            nearbyTickets = parts[2].Split("\n", StringSplitOptions.RemoveEmptyEntries)
              .Skip(1)
              .Select(x => x.Split(",")
              .Select(Int32.Parse).ToArray()).ToArray();
        }
    }

    class TicketParser
    {
        private IEnumerable<Rule> rules;
        private bool isInvalid(int value) => this.rules.All(rule => !rule.IsSatisfied(value));

        public TicketParser(string rules)
        {
            this.rules = rules.Split("\n").Select(Rule.Parse);
        }

        public IEnumerable<int> FindInvalid(int[] ticket) => ticket.Where(isInvalid).ToList();
        public bool IsValid(int[] ticket) => !ticket.Any(isInvalid);
        public string[][] ListRules(int[] ticket) => ticket.Select(ListRulesSatisfiedBy).ToArray();
        public string[] ListRulesSatisfiedBy(int value) => rules.Where(rule => rule.IsSatisfied(value)).Select(rule => rule.Name).ToArray();
    }

    class Rule
    {
        public string Name { get; set; }
        public IEnumerable<Func<int, bool>> Conditions;

        public static Rule Parse(string rule)
        {
            var tokens = rule.Split(":").Select(x => x.Trim()).ToList();

            return new Rule
            {
                Name = tokens[0],
                Conditions = tokens[1].Split("or").Select(x => x.Trim()).Select<string, Func<int, bool>>(range =>
                {
                    var limits = range.Split("-").Select(Int32.Parse);
                    return value => limits.First() <= value && value <= limits.Last();
                })
            };
        }

        public bool IsSatisfied(int value) => Conditions.Any(condition => condition(value));
    }
}
