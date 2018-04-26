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

        public double CalculateWinRate(List<char> results)
        {
            double calculatedWinRatio = 0;

            foreach (var result in results)
            {
                if (result == 'A')
                {
                    calculatedWinRatio += 0.4;
                }

                if (result == 'B')
                {
                    calculatedWinRatio += 0.6;
                }

                if (result == 'P')
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
    }
}
