using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Parkering
{
    internal class UserInputs
    {
        static public Car UserSetVehicleInfo(Car car)
        {
            car.Color = GetColorFromUser();

            bool isElectricalCollected = false;
            while (!isElectricalCollected)
            {
                Console.Write("Är bilen en elbil (ja) eller (nej): ");
                string answer = Console.ReadLine();
                Console.WriteLine();
                switch (answer.ToLower())
                {
                    case "ja":
                        car.IsElectrical = true;
                        isElectricalCollected = true;
                        break;
                    case "nej":
                        car.IsElectrical = false;
                        isElectricalCollected = true;
                        break;
                    default:
                        Error(answer);
                        break;
                }
            }
            return car;
            
        }
        static public Bus UserSetVehicleInfo(Bus bus)
        {
            bus.Color = GetColorFromUser();
            bool isAmountOfSeatsCollected = false;
            while (!isAmountOfSeatsCollected)
            {
                Console.Write("Hur många platser har bussen: ");
                int amountOfSeats = 0;
                string input = Console.ReadLine();
                bool success = int.TryParse(input, out amountOfSeats);
                if(success)
                {
                    isAmountOfSeatsCollected = true;
                } else
                {
                    Error(input, "Är inte ett nummer");
                }
                
            }
            return bus;
        }
        static public Motorcycle UserSetVehicleInfo(Motorcycle motorcycle)
        {
            motorcycle.Color = GetColorFromUser();
            bool isBrandCollected = false;
            while(!isBrandCollected)
            {
                Console.Write("Vilket märke är motorcyklen: ");
                string brand = Console.ReadLine();
                if(brand == "")
                {
                    Error(brand, "Du skrev inget");
                } else
                {
                    isBrandCollected = true;
                }
            }
            return motorcycle;
        }
        static public Vehicle UserSetVehicleInfo(Vehicle vehicle)
        {
            switch(vehicle)
            {
                case Car:
                    return UserSetVehicleInfo((Car)vehicle);
                case Bus:
                    return UserSetVehicleInfo((Bus)vehicle);
                case Motorcycle:
                    return UserSetVehicleInfo((Motorcycle)vehicle);
                default:
                    return vehicle;
            }

        }
        static private Colors GetColor(char colorChar)
        {
            foreach (Colors color in Enum.GetValues(typeof(Colors)))
            {
                if(Helper.ColorsToSwedish(color)[0] == char.ToUpper(colorChar))
                {
                    return color;
                }
            }
            throw new Exception();
        }
        static private Colors GetColorFromUser()
        {
            while (true)
            {
                Console.WriteLine("Tryck C för att få fram vilka färger det finns");
                Console.Write("Fordonets färg: ");
                var key = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (key)
                {
                    case 'C' or 'c':
                        WriteOutColors();
                        break;
                    default:
                        try
                        {
                            Colors color = GetColor(key);
                            return color;
                        }
                        catch
                        {
                            Error(key);
                        }
                        break;
                }
            }
        }
        static private void WriteOutColors()
        {
            foreach(Colors color in Enum.GetValues(typeof(Colors)))
            {
                Console.WriteLine($"{Helper.ColorsToSwedish(color)} ({Helper.ColorsToSwedish(color)[0]})");
            }
        }
        static private void Error(char input)
        {
            Console.WriteLine($"{input} finns inte som alternativ");
        }
        static private void Error(string input)
        {
            Console.WriteLine($"{input} finns inte som alternativ");
        }
        static private void Error(string input, string errorMessage)
        {
            Console.WriteLine($"{input} {errorMessage}");
        }

    }
}
