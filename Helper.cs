using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkering
{
    internal class Helper
    {
        static public string ColorsToSwedish(Colors color)
        {
            string swedishText = "";
            switch(color)
            {
                case Colors.Red:
                    swedishText = "Röd";
                    break;
                case Colors.Green:
                    swedishText = "Grön";
                    break;
                case Colors.Blue:
                    swedishText = "Blå";
                    break;
                default:
                    break;
            }
            return swedishText;
        }
        static public string GetRandomLicensePlate()
        {
            Random random = new Random();
            string licensePlate = "";
            for (int i = 0; i < 3; i++)
            {
                licensePlate += (char)random.Next('A', 'Z' + 1);

            }
            for (int i = 0; i < 3; i++)
            {
                licensePlate += (char)random.Next('0', '9' + 1);

            }
            return licensePlate;
        }
        static public Vehicle GetRandomVehicle()
        {
            string randomLicensePlate = Helper.GetRandomLicensePlate();
            Vehicle[] vehicleOptions = { new Car(randomLicensePlate, Colors.Red, false), new Bus(randomLicensePlate, Colors.Red, 0), new Motorcycle(randomLicensePlate, Colors.Red, "") };
            Random rnd = new Random();
            int randomTypeOfCar = rnd.Next(0, 3);
            return vehicleOptions[randomTypeOfCar];
        }
        static public string GetTimeString(TimeSpan parkedTotalTime)
        {
            string secondsString = $"{(parkedTotalTime.Seconds > 9 ? parkedTotalTime.Seconds : $"0{parkedTotalTime.Seconds}")}";
            string minutesString = $"{(parkedTotalTime.Minutes > 9 ? parkedTotalTime.Minutes : $"0{parkedTotalTime.Minutes}")}";
            return $"{minutesString}:{secondsString}";
        }
        
    }
}
