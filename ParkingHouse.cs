using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkering
{
    internal class ParkingHouse
    {
        public List<ParkingSpot> ParkingSpots { get; set; }
        public ParkingHouse(int amountOfParkings)
        {
            ParkingSpots = new List<ParkingSpot>();
            for (int i = 0; i < amountOfParkings; i++)
            {
                ParkingSpots.Add(new ParkingSpot());
            }


            ParkingSpots[0].AddVehicle(new Car("ABC123", Colors.Red, false));
            ParkingSpots[1].AddVehicle(new Car("DFG321", Colors.Blue, true));
            ParkingSpots[2].AddVehicle(new Motorcycle("AAA111", Colors.Green, "Volkswagen"));
        }
        public void WriteOutParking()
        {
            int index = 1;
            foreach(ParkingSpot parkingSpot in ParkingSpots)
            {
                foreach (Vehicle vehicle in parkingSpot.Vehicles)
                {
                    Console.WriteLine($"{index}\t {vehicle.GetInformation()}");
                    
                }
                index++;
            }
        }
    }
}
