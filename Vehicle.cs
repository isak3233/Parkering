using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Parkering
{
    enum Colors
    {
        Red,
        Green,
        Blue,
        Yellow
    }
    internal abstract class Vehicle
    {
        public string LicensePlate {  get;  }
        public Colors Color { get; set; } 
        public float Size { get; set; }
        protected int padding = 8;
        public Vehicle()
        {
            LicensePlate = Helper.GetRandomLicensePlate();
            Color = Colors.Red;
        }
        public Vehicle(string licensePlate, Colors color)
        {

            LicensePlate = licensePlate;
            Color = color;
        }
        public override string ToString()
        {
            string color = Color.ToString();
            if (Helper.ColorsToSwedish(Color) != "")
            {
                color = Helper.ColorsToSwedish(Color);
            }
            return $"{LicensePlate.PadRight(10)}{color.PadRight(padding)}";
        }
    }
    internal class Car : Vehicle
    {
        public bool IsElectrical { get; set; }
        public Car() : base()
        {
            Size = 1.0f;
            IsElectrical = false;
        }
        public Car(string licensePlate, Colors color, bool isElectrical) : base(licensePlate, color)
        {
            Size = 1.0f;
            IsElectrical = isElectrical;
        }
        public override string ToString()
        {
            string baseInfo = base.ToString();
            
            return $"{"Bil".PadRight(padding)}{baseInfo}{(IsElectrical ? "Elbil" : "Förbränningsbil")}";

        }
    }
    internal class Motorcycle : Vehicle
    {
        public string Brand { get; set; }
        public Motorcycle() : base()
        {
            Size = 0.5f;
            Brand = "";
        }
        public Motorcycle(string licensePlate, Colors color, string brand) : base(licensePlate, color)
        {
            Size = 0.5f;
            Brand = brand;
        }
        public override string ToString()
        {
            string baseInfo = base.ToString();
            return $"{"MC".PadRight(padding)}{baseInfo}{Brand}";

        }
    }
    internal class Bus : Vehicle
    {
        public int AmountOfSeats { get; set; }
        public Bus() : base()
        {
            Size = 2.0f;
            AmountOfSeats = 0;
        }
        public Bus(string licensePlate, Colors color, int amountOfSeats) : base(licensePlate, color)
        {
            Size = 2.0f;
            AmountOfSeats = amountOfSeats;
        }
        public override string ToString()
        {
            string baseInfo = base.ToString();
            return $"{"Buss".PadRight(padding)}{baseInfo}{AmountOfSeats}";

        }
    }
}
