using System;
using System.Collections.Generic;
using System.Text;

namespace Contraband
{
    class CleanCar : Car
    {
        public CleanCar()
        {
            passengers = generator.Next(1, 4); //random amount of passangers
            contrabandAmount = 0; //clean cars have np contrabands
        }
    }
}
