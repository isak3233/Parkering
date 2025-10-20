using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parkering
{
    internal class Helper
    {
        static public string ColorsToSwedish(Colors color)
        {
            string swedishText = "";
            switch(color)
            {
                case Colors.Red:
                    swedishText = "Röd";
                    break;
                case Colors.Green:
                    swedishText = "Grön";
                    break;
                case Colors.Blue:
                    swedishText = "Blå";
                    break;
                default:
                    break;
            }
            return swedishText;
        }
    }
}
