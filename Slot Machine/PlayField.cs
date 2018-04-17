using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Slot_Machine
{
    class PlayField
    {
        private Bank bank;
        private double stake;
        private char[,] gamefield;
        private Random random;
        private const int ROWS = 4;
        private const int COLUMNS = 3;
        private Calculator calculator;
        private double currentwin;

        public void Initialize()
        {
            Console.WriteLine("Please deposit money you would like to play with:");
            double deposit = -1;
            while (deposit < 0)
            {
                try
                {
                    deposit = Convert.ToDouble(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("The deposit has to be number higher than 0");
                }
            }

            //initialize bank
            bank = new Bank(deposit);

            //gamefield is place for the rolled chars
            gamefield = new char[ROWS,COLUMNS];

            //Initialize Random
            random = new Random();

            //Initialize calculator
            calculator = new Calculator();

            StartBetting();

        }

        private void StartBetting()
        {
            if (bank.Balance <= 0)
            {
                EndTheGame();
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Your actual account balance: {bank.Balance}");
                Console.WriteLine("Enter stake amount:");
                stake = -1;
                //checks if stake is not higher than bank balance
                while (stake < 0 || stake > bank.Balance)
                {
                    try
                    {
                        stake = Convert.ToDouble(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("The stake has to be number higher than 0 and smaller than your balance");
                    }
                }

                Roll();
            }
        }

        private void Roll()
        {
            for (int x =0; x<ROWS;x++ )
            {
                for (int y = 0; y < COLUMNS; y++)
                {
                    int randomNumber = random.Next(1, 100);
                    char symbolToPaste = calculator.GetSymbol(randomNumber);

                    gamefield[x, y] = symbolToPaste;

                    //Displays Playfield
                    Console.Write(gamefield[x,y]);
                }
                Console.WriteLine();
            }
            CheckResults();
        }

        public void CheckResults()
        {
            List<char> results = new List<char>();
            double ratio = 0;
            currentwin = 0;
            //loop is going trough each line
            for (int x = 0; x < ROWS; x++)
            {
                results.Add(gamefield[x,0]);
                results.Add(gamefield[x, 1]);
                results.Add(gamefield[x, 2]);

                //win if:
                //cell 0 and cell 1 or 2 are stars
                if (gamefield[x, 0] == '*')
                {
                    if (gamefield[x, 1] == '*')
                    {
                        ratio += calculator.CalculateWinRate(results);
                    }

                    else if (gamefield[x, 2] == '*')
                    {
                        ratio += calculator.CalculateWinRate(results);
                    }
                    //cell 0 is star and rest are equal
                    else if (gamefield[x, 1] == gamefield[x, 2])
                    {
                        ratio += calculator.CalculateWinRate(results);
                    }
                }
                //cell 1 and 2 are stars
                else if (gamefield[x, 1] == '*')
                {
                    if (gamefield[x, 2] == '*')
                    {
                        ratio += calculator.CalculateWinRate(results);
                    }
                    //cell 1 is star and cell 0 and 2 are equal
                    if (gamefield[x, 0] == gamefield[x, 2])
                    {
                        ratio += calculator.CalculateWinRate(results);
                    }
                }
                //cell 2 is star and cell 0 and 1 are equal
                else if (gamefield[x, 2] == '*')
                {
                    if (gamefield[x, 0] == gamefield[x, 1])
                    {
                        ratio += calculator.CalculateWinRate(results);
                    }
                }
                //all cells are the same and none of them is a star
                else if (gamefield[x,0] == gamefield[x,1] && gamefield[x,1] == gamefield[x,2])
                {
                    ratio += calculator.CalculateWinRate(results);
                }

                currentwin += ratio * stake;

                //reset ratio and results list for each loop
                ratio = 0;
                results.Clear();
            }
            bank.AddWin(currentwin, stake);
            DisplayResults();

        }

        private void DisplayResults()
        {
            Console.WriteLine($"You have won: {currentwin}");
            Console.WriteLine($"Current balance is: {bank.Balance}");
            Console.WriteLine("Click any key to continue");
            Console.ReadKey();
            StartBetting();
        }

        private void EndTheGame()
        {
            Console.WriteLine("You are out of money. Thanks for playing!");
            Console.ReadKey();
        }
    }
}
