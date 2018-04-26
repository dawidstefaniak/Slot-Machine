using Slot_Machine.Models;
using Slot_Machine.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slot_Machine.Services
{
    public class Calculator : ICalculator
    {
        readonly Random _random = new Random();

        public double CalculateWinRate(List<SymbolDto> results)
        {
            double calculatedWinRatio = 0;

            foreach (var result in results)
            {
                if (result.SymbolChar == 'A')
                {
                    calculatedWinRatio += 0.4;
                }

                if (result.SymbolChar == 'B')
                {
                    calculatedWinRatio += 0.6;
                }

                if (result.SymbolChar == 'P')
                {
                    calculatedWinRatio += 0.8;
                }
                // for wildcard there is nothing to add.
            }

            return calculatedWinRatio;
        }
        public char GetSymbol()
        {
            int randomNumber = _random.Next(1, 100);
            //Returns Wildcard(5% chance)
            if (randomNumber <= 5)
            {
                return '*';
            }
            //Returns Pineapple (15% chance)
            if (randomNumber <= 20)
            {
                return 'P';
            }
            //Returns Banana (35% chance)
            if (randomNumber <= 55)
            {
                return 'B';
            }
            //Returns Apple (45% chance)
            return 'A';
        }
        public double CheckWin(List<SymbolDto> results)
        {
            var ratio = 0.0;
            //win if:
            //cell 0 and cell 1 or 2 are stars
            if (results[0].SymbolChar == '*')
            {
                if (results[1].SymbolChar == '*')
                {
                    ratio += CalculateWinRate(results);
                }

                else if (results[2].SymbolChar == '*')
                {
                    ratio += CalculateWinRate(results);
                }
                //cell 0 is star and rest are equal
                else if (results[1].SymbolChar == results[2].SymbolChar)
                {
                    ratio += CalculateWinRate(results);
                }
            }
            //cell 1 and 2 are stars
            else if (results[1].SymbolChar == '*')
            {
                if (results[2].SymbolChar == '*')
                {
                    ratio += CalculateWinRate(results);
                }
                //cell 1 is star and cell 0 and 2 are equal
                if (results[0].SymbolChar == results[2].SymbolChar)
                {
                    ratio += CalculateWinRate(results);
                }
            }
            //cell 2 is star and cell 0 and 1 are equal
            else if (results[2].SymbolChar == '*')
            {
                if (results[0].SymbolChar == results[1].SymbolChar)
                {
                    ratio += CalculateWinRate(results);
                }
            }
            //all cells are the same and none of them is a star
            else if (results[0].SymbolChar == results[1].SymbolChar && results[1].SymbolChar == results[2].SymbolChar)
            {
                ratio += CalculateWinRate(results);
            }
            return ratio;
        }
    }
}
