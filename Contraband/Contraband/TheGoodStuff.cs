using System;
using System.Collections.Generic;
using System.Text;

namespace Contraband
{
    class TheGoodStuff
    {
        public static Random generator = new Random();

        public static void ClickToContinue() //me me lazy
        {
            Console.Write("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        public static string String(int min, int max, bool lowerCase, string question) //returns a string that the player inputs
        {
            string input = "";
            while (input.Length > max || input.Length < min) //loop as long as input has wrong size
            {
                Console.WriteLine(question); //have the question in method so you can see it again if you write the wrong thing
                Console.WriteLine(min + "-" + max + " characters");
                input = Console.ReadLine(); //get input from player
                input.Trim();
                if (input.Length > max) //error messages if input has wrong size
                    Console.WriteLine("Too long, try again!");
                if (input.Length < min)
                    Console.WriteLine("Too short, try again!");
                ClickToContinue(); //to read error message
            }
            if (lowerCase) //if lower case is wanted when called, for checking input against another string
                input.ToLower();
            return input;
        }

        public static int Int(string question) //returns an int that the player inputs
        {
            int inputInt = 0; //creates nullable int to return and know when to stop looping
            while (true) //loop as long as player has not input a number or number is bigger/smaller than int.max/minValue
            {
                Console.WriteLine(question); //write out question every loop
                Console.WriteLine("Write with numbers");
                string input = Console.ReadLine(); //gets player input

                if (!double.TryParse(input, out double knowIfTooBig)) //if players input is not in numbers, use double to avoid int.max/minValue
                    Console.WriteLine("Write in numbers, try again!");
                else
                {
                    if (knowIfTooBig > int.MaxValue) //if inputs value is bigger than can be stored in an int
                        Console.WriteLine("Number too big!");
                    if (knowIfTooBig < int.MinValue) //if inputs value is smaller than can be stored in an int
                        Console.WriteLine("Number too small!");
                    if (knowIfTooBig <= int.MaxValue && knowIfTooBig >= int.MinValue)
                    {
                        inputInt = (int)knowIfTooBig; //give inputInt a value that is not null to break loop
                        break;
                    }
                }
                ClickToContinue();
            }
            return inputInt;
        }

        public static int Selection(string[] items, string question, int columns) //gives the player a list of options and lets them choose, returns the index in items of choosen item
        {
            int mostChar = 0; //find the amount of chars in the longest string
            foreach (string item in items)
            {
                char[] charsInItem = item.ToCharArray();
                if (charsInItem.Length > mostChar)
                    mostChar = charsInItem.Length;
            }
            mostChar += 3; //add 3 to take the 3 chars added later into account

            int selectedIndex = 0; //creates int to return
            while (true) //infinite loop cus you cant create a defined ConsoleKeyInfo, breakes when input is enter
            {
                string totWriteOut = "";
                Console.WriteLine(question); //write question every loop
                Console.WriteLine();
                for (int i = 0; i < items.Length; i++) //loop once for all strings in items
                { //write out the string and a > if it is currently selected
                    string writeOut = "";
                    if (i == selectedIndex)
                        writeOut += " > ";
                    else
                        writeOut += "   ";
                    writeOut += items[i];
                    while (writeOut.ToCharArray().Length < mostChar) //add some spaces to make all writeOut equaly long in every loop
                        writeOut += " ";

                    totWriteOut += writeOut;

                    if ((i + 1) % columns == 0 || i == items.Length - 1) //console writeline it and reset string if a full row is added
                    {
                        Console.WriteLine(totWriteOut);
                        totWriteOut = "";
                    }
                }
                ConsoleKeyInfo input = Console.ReadKey(); //get a key input from the player
                if (input.Key == ConsoleKey.UpArrow && (selectedIndex - columns) >= 0) //go up
                    selectedIndex -= columns;
                if (input.Key == ConsoleKey.LeftArrow && selectedIndex > 0 && selectedIndex % columns != 0) //go left
                    selectedIndex--;
                if (input.Key == ConsoleKey.DownArrow && (selectedIndex + columns) < items.Length) //go down
                    selectedIndex += columns;
                if (input.Key == ConsoleKey.RightArrow && selectedIndex < items.Length - 1 && (selectedIndex + 1) % columns != 0) //go right
                    selectedIndex++;
                if (input.Key == ConsoleKey.Enter) //break when player presses enter
                    break;
                Console.Clear();
            }
            return selectedIndex;
        }

        public static void Selection(string[] items, string question, int columns, Action[] methods) //selection that calles static methods
        {
            int index = Selection(items, question, columns); //call selection
            methods[index](); //call selected method
        }

        public static bool IsItInYet(int nom, int[] noms) //looks in an intager array to find if a specific number is in it
        {
            foreach (int i in noms)
            {
                if (nom == i)
                {
                    return true;
                }
            }
            return false;
        }

        public class RandomCollection<T>
        {
            Random generator = new Random();
            public T[] array;

            public RandomCollection()
            {
                array = new T[0];
            }
            public RandomCollection(T[] preArray)
            {
                array = preArray;
                Random(); //uses it muliple times to make it more random
                Random();
                Random();
            }

            public void Add(T item) //adds an item to array
            {
                bool toBig = array.Length >= int.MaxValue; //if the array is already to big
                if (!toBig)
                {
                    T[] newArray = new T[array.Length + 1]; //make a new array that is one slot bigger than the old one
                    for (int i = 0; i < array.Length; i++) //add array to new array
                    {
                        newArray[i] = array[i];
                    }
                    newArray[array.Length] = item; //add the new item to the array
                    array = newArray; //set old array to the new one
                    Random(); //randomize
                    Random();
                    Random();
                }
                else Console.WriteLine("Array to big!");
            }

            public void Remove(T item)
            {
                int? index = null; //nullable because 0 is a valid index
                for (int i = 0; i < array.Length; i++) //is given item in array
                {
                    if (array[i].ToString() == item.ToString())
                    {
                        index = i; //remeber its index in array
                        break;
                    }
                }

                T[] newArray = new T[array.Length - 1]; //create new array that is one slot smaller
                if (index != null)
                {
                    int a = 0; //describes index for the new array
                    for (int i = 0; i < array.Length; i++) //loops through all the items in array
                    {
                        if (i == index) continue; //if i is the index that should be removed, continue
                        else
                        {
                            newArray[a] = array[i]; //add the old array to the new one
                            a++;
                        }
                    }
                    array = newArray; //set old array to the new one
                    Random(); //randomize
                    Random();
                    Random();
                }
                else Console.WriteLine("Given item is not in the collection!");
            }

            void Random() //scrambles the order of the array, could be improved because currently objects further down on the list tends to be placed further down on the list even after the scramble
            {
                T[] newArray = new T[array.Length]; //array that contains the same values with a new random index
                int[] takenNumbers = new int[array.Length]; //numbers that have already been randomized
                for (int i = 0; i < array.Length; i++) //set all values to -1 so 0 is not a teken number
                    takenNumbers[i] = -1;

                for (int i = 0; i < array.Length; i++) //loops through the length of array to give all of them a new index
                {
                    int newIndex = generator.Next(0, array.Length);
                    if (TheGoodStuff.IsItInYet(newIndex, takenNumbers)) //has the new random int already been randomized
                    {
                        for (int a = 0; a < array.Length; a++) // find the first int that is not in taken numbers
                        {
                            if (!TheGoodStuff.IsItInYet(a, takenNumbers))
                            {
                                newIndex = a; //set the new index to a valid number
                                break; //to neccecary but for preformance
                            }
                        }
                    }
                    takenNumbers[i] = newIndex; //add the number to the taken ones
                    newArray[newIndex] = array[i]; //pass the value to the new array with the new index
                }
                array = newArray; //update the array
            }

            public void Scramble()
            {
                Random();
                Random();
                Random();
                Random();
            }
        }

    }
}
