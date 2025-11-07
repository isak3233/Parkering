using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkering
{
    internal class ParkingHouse
    {
        public ParkingSpot[] ParkingSpots { get; }
        private float ParkFee { get; }
        private Dictionary<Vehicle, DateTime> parkTimers = new Dictionary<Vehicle, DateTime>();
        public ParkingHouse(int amountOfParkings, float parkFee)
        {
            ParkingSpots = new ParkingSpot[amountOfParkings];
            for(int i = 0; i < ParkingSpots.Length; i++)
            {
                ParkingSpots[i] = new ParkingSpot();
            }
            ParkFee = parkFee;
        }
        public bool TryAddVehicle(Vehicle vehicleToAdd)
        {
            // Försöker hitta bra plats åt motorcykel
            if (vehicleToAdd is Motorcycle)
            {
                for (int i = 0; i < ParkingSpots.Length; i++)
                {
                    ParkingSpot parkingSpotForMC = ParkingSpots[i];
                    if (parkingSpotForMC.AvailableSpace == 0.5f && ParkingSpots[i].TryAddVehicle(vehicleToAdd))
                    {
                        parkTimers.Add(vehicleToAdd, DateTime.Now);
                        return true;
                    }
                }
            }
            // Hittar bra plats åt bilar, bussar och motorcykeln om motorcykeln inte hittade en bra plats
            for (int i = 0;i < ParkingSpots.Length; i++)
            {
                ParkingSpot parkingSpot = ParkingSpots[i];
                
                if (vehicleToAdd.Size > 1.0f)
                {
                    float sizeStillToAdd = vehicleToAdd.Size;
                    for (int j = 0; j < vehicleToAdd.Size; j++)
                    {
                        if (j + i < ParkingSpots.Length)
                        {
                            sizeStillToAdd -= ParkingSpots[i + j].AvailableSpace;
                        }

                    }
                    if (sizeStillToAdd <= 0)
                    {

                        parkTimers.Add(vehicleToAdd, DateTime.Now);
                        for (int j = 0; j < vehicleToAdd.Size; j++)
                        {
                            ParkingSpots[i + j].TryAddVehicle(vehicleToAdd);
                        }
                        return true;
                    }
                } else if (ParkingSpots[i].TryAddVehicle(vehicleToAdd))
                {
                    parkTimers.Add(vehicleToAdd, DateTime.Now);
                    return true;
                }
            }
            return false;
        }
        private void RemoveVehicle(Vehicle vehicleToRemove)
        {
            for(int i = 0;  i < ParkingSpots.Length; i++)
            {
                ParkingSpots[i].RemoveVehicle(vehicleToRemove);
            }
        }
        public void RemoveVehicle(string licensePlate)
        {
            Vehicle vehicleToRemove = GetVehicle(licensePlate);
            RemoveVehicle(vehicleToRemove);
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
        
        public (TimeSpan parkedTotalTime, double parkingFee) GetParkingFeeInfo(string licensePlate)
        {

            TimeSpan parkedTotalTime = DateTime.Now - parkTimers[GetVehicle(licensePlate)];
            return (parkedTotalTime, (parkedTotalTime.TotalMinutes * ParkFee));
            
        }
        public void WriteOutParking()
        {
            List<Vehicle> vehiclesWritenOut = new List<Vehicle>();
            for (int i = 0; i < ParkingSpots.Length; i++)
            {
                ParkingSpot parkingSpot = ParkingSpots[i];
                if(parkingSpot.Vehicles.Count == 0 )
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
                    string extraSlots = $"-{(i) + vehicle.Size}";
                    string indexString = $"Plats {(i+1)}{(vehicle.Size > 1.0f  ? extraSlots : "")}";
                    Console.WriteLine($"{indexString.PadRight(15)} {vehicle}");
                    
                    vehiclesWritenOut.Add(vehicle);

                }
            }
        }
       
    }
}
