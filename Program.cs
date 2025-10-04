namespace lab3
{
    class Program
    {
        // utility function of an individual U(x) = ln(x + 30) -> x >-30
        class Lottery
        {
            public float condition1 { get; private set; }
            public float condition2{ get; private set; }
            public float chanceOfCondition { get; private set; }
            
            // Calculations
            public float M { get; private set; } // Expected profit
            public float U { get; private set; } // Expected usefulness
            public float X{ get; private set; } // Deterministic equivalent
            public float Pi { get; private set; } // risk premium

            public Lottery(){
                this.condition1 = 0;
                this.condition2 = 0;
                this.chanceOfCondition = 0;
                this.M = 0;
                this.U = 0;
                this.X = 0;
                this.Pi = 0;
            }
            public Lottery(float condition1, float chanceOfCondition, float condition2)
            {
                if(condition1 <= -30 ||  condition2 <= -30)
                    throw new InvalidDataValue($"Incorrect input: {condition1} and {condition2} must be greater than -30");
                if(chanceOfCondition >= 1 || chanceOfCondition < 0) 
                    throw new InvalidDataValue($"Incorrect input: {chanceOfCondition}, must be less than 1 and greater than 0");
                this.condition1 = condition1;
                this.condition2 = condition2;
                this.chanceOfCondition = chanceOfCondition;
                
                //Calculation
                this.M = condition1 * chanceOfCondition +  condition2 * (1 - chanceOfCondition);
                this.U = (float)(Math.Log(condition1 + 30) * chanceOfCondition) + (float)(Math.Log(condition2 + 30) * (1 - chanceOfCondition));
                this.X = (float) Math.Pow(Math.E, U) - 30;
                this.Pi = M - X;
            }

            public override string ToString()
            {
                return "Lottery( " + condition1 + ", " + chanceOfCondition + ", "+ condition2 + " )\n" +
                       "Expected profit: " + M + "\n" +
                       "Expected usefulness: " + U + "\n" +
                       "Deterministic equivalent: " + X + "\n" +
                       "Risk premium: " + Pi;
            }
        }
        
        static void Main(string[] args)
        {
            try
            {
                Lottery[] lotery = new Lottery[3];
                lotery[0] = new Lottery(-20, 0.5f, -10);
                lotery[1] = new Lottery(-25, 0.5f, -15);
                lotery[2] = new Lottery(-29, 0.5f, -19);

                foreach (var l in lotery)
                    Console.WriteLine(l.ToString() + "\n");
                
                Lottery bestLottery = lotery.Where(b => b.U == lotery.Max(lot => lot.U)).FirstOrDefault();
                
                Console.WriteLine("Best option for person, that will not take risk is:\n" + bestLottery.ToString());
            }
            catch(InvalidDataValue e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public class InvalidDataValue : Exception
        {
            public InvalidDataValue(){}
            public InvalidDataValue(string message) : base(message) { }
            public InvalidDataValue(string message, Exception innerException) : base(message, innerException) { }
        }
    }
}