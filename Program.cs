namespace Parkering
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ParkingHouse parkingHouse = new ParkingHouse(15);
            parkingHouse.WriteOutParking();
        }
    }
}
