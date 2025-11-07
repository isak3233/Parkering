namespace Parkering
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            int amountOfParkingSpots = 15;
            float parkingHouseParkingFee = 1.5f;
            ParkingHouse parkingHouse = new ParkingHouse(amountOfParkingSpots, parkingHouseParkingFee);

            bool isRandomVehicle = UserInputs.GetYesOrNo("Ska fordonen slumpmässigt genereras");
            Console.Clear();

            while (true)
            {
                parkingHouse.WriteOutParking();

                Console.WriteLine("Parkera ett nytt slumpmässigt fordon (G)");
                Console.WriteLine("Checka ut ett parkerat fordon (C)");
                char userAction = UserInputs.GetUserAction();

                switch(char.ToLower(userAction))
                {
                    case 'g':
                        Vehicle newVehicle;

                        if (isRandomVehicle)
                        {
                            newVehicle = Helper.GetRandomVehicle();
                        } else
                        {
                            newVehicle = Helper.GetSetVehicle();
                        }

                        UserInputs.SetVehicleInfo(newVehicle);

                        bool success = parkingHouse.TryAddVehicle(newVehicle);
                        if(success)
                        {
                            Console.WriteLine("Fordonet lades till i parkerings huset");
                        } else
                        {
                            Console.WriteLine("Det fanns inte plats i parkerings huset");
                        }
                        break;
                    case 'c':
                        try
                        {
                            string licensePlate = UserInputs.GetLicensePlate(parkingHouse);
                            (TimeSpan parkedTotalTime, double parkingFee) = parkingHouse.GetParkingFeeInfo(licensePlate);
                            Console.WriteLine($"Fordonet har varit parkerad i {Helper.GetTimeString(parkedTotalTime)} och det kostar då {(parkingFee):F2}kr");

                            parkingHouse.RemoveVehicle(licensePlate);
                        } catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    default:
                        UserInputs.Error(userAction);
                        break;
                }
                Console.Write("Tryck ner tangent för att fortsätta");
                Console.ReadKey();
                Console.Clear();
            }
        }
 

    }
}
