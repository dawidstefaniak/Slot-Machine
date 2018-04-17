using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slot_Machine
{
    public class Bank
    {
        public double Balance { get; private set; }

        public Bank(double balance)
        {
            Balance = balance;
        }

        public void AddWin(double winAmount, double stake)
        {
            Balance += winAmount - stake;
        }

    }
}
