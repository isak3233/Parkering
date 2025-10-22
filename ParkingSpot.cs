using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Parkering
{
    internal class ParkingSpot
    {
        public float AvailableSpace { get; set; }
        public List<Vehicle> Vehicles { get; }
        public ParkingSpot()
        {
            Vehicles = new List<Vehicle>();
            AvailableSpace = 1.0f;
        }
        public bool TryAddVehicle(Vehicle vehicleToAdd, float sizeAlreadyAdded = 0.0f)
        {
            float sizeToAdd = vehicleToAdd.Size - sizeAlreadyAdded;
            if (sizeToAdd > 1.0f && AvailableSpace == 1.0f)
            {
                AvailableSpace = 0.0f;
                Vehicles.Add(vehicleToAdd);
                return true;
            }
            if(AvailableSpace - sizeToAdd >= 0.0f)
            {
                AvailableSpace -= sizeToAdd;
                Vehicles.Add(vehicleToAdd);
                return true;
            }
            return false;
        }
        public void RemoveVehicle(Vehicle vehicleToRemove)
        {
            if(!Vehicles.Contains(vehicleToRemove))
            {
                return;
            }

            if (vehicleToRemove.Size > 1.0f)
            {
                AvailableSpace = 1.0f;
                Vehicles.Remove(vehicleToRemove);
            } else
            {
                AvailableSpace += vehicleToRemove.Size;
                Vehicles.Remove(vehicleToRemove);
            }
        }
    }
}
