using PRG2REAL_assignment;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Number : S10267163
// Student Name : Caden Wong
// Partner Name : Aidan Foo
//==========================================================
namespace PRG2REAL_assignment
{
    class CFFTFlight : Flight
    {
        // Attrbute
        private double RequestFee()
        {
            return 150; // Set default request fee to $150 for CFFT flights
        }

        // Default Constructor
        public CFFTFlight() { }

        // Parameterized Constructor
        public CFFTFlight(string fn, string o, string d, DateTime et, string s, Airline airline) : base(fn, o, d, et, s, airline) { }

        // Override CalculateFees method
        public override double CalculateFees()
        {
            if (Destination == "SIN")
            {
                return 500 + 300 + RequestFee();
            }
            else
            {
                return 800 + 300 + RequestFee();
            }
        }

        // ToString method 
        public override string ToString()
        {
            return base.ToString() + $"\t\t{ExpectedTime}\t\t{Status}";
        }
    }
}
