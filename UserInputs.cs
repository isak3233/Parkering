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
        static public char GetUserAction()
        {
            while(true)
            {
                Console.WriteLine("Parkera ett nytt slumpmässigt fordon (G)");
                Console.WriteLine("Checka ut ett parkerat fordon (C)");
                char key = Console.ReadKey().KeyChar;
                RemoveOptionFromConsole();
                return key;
            }
            
        }
      
        static public string GetLicensePlate(ParkingHouse parkingHouse)
        {
            while (true)
            {
                Console.Write("Fordonets registreringsnummer (A för avbryt): ");
                string licensePlate = Console.ReadLine();
                if (parkingHouse.LicensePlateExist(licensePlate.ToUpper()))
                {
                    return licensePlate.ToUpper();
                } else
                {
                    if(licensePlate.ToUpper() == "A")
                    {
                        throw new Exception("Avbröt check out");
                    }
                    Error(licensePlate, "Existerar inte");
                }
                
            }
        }
        static public void SetVehicleInfo(Car car)
        {
            Console.WriteLine("Det kom in en bil");
            car.Color = GetColorFromUser();

            bool isElectricalCollected = false;
            while (!isElectricalCollected)
            {
                Console.Write("Är bilen en elbil (ja) eller (nej): ");
                string answer = Console.ReadLine();
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
            
        }
        static public void SetVehicleInfo(Bus bus)
        {
            Console.WriteLine("Det kom in en buss");
            bus.Color = GetColorFromUser();
            bool isAmountOfSeatsCollected = false;
            while (!isAmountOfSeatsCollected)
            {
                Console.Write("Hur många platser har bussen: ");
                int amountOfSeats = 0;
                string input = Console.ReadLine();
                RemoveOptionFromConsole();
                bool success = int.TryParse(input, out amountOfSeats);
                if(success)
                {
                    bus.AmountOfSeats = amountOfSeats;
                    isAmountOfSeatsCollected = true;
                } else
                {
                    Error(input, "Är inte ett nummer");
                }
                
            }
        }
        static public void SetVehicleInfo(Motorcycle motorcycle)
        {
            Console.WriteLine("Det kom in en motorcykel");
            motorcycle.Color = GetColorFromUser();
            bool isBrandCollected = false;
            while(!isBrandCollected)
            {
                Console.Write("Vilket märke är motorcyklen: ");
                string brand = Console.ReadLine();
                RemoveOptionFromConsole();
                if (brand == "")
                {
                    Error(brand, "Du skrev inget");
                } else
                {
                    motorcycle.Brand = brand;
                    isBrandCollected = true;
                }
            }
        }
        static public void SetVehicleInfo(Vehicle vehicle)
        {
            switch(vehicle)
            {
                case Car:
                    SetVehicleInfo((Car)vehicle);
                    break;
                case Bus:
                    SetVehicleInfo((Bus)vehicle);
                    break;
                case Motorcycle:
                    SetVehicleInfo((Motorcycle)vehicle);
                    break;
                default:
                    break;
            }

        }
        static private Colors GetColor(char colorChar)
        {
            foreach (Colors color in Enum.GetValues(typeof(Colors)))
            {
                if(color.ToString()[0] == char.ToUpper(colorChar))
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
                Console.WriteLine("Tryck F för att få fram vilka färger det finns");
                Console.Write("Fordonets färg: ");
                var key = Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (char.ToLower(key))
                {
                    case 'f':
                        RemoveOptionFromConsole();
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
                if(Helper.ColorsToSwedish(color) == "")
                {
                    Console.WriteLine($"{color} ({color.ToString()[0]})");
                } else
                {
                    Console.WriteLine($"{Helper.ColorsToSwedish(color)} ({color.ToString()[0]})");
                }
                    
            }
        }
        static public void Error(char input)
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
        static private void RemoveOptionFromConsole()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
        }

    }
}
