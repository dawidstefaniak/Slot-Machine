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
        private Bank _bank;
        private double _stake;
        private char[,] _gamefield;
        private const int ROWS = 4;
        private const int COLUMNS = 3;
        private Calculator _calculator;
        private double _currentwin;

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
            _bank = new Bank(deposit);

            //gamefield is place for the rolled chars
            _gamefield = new char[ROWS,COLUMNS];

            //Initialize calculator
            _calculator = new Calculator();

            StartBetting();

        }

        private void StartBetting()
        {
            if (_bank.Balance <= 0)
            {
                EndTheGame();
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Your actual account balance: {_bank.Balance:C}");
                Console.WriteLine("Enter stake amount:");
                _stake = -1;
                //checks if stake is not higher than bank balance
                while (_stake < 0 || _stake > _bank.Balance)
                {
                    try
                    {
                        _stake = Convert.ToDouble(Console.ReadLine());
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
                    //Draw random symbol
                    _gamefield[x, y] = _calculator.GetSymbol();

                    //Displays Playfield
                    Console.Write(_gamefield[x,y]);
                }
                Console.WriteLine();
            }
            CheckResults();
        }

        public void CheckResults()
        {
            List<char> results = new List<char>();
            double ratio = 0;
            _currentwin = 0;
            //loop is going trough each line
            for (int x = 0; x < ROWS; x++)
            {
                results.Add(_gamefield[x,0]);
                results.Add(_gamefield[x, 1]);
                results.Add(_gamefield[x, 2]);

                //win if:
                //cell 0 and cell 1 or 2 are stars
                if (_gamefield[x, 0] == '*')
                {
                    if (_gamefield[x, 1] == '*')
                    {
                        ratio += _calculator.CalculateWinRate(results);
                    }

                    else if (_gamefield[x, 2] == '*')
                    {
                        ratio += _calculator.CalculateWinRate(results);
                    }
                    //cell 0 is star and rest are equal
                    else if (_gamefield[x, 1] == _gamefield[x, 2])
                    {
                        ratio += _calculator.CalculateWinRate(results);
                    }
                }
                //cell 1 and 2 are stars
                else if (_gamefield[x, 1] == '*')
                {
                    if (_gamefield[x, 2] == '*')
                    {
                        ratio += _calculator.CalculateWinRate(results);
                    }
                    //cell 1 is star and cell 0 and 2 are equal
                    if (_gamefield[x, 0] == _gamefield[x, 2])
                    {
                        ratio += _calculator.CalculateWinRate(results);
                    }
                }
                //cell 2 is star and cell 0 and 1 are equal
                else if (_gamefield[x, 2] == '*')
                {
                    if (_gamefield[x, 0] == _gamefield[x, 1])
                    {
                        ratio += _calculator.CalculateWinRate(results);
                    }
                }
                //all cells are the same and none of them is a star
                else if (_gamefield[x,0] == _gamefield[x,1] && _gamefield[x,1] == _gamefield[x,2])
                {
                    ratio += _calculator.CalculateWinRate(results);
                }

                _currentwin += ratio * _stake;

                //reset ratio and results list for each loop
                ratio = 0;
                results.Clear();
            }
            _bank.AddWin(_currentwin, _stake);
            DisplayResults();

        }

        private void DisplayResults()
        {
            Console.WriteLine($"You have won: {_currentwin:C}");
            Console.WriteLine($"Current balance is: {_bank.Balance:C}");
            Console.WriteLine("Click any key to continue");
            Console.ReadKey();
            StartBetting();
        }

        private void EndTheGame()
        {
            Console.Clear();
            Console.WriteLine("You are out of money. Thanks for playing!");
            Console.ReadKey();
        }
    }
}
