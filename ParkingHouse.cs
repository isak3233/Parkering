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
        public bool TryAddVehicleToParkHouse(Vehicle vehicleToAdd)
        {
            for(int i = 0;i < ParkingSpots.Length; i++)
            {
                ParkingSpot parkingSpot = ParkingSpots[i];
                if(vehicleToAdd.Size > 1.0f)
                {
                    float sizeStillToAdd = vehicleToAdd.Size;
                    for(int j = 0;  j < vehicleToAdd.Size; j++)
                    {
                        if(j + i < ParkingSpots.Length)
                        {
                            sizeStillToAdd -= ParkingSpots[i + j].AvailableSpace;
                        }
                        
                    }
                    
                    if(sizeStillToAdd <= 0)
                    {
                        
                        parkTimers.Add(vehicleToAdd, DateTime.Now);
                        for (int j = 0; j < vehicleToAdd.Size; j++)
                        {
                            ParkingSpots[i + j].TryAddVehicle(vehicleToAdd);
                        }
                        return true;
                    }
                }
                if (ParkingSpots[i].TryAddVehicle(vehicleToAdd))
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
            OptimizeParking();
        }
        public void RemoveVehicle(string licensePlate)
        {
            Vehicle vehicleToRemove = GetVehicle(licensePlate);
            RemoveVehicle(vehicleToRemove);
        }
        private void OptimizeParking()
        {
            for (int i = 1; i < ParkingSpots.Length; i++)
            {
                ParkingSpot parkingSpot = ParkingSpots[i];
                List<Vehicle> vehicles = new (parkingSpot.Vehicles);

                foreach (Vehicle vehicle in vehicles)
                {
                    for (int j = 0; j < i; j++)
                    {
                        ParkingSpot newParkingSpot = ParkingSpots[j];

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
