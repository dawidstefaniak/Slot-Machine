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
        double CalculateWinRate(List<char> results);
    }
}
