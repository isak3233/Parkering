using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkering
{
    enum Colors
    {
        Red,
        Green,
        Blue
    }
    internal class Vehicle
    {
        public string LicensePlate {  get;  }
        public Colors Color {  get; } 
        public Vehicle(string licensePlate, Colors color)
        {
            LicensePlate = licensePlate;
            Color = color;
        }
        public virtual string GetInformation()
        {
            return $"{LicensePlate}\t{Helper.ColorsToSwedish(Color)}";
        }
    }
    internal class Car : Vehicle
    {
        public bool IsElectrical { get; }
        public Car(string licensePlate, Colors color, bool isElectrical) : base(licensePlate, color)
        {
            IsElectrical = isElectrical;
        }
        public override string GetInformation()
        {
            string baseInfo = base.GetInformation();
            return $"Bil\t{baseInfo}\t{(IsElectrical ? "Elbil" : "Förbränningsbil")}";

        }
    }
    internal class Motorcycle : Vehicle
    {
        public string Brand { get; }
        public Motorcycle(string licensePlate, Colors color, string brand) : base(licensePlate, color)
        {
            Brand = brand;
        }
        public override string GetInformation()
        {
            string baseInfo = base.GetInformation();
            return $"MC\t{baseInfo}\t{Brand}";

        }
    }
    internal class Bus : Vehicle
    {
        public int AmountOfSeats { get; }
        public Bus(string licensePlate, Colors color, int amountOfSeats) : base(licensePlate, color)
        {
            AmountOfSeats = amountOfSeats;
        }
        public override string GetInformation()
        {
            string baseInfo = base.GetInformation();
            return $"MC\t{baseInfo}\t{AmountOfSeats}";

        }
    }
}
