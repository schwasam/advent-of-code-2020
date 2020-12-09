namespace Day08
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            var listing = File.ReadAllText("input.txt");
            var program = new Prog(listing);

            var result1 = program.Run();

            Console.WriteLine($"Result Part 1 - {result1.accumulator}, {result1.terminated}");

            for (var candidate = 0; candidate < program.Instructions.Count(); candidate += 1)
            {
                var mutation = program.Mutate(candidate);
                var result2 = mutation.Run();
                if (result2.terminated)
                {
                    Console.WriteLine($"Result Part 2 - Instruction {candidate}");
                    Console.WriteLine($"Result Part 2 - Accumulator {result2.accumulator}");
                    break;
                }
            }
        }
    }

    class Prog
    {
        public List<Instruction> Instructions { get; private set; }

        public int InstructionPointer { get; private set; }

        // counts how many times a line in the listing has been executed => loop detection
        public Dictionary<int, int> Counters { get; private set; } = new Dictionary<int, int>();

        public int Accumulator { get; private set; }

        public Prog(string listing)
        {
            this.Instructions = listing.Replace("\r", "").Split('\n', StringSplitOptions.RemoveEmptyEntries)
                .Select(Parse).ToList();
        }

        public Prog(List<Instruction> instructions)
        {
            this.Instructions = instructions;
        }

        public (int accumulator, bool terminated) Run()
        {
            while (true)
            {
                if (this.InstructionPointer >= this.Instructions.Count())
                {
                    // no infinite loop detected
                    return (this.Accumulator, true);
                }

                if (!this.Counters.ContainsKey(this.InstructionPointer)) this.Counters.Add(this.InstructionPointer, 0);
                if (this.Counters[this.InstructionPointer] > 0) return (this.Accumulator, false); // we execute an instruction the 2nd time
                this.Counters[this.InstructionPointer] += 1;

                var instruction = this.Instructions[this.InstructionPointer];
                switch (instruction)
                {
                    case Acc acc:
                        this.Accumulator += acc.Argument;
                        this.InstructionPointer += 1;
                        break;
                    case Jmp jmp:
                        this.InstructionPointer += jmp.Argument;
                        break;
                    case Nop _:
                        this.InstructionPointer += 1;
                        break;
                }
            }
        }

        public Prog Mutate(int instruction)
        {
            // copy instructions
            var mutation = new List<Instruction>(this.Instructions);
            // mutate one of them
            mutation[instruction] = mutation[instruction].Mutate();

            return new Prog(mutation);
        }

        private Instruction Parse(string line)
        {
            var tokens = line.Split(' ');
            var opcode = tokens[0];
            var argument = Int32.Parse(tokens[1]);

            Instruction instruction = opcode switch
            {
                "acc" => new Acc(argument),
                "jmp" => new Jmp(argument),
                "nop" => new Nop(argument),
                _ => throw new NotImplementedException($"OpCode {opcode} not implemented.")
            };

            return instruction;
        }
    }

    abstract class Instruction
    {
        public int Argument { get; set; }

        protected Instruction(int argument)
        {
            this.Argument = argument;
        }

        public virtual Instruction Mutate() { return this; }
    }

    class Acc : Instruction
    {
        public Acc(int argument) : base(argument) { }
    }

    class Jmp : Instruction
    {
        public Jmp(int argument) : base(argument) { }

        public override Instruction Mutate()
        {
            return new Nop(this.Argument);
        }
    }

    class Nop : Instruction
    {
        public Nop(int argument) : base(argument) { }

        public override Instruction Mutate()
        {
            return new Jmp(this.Argument);
        }
    }
}
