using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkering
{
    internal class ParkingHouse
    {
        public Vehicle[] ParkingSpots { get; set; }
        public ParkingHouse(int amountOfParkings)
        {
            ParkingSpots = new Vehicle[amountOfParkings];
            ParkingSpots[0] = new Car("ABC123", Colors.Red, false);
            ParkingSpots[1] = new Car("DFG321", Colors.Blue, true);
            ParkingSpots[2] = new Motorcycle("AAA111", Colors.Green, "Volkswagen");
        }
        public void WriteOutParking()
        {
            int index = 1;
            foreach(Vehicle vehicle in ParkingSpots)
            {
                if(vehicle != null)
                {
                    Console.WriteLine($"{index}\t {vehicle.GetInformation()}");
                }
                
                index++;
            }
        }
    }
}
