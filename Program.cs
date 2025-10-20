namespace Parkering
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Vehicle car1 = new("ABC123");
            Console.WriteLine(car1.LicensePlate);
        }
    }
}
