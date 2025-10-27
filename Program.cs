namespace Parkering
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            int amountOfParkingSpots = 15;
            float parkingHouseParkingFee = 1.5f;
            ParkingHouse parkingHouse = new ParkingHouse(amountOfParkingSpots, parkingHouseParkingFee);
            while (true)
            {
                parkingHouse.WriteOutParking();
                char userAction = UserInputs.GetUserAction();
                switch(userAction)
                {
                    case 'g':
                        Vehicle vehicleToAdd = UserInputs.SetVehicleInfo(Helper.GetRandomVehicle());
                        bool success = parkingHouse.TryAddVehicleToParkHouse(vehicleToAdd);
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
