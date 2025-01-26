//==========================================================
// Student Number : S10267163
// Student Name : Caden Wong
// Partner Name : Aidan Foo
//==========================================================

void DisplayMenu()
{
    Console.WriteLine("=============================================\r\nWelcome to Changi Airport Terminal 5\r\n=============================================\r\n1. List All Flights\r\n2. List Boarding Gates\r\n3. Assign a Boarding Gate to a Flight\r\n4. Create Flight\r\n5. Display Airline Flights\r\n6. Modify Flight Details\r\n7. Display Flight Schedule\r\n0. Exit");
}

void CreateFlightObject()
{

}

List<Airline> airlineList = new List<Airline>();
bool trueornot = true;
while (trueornot == true)
{
    DisplayMenu();
    Console.WriteLine("Please select your option: ");
    int option = Convert.ToInt32(Console.ReadLine());
    switch (option)
    {
        case 0:
            trueornot = false;
            Console.WriteLine("Goodbye!");  
            break;

        case 1:
            break;
        case 2:
            break;
        case 3:
            break;
        case 4:
            break;
        case 5:
            break;
        case 6:
            break;
        case 7:
            break;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;

    }
}

