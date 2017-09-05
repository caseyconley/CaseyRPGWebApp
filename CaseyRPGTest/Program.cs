using System;
using CaseyRPGLib.Models;

namespace CaseyRPGTest
{
    class Program
    {
        static void Main(string[] args)
        {
            CharacterClassProperties.Initialize();

            Rogue myRogue = new Rogue("Garona Halforcen");
            Console.WriteLine(myRogue.ToDetailsString());
            Console.WriteLine(myRogue.Class.ToDetailsString());

            myRogue.AddItemToBackpack(new ShortSword());
            Console.WriteLine("Added shortsword to inventory");
            Console.WriteLine(myRogue.ToDetailsString());

            myRogue.LevelUp();
            Console.WriteLine("Leveled up");
            Console.WriteLine(myRogue.ToDetailsString());

            Console.ReadLine();
        }
    }
}