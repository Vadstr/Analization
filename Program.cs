using System;

namespace Analization
{
    class Program
    {
        static void Main(string[] args)
        {
            var restart = false;
            do
            {
                Console.Write("\nInput:");
                var formula = Console.ReadLine();
                var anal = new Analizationn(formula);
                Console.WriteLine("\n" + formula + " = " + anal.Result);
                Console.WriteLine("\n If you want reuse it - 1 \n Exit - anouther one");
                var action = Console.ReadLine();
                if (action == "1")
                {
                    restart = true;
                }
                else 
                {
                    restart = false;
                }
            } while (restart);
        }
    }
}
