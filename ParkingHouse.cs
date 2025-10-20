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
            //Bus bus1 = new Bus("BBB222", Colors.Red, 55);
            //Bus bus2 = new Bus("SSS333", Colors.Green, 44);
            //Car car1 = new Car("ABC123", Colors.Red, false);
            //Car car2 = new Car("ABC124", Colors.Red, false);
            //Motorcycle motorcycle1 = new Motorcycle("AAA111", Colors.Green, "Yamaha");
            //Motorcycle motorcycle2 = new Motorcycle("AAA222", Colors.Green, "Yamaha");
            //Motorcycle motorcycle3 = new Motorcycle("AAA333", Colors.Green, "Yamaha");
            //Motorcycle motorcycle4 = new Motorcycle("AAA444", Colors.Green, "Yamaha");
            //TryAddVehicleToParkHouse(bus1);
            //TryAddVehicleToParkHouse(bus2);
            //TryAddVehicleToParkHouse(motorcycle1);
            //TryAddVehicleToParkHouse(car1);
            //TryAddVehicleToParkHouse(motorcycle2);
            //TryAddVehicleToParkHouse(motorcycle3);
            //TryAddVehicleToParkHouse(car2);
            //TryAddVehicleToParkHouse(motorcycle4);


        }
        public bool TryAddVehicleToParkHouse(Vehicle vehicleToAdd)
        {
            for(int i = 0;i < ParkingSpots.Count; i++)
            {
                ParkingSpot parkingSpot = ParkingSpots[i];
                if(vehicleToAdd.Size > 1.0f)
                {
                    if(parkingSpot.AvailableSpace == 1.0f && ParkingSpots[i+1].AvailableSpace == 1.0f)
                    {
                        vehicleToAdd.TimeWhenParked = DateTime.Now;
                        ParkingSpots[i].TryAddVehicle(vehicleToAdd);
                        ParkingSpots[i+1].TryAddVehicle(vehicleToAdd);
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
        public void RemoveVehicle(Vehicle vehicleToRemove)
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
            for (int i = 0; i < ParkingSpots.Count;i++)
            {
                ParkingSpot parkingSpot = ParkingSpots[i];
                if(parkingSpot.Vehicles.Count == 0)
                {
                    Console.WriteLine($"Plats {i + 1}\t Tom");
                }
                foreach (Vehicle vehicle in parkingSpot.Vehicles)
                {
                    if (vehicle is Bus)
                    {
                        Console.WriteLine($"Plats {i+1}-{i+2}\t {vehicle.GetInformation()}");
                        i += 1;
                    } else
                    {
                        Console.WriteLine($"Plats {i+1}\t\t {vehicle.GetInformation()}");
                    }
                        
                    
                }
            }
        }
    }
}
