using System;

using ValidationCore;

namespace BullsAndCowsProj
{
    class Program
    {
        static void Main(string[] args)
        {
            int sizeOfCharactersArbitrary;

            Console.WriteLine("Imagine the number of characters arbitrary, computer has to guess this.");

            do
            {
                Console.WriteLine("Enter length of your characters sequence (no more than 7)");
                sizeOfCharactersArbitrary = Validation.TryParseIntValueInLine(Console.ReadLine());
            }
            while ((sizeOfCharactersArbitrary == int.MinValue) || (sizeOfCharactersArbitrary < 0) || (sizeOfCharactersArbitrary > 7));

            Console.WriteLine("Enter the number of bulls and cows, separated by a space.");
            Console.WriteLine("Bulls - number in place, Cows - the correct numbers.");

            Game game = new Game(sizeOfCharactersArbitrary);

            while (game.OutputList.Count > 1)
            {
                Random rnd = new Random();

                game.BullsAndCows[0] = int.MinValue;

                string probability = game.OutputList[rnd.Next(0, game.OutputList.Count - 1)];

                Console.Write("{0} input bulls and cows = ", probability);

                do
                {
                    try
                    {
                        game.GetNumbersOfBullsAndCows(Console.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                } while (game.BullsAndCows[0] == int.MinValue);

                game.SearchFunction(probability);

                if (game.BullsAndCows[0] == game.SizeOfCharactersArbitrary)
                { 
                    Console.WriteLine("OutputList is {0}!", game.OutputList[0]);
                    break;
                }

                if (game.OutputList.Count >= 1)
                    Console.WriteLine("Next move: ");

                if (game.OutputList.Count < 1)
                    Console.WriteLine("Computer can't guess your number. Game over!");
            }

            Console.ReadKey();
        }

    }
}
