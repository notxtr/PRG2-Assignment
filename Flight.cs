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
    // Abstract class
    abstract class Flight
    {
        // Attributes
        private string flightNumber;
        private string origin;
        private string destination;
        private DateTime expectedTime;
        private string status;

        // Properties
        public string FlightNumber { get { return flightNumber; } set { flightNumber = value; } }
        public string Origin { get { return origin; } set { origin = value; } }
        public string Destination { get { return destination; } set { destination = value; } }
        public DateTime ExpectedTime { get { return expectedTime; } set { expectedTime = value; } }
        public string Status { get { return status; } set { status = value; } }

        // Default Constructor 
        public Flight() { }

        // Parameterized Constructor
        public Flight(string _flightNumber, string _origin, string _destination, DateTime _expectedTime, string _status)
        {
            FlightNumber = _flightNumber;
            Origin = _origin;
            Destination = _destination;
            ExpectedTime = _expectedTime;
            Status = _status;
        }

        // Virtual method to be overridden by subclasses
        public virtual double CalculateFees()
        {
          return 0;
        }

        // ToString method 
        public override string ToString()
        {
            return $"{FlightNumber}\t{Origin}\t\t{Destination}";
        }
    }
}