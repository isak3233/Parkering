using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkering
{
    internal class ParkingSpot
    {
        public List<Vehicle> Vehicles { get; set; }
        public ParkingSpot()
        {
            Vehicles = new List<Vehicle>();
        }
        public void AddVehicle(Vehicle vehicleToAdd)
        {
            Vehicles.Add(vehicleToAdd);
        }
        public void RemoveVehicle(Vehicle vehicleToRemove)
        {
            Vehicles.Remove(vehicleToRemove);
        }
    }
}
