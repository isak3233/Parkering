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
        }
        public bool TryAddVehicleToParkHouse(Vehicle vehicleToAdd)
        {
            for(int i = 0;i < ParkingSpots.Count; i++)
            {
                ParkingSpot parkingSpot = ParkingSpots[i];
                if(vehicleToAdd.Size > 1.0f)
                {
                    float sizeStillToAdd = vehicleToAdd.Size;
                    for(int j = 0;  j < vehicleToAdd.Size; j++)
                    {
                        sizeStillToAdd -= parkingSpot.AvailableSpace;
                    }
                    if(sizeStillToAdd <= 0)
                    {
                        vehicleToAdd.TimeWhenParked = DateTime.Now;
                        float sizeAdded = 0.0f;
                        for (int j = 0; j < vehicleToAdd.Size; j++)
                        {
                            ParkingSpots[i + j].TryAddVehicle(vehicleToAdd, sizeAdded);
                            sizeAdded += 1.0f;
                        }
                        return true;
                    }
                }
                if (ParkingSpots[i].TryAddVehicle(vehicleToAdd))
                {
                    vehicleToAdd.TimeWhenParked = DateTime.Now;
                    return true;
                }
            }
            return false;
        }
        private void RemoveVehicle(Vehicle vehicleToRemove)
        {
            for(int i = 0;  i < ParkingSpots.Count;i++)
            {
                ParkingSpots[i].RemoveVehicle(vehicleToRemove);
            }
            OptimizeParking();
        }
        public void RemoveVehicle(string licensePlate)
        {
            Vehicle vehicleToRemove = GetVehicle(licensePlate);
            RemoveVehicle(vehicleToRemove);
        }
        private void OptimizeParking()
        {
            for (int i = 1; i < ParkingSpots.Count; i++)
            {
                ParkingSpot parkingSpot = ParkingSpots[i];
                List<Vehicle> vehicles = new (parkingSpot.Vehicles);

                foreach (var vehicle in vehicles)
                {
                    for (int j = 0; j < i; j++)
                    {
                        var newParkingSpot = ParkingSpots[j];

                        if (newParkingSpot.TryAddVehicle(vehicle))
                        {
                            parkingSpot.RemoveVehicle(vehicle);
                            break;
                        }
                    }
                }
            }
        }
        public Vehicle GetVehicle(string licensePlate)
        {
            foreach (ParkingSpot parkingSpot in ParkingSpots)
            {
                foreach (Vehicle vehicle in parkingSpot.Vehicles)
                {
                    if (vehicle.LicensePlate == licensePlate)
                    {
                        return vehicle;
                    }
                }
            }
            throw new Exception("Finns inget fordon med denna registreringsskylt " + licensePlate);
        }
        public bool LicensePlateExist(string licensePlate)
        {
            try
            {
                GetVehicle(licensePlate);
                return true;
            } catch
            {
                return false;
            }
        }
        public void WriteOutParking()
        {
            List<Vehicle> vehiclesWritenOut = new List<Vehicle>();
            for (int i = 0; i < ParkingSpots.Count;i++)
            {
                ParkingSpot parkingSpot = ParkingSpots[i];
                if(parkingSpot.Vehicles.Count == 0)
                {
                    string indexString = $"Plats {i + 1}";
                    Console.WriteLine($"{indexString.PadRight(15)} Tom");
                }
                
                foreach (Vehicle vehicle in parkingSpot.Vehicles)
                {
                    if (vehiclesWritenOut.Contains(vehicle))
                    {
                        continue;
                    }
                    string indexString = "Plats ";
                    
                    for(int j = 0; j < vehicle.Size; j++)
                    {
                        if (j != 0)
                        {
                            indexString += "-";
                        }
                        indexString += i + (j+1);
                    }
                    Console.WriteLine($"{indexString.PadRight(15)} {vehicle.GetInformation()}");
                    vehiclesWritenOut.Add(vehicle);
                }
                
            }
        }
       
    }
}
