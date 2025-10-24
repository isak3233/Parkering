namespace Parkering
{
    internal class Program
    {
        
        static void Main(string[] args)
        {

            ParkingHouse parkingHouse = new ParkingHouse(15);
            Bus bus1 = new Bus("AAA111", Colors.Red, 20);
            Bus bus2 = new Bus("BBB222", Colors.Blue, 20);
            parkingHouse.TryAddVehicleToParkHouse(bus1);
            parkingHouse.TryAddVehicleToParkHouse(bus2);
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
                        string licensePlate = UserInputs.GetLicensePlate(parkingHouse);
                        WriteOutParkingFee(parkingHouse.GetVehicle(licensePlate));
                        parkingHouse.RemoveVehicle(licensePlate);
                        break;
                    case 'o':
                        parkingHouse.OptimizeParking();
                        break;
                    default:
                        break;
                }
                Console.Write("Tryck ENTER för att fortsätta");
                Console.ReadLine();
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
