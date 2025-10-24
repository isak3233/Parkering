namespace Parkering
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            ParkingHouse parkingHouse = new ParkingHouse(15);
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
                            WriteOutParkingFee(parkingHouse.GetVehicle(licensePlate));
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
        static private void WriteOutParkingFee(Vehicle vehicle)
        {
            TimeSpan parkedTotalTime = DateTime.Now - vehicle.TimeWhenParked;
            Console.WriteLine($"Fordonet har varit parkerad i {Helper.GetTimeString(parkedTotalTime)} och det kostar då {GetParkingFee(parkedTotalTime):F2}kr");
        }
        static private double GetParkingFee(TimeSpan parkedTotalTime)
        {
            return parkedTotalTime.TotalMinutes * 1.5;
        }




    }
}
