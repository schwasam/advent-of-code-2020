namespace Day06
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            var content = File.ReadAllText("input.txt");
            var answersByGroup = content.Split("\n\n").Select(x => new PassengerGroup(x));

            var distinctAnswersByGroup = answersByGroup.Select(x => x.DistinctAnswers);
            Console.WriteLine($"Result Part 1 - {distinctAnswersByGroup.Sum()}");

            var unanimousAnswersByGroup = answersByGroup.Select(x => x.UnanimousAnswers);
            Console.WriteLine($"Result Part 2 - {unanimousAnswersByGroup.Sum()}");
        }
    }

    public class PassengerGroup
    {
        private IEnumerable<IEnumerable<char>> AnswersByPassenger;

        public PassengerGroup(string inputs)
        {
            // splitting by \n seems to yield some empty lines, lets ignore them
            this.AnswersByPassenger = inputs.Split("\n").Select(ParseCustomsForm).Where(x => x.Any());
        }

        public int DistinctAnswers => this.AnswersByPassenger.SelectMany(x => x).Distinct().Count();

        public int UnanimousAnswers => this.AnswersByPassenger.Aggregate((left, right) => left.Intersect(right)).Count();

        private IEnumerable<char> ParseCustomsForm(string input)
        {
            var answers = input.Select(x => x).ToList();

            return answers;
        }
    }
}
