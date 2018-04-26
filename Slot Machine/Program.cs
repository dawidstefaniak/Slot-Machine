using Slot_Machine.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slot_Machine
{
    class Program
    {
        static void Main(string[] args)
        {
            //Injecting new calculator as service
            var playfield = new PlayField(new Calculator());
            playfield.Initialize();
        }
    }
}
