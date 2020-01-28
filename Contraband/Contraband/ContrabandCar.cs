using System;
using System.Collections.Generic;
using System.Text;

namespace Contraband
{
    class ContrabandCar : Car
    {
        public ContrabandCar()
        {
            totContraCar++; //add to the amount of contraband cars
            passengers = generator.Next(1, 5); //random amount of passengers
            contrabandAmount = generator.Next(1, 5); //random amount of contrabands
        }
    }
}
