using System;
using System.Collections.Generic;
using System.Text;

namespace Contraband
{
    class Car
    {
        public static int totContraCar = 0; //amont of contraband cars
        public int passengers;
        public int contrabandAmount;
        public bool alreadyChecked = false;
        public Random generator = new Random();

        public bool Examine() //true if contraband found
        {
            alreadyChecked = true;

                if (generator.Next(0, contrabandAmount + 1) == 0) //math kinda to see if there are contrabands in the car
                {

                    Console.WriteLine("Det finns inget stöldgods i bilen!");
                    return false;
                }
                else
                {
                    Console.WriteLine("Det finns " + contrabandAmount + " stöldgods i bilen!");
                    return true;
                }
        }

        public void PrintStats() //write out text
        {
            Console.WriteLine("Bilen har " + passengers + " passagerare");
        }
    }
}
