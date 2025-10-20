using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkering
{
    internal class Vehicle
    {
        public string LicensePlate {  get;  }
        public Vehicle(string licensePlate)
        {
            LicensePlate = licensePlate;
        }
    }
}
