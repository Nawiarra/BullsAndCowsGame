using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using ValidationCore;

namespace BullsAndCowsProj
{
    public class Game
    {
        private static Random RandomNumber = new Random();
        public int SizeOfCharactersArbitrary { get; set; }
        public int[] BullsAndCows { get; set; }
        public List<string> OutputList { get; set; }
        public Game(int sizeOfCharactersArbitrary)
        {
            SizeOfCharactersArbitrary = sizeOfCharactersArbitrary;

            OutputList = RemoveListNumder(SetCombinations(sizeOfCharactersArbitrary)).ToList();

            BullsAndCows = new int[2];
        }
        public static IEnumerable<string> SetCombinations(int sizeNumber)
        {
            int values = (int)Math.Pow(10, sizeNumber);
            string str = new string('0', sizeNumber);

            for (int i = 0; i < values; i++)
            {
                yield return i.ToString(str, CultureInfo.InvariantCulture);
            }

        }
        public void GetNumbersOfBullsAndCows(string line)
        {
            string[] input = line.Split(' ').ToArray();

            BullsAndCows = new int[input.Length];

            if (input.Length < 2)
            {
                BullsAndCows[0] = int.MinValue;

                throw new Exception("Separate bools and cows numbers by space");
            }

            for (int i = 0; i < input.Length; i++)
            {
                if (i < 2)
                {
                    BullsAndCows[i] = Validation.TryParseIntValueInLine(input[i]);

                    if ((BullsAndCows[i] == int.MinValue) || (BullsAndCows[i] < 0) || (BullsAndCows[0] + BullsAndCows[1] > SizeOfCharactersArbitrary))
                    {
                        BullsAndCows[0] = int.MinValue;

                        throw new Exception("Incorrect bulls and cows number");
                    }
                }
            }
        }

        public static IEnumerable<T> RemoveListNumder<T>(IEnumerable<T> sourse)
        {
            List<T> listNumber = sourse.ToList();

            while (listNumber.Count > 0)
            {
                int bufferNumber = RandomNumber.Next(listNumber.Count);
                yield return listNumber[bufferNumber];
                listNumber.RemoveAt(bufferNumber);
            }
        }

        public void SearchFunction(string probability)
        {
            for (int probablyOutput = OutputList.Count - 1; probablyOutput >= 0; probablyOutput--)
            {
                int countBulls = 0, countCows = 0;

                for (int bufferNumber = 0; bufferNumber < SizeOfCharactersArbitrary; bufferNumber++)
                {
                    if (OutputList[probablyOutput][bufferNumber] == probability[bufferNumber])
                        countBulls++;

                    if (OutputList[probablyOutput].Contains(probability[bufferNumber]))
                        countCows++;
                }

                if (BullsAndCows[1] == BullsAndCows[0] && BullsAndCows[1] == 0)
                {
                    if ((countBulls != BullsAndCows[0]) || (countCows != BullsAndCows[1]))
                        OutputList.RemoveAt(probablyOutput);
                }

                else if ((countBulls != BullsAndCows[0]) && (countCows != BullsAndCows[1]))
                    OutputList.RemoveAt(probablyOutput);
            }

        }



    }
}
