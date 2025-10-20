namespace Parkering
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ParkingHouse parkingHouse = new ParkingHouse(15);
            parkingHouse.WriteOutParking();
            
            UserInputs.UserSetVehicleInfo(GetRandomVehicle());
        }
        static private Vehicle GetRandomVehicle()
        {
            string randomLicensePlate = GetRandomLicensePlate();
            Vehicle[] vehicleOptions = { new Car(randomLicensePlate, Colors.Red, false), new Bus(randomLicensePlate, Colors.Red, 0), new Motorcycle(randomLicensePlate, Colors.Red, "") };
            Random rnd = new Random();
            int randomTypeOfCar = rnd.Next(0, 3);
            return vehicleOptions[randomTypeOfCar];
        }
        static private string GetRandomLicensePlate()
        {
            Random random = new Random();
            string licensePlate = "";
            for(int i = 0; i < 3; i++)
            {
                licensePlate += (char)random.Next('A', 'Z' + 1);

            }
            for (int i = 0; i < 3; i++)
            {
                licensePlate += (char)random.Next('0', '9' + 1);

            }
            return licensePlate;
        }
    }
}
