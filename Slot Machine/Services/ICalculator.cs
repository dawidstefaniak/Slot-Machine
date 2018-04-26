using Slot_Machine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slot_Machine.Services
{
    public interface ICalculator
    {
        char GetSymbol();
        double CalculateWinRate(List<SymbolDto> results);
        double CheckWin(List<SymbolDto> results);
    }
}
