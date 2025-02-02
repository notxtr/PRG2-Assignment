using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2REAL_assignment
{
    class Airline
    {
        // Attributes
        private string name;
        private string code;
        private Dictionary<string, Flight> flights = new Dictionary<string, Flight>();

        // Properties
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();

        // Default constructor
        public Airline() { }

        // Parameterized constructor
        public Airline(string n, string c)
        {
            Name = n;
            Code = c;
        }

        // Method to add flight
        public bool AddFlight(Flight f)
        {
            if (Flights.ContainsKey(f.FlightNumber)) // check if flight number already exists
            {
                return false;
            }
            else
            {
                Flights.Add(f.FlightNumber, f);
                return true;
            }
        }

        // Method to calculate fee
        public double CalculateFees()
        {
            double totalBill = 0;
            int NumOfFlights = Flights.Count; // Number of flights for the airline

            // Find fees for each flight
            foreach (KeyValuePair<string, Flight> f in Flights)
            {
                totalBill += f.Value.CalculateFees(); // set totalBill as base fee
            }

            // 3% discount for airlines with more than 5 flights
            if (NumOfFlights > 5)
            {
                totalBill = totalBill * 0.97; 
            }

            // $350 discount for every 3 flights arriving/departing
            int stacks = (int)(NumOfFlights / 3);
            totalBill = totalBill - (350 * stacks);

            // Implement other discounts 
            foreach (Flight f in Flights.Values)
            {
                if (f.ExpectedTime.TimeOfDay < new TimeSpan(11, 0, 0) || f.ExpectedTime.TimeOfDay > new TimeSpan(21, 0, 0))
                {
                    totalBill -= 110;
                }
                if (f.Origin == "DXB" || f.Origin == "BKK" || f.Origin == "NRT")
                {
                    totalBill -= 25;
                }
                if (f is CFFTFlight || f is DDJBFlight || f is LWTTFlight)
                {
                    totalBill -= 50;
                }
            }
            return totalBill;
        }

        // Method to remove flight
        public bool RemoveFlight(Flight f)
        {
            return Flights.Remove(f.FlightNumber);
        }

        // ToString method
        public override string ToString()
        {
            return $"{Name} : {Code}";
        }
    }
}
