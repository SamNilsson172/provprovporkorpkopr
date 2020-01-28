using System;
using System.Collections.Generic;
using System.Threading;

namespace Contraband
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true) //game loop
            {
                int carAmount = 0; //amount of cars the player wants to create
                while (carAmount < 1 || carAmount > 100) //makes sure the player doesn't create to few or to many
                {
                    Console.Clear();
                    carAmount = TheGoodStuff.Int("Hur många bilar ska skapas?" + "\r\n" + "min 1, max 100"); //takes intagerar input from the player
                }

                TheGoodStuff.RandomCollection<Car> carList = new TheGoodStuff.RandomCollection<Car>(); //list for all the cars with random positions, basically just a list, there's no real need for their index to be randomized
                for (int i = 0; i < carAmount; i++) //loops for all the cars that will be created
                {
                    Thread.Sleep(10); //waits for better randomization 
                    switch (TheGoodStuff.generator.Next(0, 2)) //50 50 chance to create different cars
                    {
                        case 0:
                            carList.Add(new ContrabandCar());
                            break;

                        case 1:
                            carList.Add(new CleanCar());
                            break;
                    }
                }

                int totArrest = 0; //total cars arrested
                string[] options = { "Undersök en ny bil", "Avsluta" };
                while (carList.array.Length > 0) //while there still are cars to be examined
                {
                    Console.WriteLine("Återstående bilar: " + carList.array.Length); //tell the player how many cars are left
                    if (TheGoodStuff.Selection(options, "Vad vill du göra?", 2) == 0) //lets the player quit any time
                    {
                        carList.array[0].PrintStats(); //looks at car
                        if (carList.array[0].Examine()) //if you found contraband in the car
                        {
                            totArrest++; 
                            Console.WriteLine("Du arresterade föraren");
                        }
                        else 
                        {
                            Console.WriteLine("Du lät föraren köra vidare");
                        }
                        carList.Remove(carList.array[0]); //remove car from list
                    }
                    else //quits
                    {
                        Environment.Exit(0);
                    }

                    TheGoodStuff.ClickToContinue();
                }

                Console.WriteLine("Du arreserade totalt " + totArrest + " av " + Car.totContraCar + " personer med stöldgods i bilen!"); //tells you how many arrests you got right
                Car.totContraCar = 0; //resets contra car amount

                string[] jaEllerNej = { "Ja", "Nej" };
                if (TheGoodStuff.Selection(jaEllerNej, "Spela igen?", 1) == 1) //asks the player if they want to quit or play again
                {
                    break;
                }
            }
        }
    }
}
