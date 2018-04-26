using Slot_Machine.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slot_Machine.Models
{
    public class SymbolDto
    {
        private char _symbolChar;
        private readonly ICalculator _calculator;
        public char SymbolChar { get => _symbolChar; set => _symbolChar = value; }

        public SymbolDto(ICalculator calculator)
        {
            _calculator = calculator;
            SymbolChar = calculator.GetSymbol();
        }
    }
}
