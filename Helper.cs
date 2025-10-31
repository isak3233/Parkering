using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkering
{
    internal class Helper
    {
        private static int[] SetVehicleOptions = {1, 2, 3};
        private static int VehicleOptionOn = -1;
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
                case Colors.Yellow:
                    swedishText = "Gul";
                    break;
                case Colors.White:
                    swedishText = "Vit";
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
        static public Vehicle GetSetVehicle()
        {
            VehicleOptionOn++;
            if(SetVehicleOptions.Length - 1 < VehicleOptionOn)
            {
                VehicleOptionOn = 0;
            }
            return GetNewInstance(SetVehicleOptions[VehicleOptionOn]);
        }
        static public Vehicle GetRandomVehicle()
        {
            string randomLicensePlate = Helper.GetRandomLicensePlate();
            Random rnd = new Random();
            int randomTypeOfCar = rnd.Next(1, 4);
            return GetNewInstance(randomTypeOfCar);
        }
        static private Vehicle GetNewInstance(int option)
        {
            switch (option)
            {
                case 1:
                    return new Car();
                case 2:
                    return new Bus();
                case 3:
                    return new Motorcycle();
            }
            throw new Exception("Finns inget fordon med alternativet " + option);
        }
        static public string GetTimeString(TimeSpan parkedTotalTime)
        {
            string secondsString = $"{(parkedTotalTime.Seconds > 9 ? parkedTotalTime.Seconds : $"0{parkedTotalTime.Seconds}")}";
            string minutesString = $"{(parkedTotalTime.Minutes > 9 ? parkedTotalTime.Minutes : $"0{parkedTotalTime.Minutes}")}";
            return $"{minutesString}:{secondsString}";
        }
        
    }
}
