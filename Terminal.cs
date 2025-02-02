using System;
using System.Collections.Generic;
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
    class Terminal
    {
        // Attributes
        private string terminalName;
        private Dictionary<string, Airline> airlines = new Dictionary<string, Airline>();
        private Dictionary<string, Flight> flights = new Dictionary<string, Flight>();
        private Dictionary<string, BoardingGate> boardingGates = new Dictionary<string, BoardingGate>();
        private Dictionary<string, double> gateFees = new Dictionary<string, double>();

        // Properties
        public string TerminalName
        {
            get { return terminalName; }
            set { terminalName = value; }
        }
        public Dictionary<string, Airline> Airlines { get; set; } = new Dictionary<string, Airline>();
        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();
        public Dictionary<string, BoardingGate> BoardingGates { get; set; } = new Dictionary<string, BoardingGate>();
        public Dictionary<string, double> GateFees { get; set; } = new Dictionary<string, double>();

        // Default constructor
        public Terminal() { }

        // Parameterized constructor
        public Terminal(string tn)
        {
            TerminalName = tn;
        }

        // Method to add airline
        public bool AddAirline(Airline a)
        {
            if (airlines.ContainsKey(a.Code)) // check if airline code already exists
            {
                return false;
            }
            else
            {
                airlines.Add(a.Code, a);
                return true;
            }
        }

        // Method to add boarding gate
        public bool AddBoardingGate(BoardingGate bd)
        {
            if (BoardingGates.ContainsKey(bd.GateName)) // check if boarding gate already exists
                return false;
            else
            {
                BoardingGates.Add(bd.GateName, bd);
                return true;
            }
        }

        // Method to get airline from flight 
        public Airline GetAirlineFromFlight(Flight f)
        {
            foreach (Airline a in airlines.Values)
            {
                if (a.Flights.ContainsKey(f.FlightNumber))
                {
                    return a;
                }
            }
            return null;
        }

        // Method to print airline fees 
        public void PrintAirlineFees()
        {
            foreach(Airline a in airlines.Values)
            {
                Console.WriteLine(a.CalculateFees());
            }
        }

        // ToString method
        public override string ToString()
        {
            return $"{TerminalName}";
        }
    }
}
