using Slot_Machine.Models;
using Slot_Machine.Services;
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
        private SymbolDto[,] _gamefield;
        private const int ROWS = 4;
        private const int COLUMNS = 3;
        private ICalculator _calculator;
        private double _currentwin;

        public PlayField(ICalculator calculator)
        {
            _calculator = calculator;
        }

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

            //Initialize calculator
            _calculator = new Calculator();

            //Creating Gamefield
            CreateNewGamefield();

            StartGame();
        }

        private void CreateNewGamefield()
        {
            _gamefield = null;

            //Creates new GameField each time method is called
            _gamefield = new SymbolDto[ROWS, COLUMNS];
            for (int x = 0; x < ROWS; x++)
            {
                for (int y = 0; y < COLUMNS; y++)
                {
                    //Creates new 2 dimension array of symbol objects
                    _gamefield[x, y] = new SymbolDto(_calculator);
                }
            }
        }


        private void StartGame()
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

                CreateNewGamefield();
                CheckResults();
            }
        }

        public void CheckResults()
        {
            List<SymbolDto> results = new List<SymbolDto>();
            _currentwin = 0;

            //loop is going trough each line
            for (int x = 0; x < ROWS; x++)
            {
                results.Add(_gamefield[x,0]);
                results.Add(_gamefield[x, 1]);
                results.Add(_gamefield[x, 2]);

                //Calculating WinRatio
                var winRatio = _calculator.CheckWin(results);

                //Write results on console
                foreach (var result in results)
                {
                    Console.Write(result.SymbolChar);
                }
                Console.WriteLine();

                //Calculate win
                _currentwin += winRatio * _stake;
                Math.Round(_currentwin, 2);
                //reset ratio and results list for each loop
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
            StartGame();
        }

        private void EndTheGame()
        {
            Console.Clear();
            Console.WriteLine("You are out of money. Thanks for playing!");
            Console.ReadKey();
        }
    }
}
