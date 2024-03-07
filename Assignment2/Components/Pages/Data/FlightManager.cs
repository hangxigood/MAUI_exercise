using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2.Components.Pages.Data
{
    internal class FlightManager
    {
        /**
       * Used to search for flights on any day of the week.
       */
        public static string WEEKDAY_ANY = "Any";
        /**
         * Used to search for flights on Sunday.
         */
        public static string WEEKDAY_SUNDAY = "Sunday";
        /**
         * Used to search for flights on Monday.
         */
        public static string WEEKDAY_MONDAY = "Monday";
        /**
         * Used to search for flights on Tuesday.
         */
        public static string WEEKDAY_TUESDAY = "Tuesday";
        /**
         * Used to search for flights on Wednesday.
         */
        public static string WEEKDAY_WEDNESDAY = "Wednesday";
        /**
         * Used to search for flights on Thursday.
         */
        public static string WEEKDAY_THURSDAY = "Thursday";
        /**
         * Used to search for flights on Friday.
         */
        public static string WEEKDAY_FRIDAY = "Friday";
        /**
         * Used to search for flights on Saturday.
         */
        public static string WEEKDAY_SATURDAY = "Saturday";

        /**
         * Used to accesse the Embedded Resource.
         */
        public static string ReadEmbeddedResource(string resourceName)
        {
            string fullResourceName = $"Assignment2.Resources.Files.{resourceName}";

            Assembly assembly = Assembly.GetExecutingAssembly();

            string resourcePath = assembly.GetManifestResourceNames()
                .FirstOrDefault(str => str.EndsWith(fullResourceName, StringComparison.CurrentCultureIgnoreCase));

            if (string.IsNullOrEmpty(resourcePath))
            {
                throw new FileNotFoundException($"Resource {fullResourceName} not found.");
            }

            using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        /**
        * Accessing the Embedded Resource(flights.csv)
        */
        public static string FLIGHTS_TEXT = ReadEmbeddedResource("flights.csv");
        /**
         * Accessing the Embedded Resource(airports.csv)
         */
        public static string AIRPORTS_TEXT = ReadEmbeddedResource("airports.csv"); 

        public static List<Flight> flights = new List<Flight>();
        public static List<string> airports = new List<string>();

        /**
         * Default constructor for FlightManager.
         */
        public FlightManager()
        {
            populateAirports();
            populateFlights();
        }

        /**
         * Gets all of the airports.
         * @return ArrayList of Airport objects.
         */
        public static List<string> GetAirports()
        {
            return airports;
        }

        /**
         * Gets all of the flights.
         * @return ArrayList of Flight objects.
         */
        public static List<Flight> GetFlights()
        {
            return flights;
        }

        /**
         * Finds an airport with code.
         * @param code Airport code
         * @return Airport object or null if none found.
         */
        public string findAirportByCode(string code)
        {
            foreach (string airport in airports)
            {
                if (airport.Equals(code))
                    return airport;
            }

            return null;
        }

        /**
         * Finds a flight with code.
         * @param code Flight code.
         * @return Flight object or null if none found.
         */
        public static Flight findFlightByCode(string code)
        {
            foreach (Flight flight in flights)
            {
                if (flight.Code.Equals(code))
                    return flight;
            }

            return null;
        }

        // TODO        
        /**
         * Finds flights going between airports on a specified weekday.
         * @param from From airport code.
         * @param to To airport code.
         * @param weekday Day of week (one of WEEKDAY_* constants). Use WEEKDAY_ANY for any day of the week.
         * @return Any found Flight objects.
         */
        public static List<Flight> findFlights(string from, string to, string weekday)
        {
            // creates a new, empty list of flights called found
            List<Flight> found = new List<Flight>();
            // iterates each flight object in Flight
            foreach (Flight flight in flights)
            {
                // If any combination is found, add the flight object to the found list. Conditions are set to allow a search based on "from, to, day", including "any" as option using the WEEKDAY_ANY constant. This allows the user more freedom when searching for a flight
                if ((from == flight.From || from == WEEKDAY_ANY) && (to == flight.To || to == WEEKDAY_ANY) &&
                    (weekday == WEEKDAY_ANY || weekday == flight.Weekday))
                {
                    found.Add(flight);
                }
            }            

            return found;
        }

        /**
         * Populates flights ArrayList with Flight objects from CSV file.
         */
        private void populateFlights()
        {
            flights.Clear();
            string[] lines = FLIGHTS_TEXT.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                int counter = 0;
                Flight flight;
                // Read the file and display it line by line.  
                foreach (string line in lines)
                {
                    string[] parts = line.Split(",");
                    string code = parts[0];
                    string airline = parts[1];
                    string from = parts[2];
                    string to = parts[3];
                    string weekday = parts[4];
                    string time = parts[5];
                    int seatsAvailable = short.Parse(parts[6]);
                    double pricePerSeat = double.Parse(parts[7]);
                    string fromAirport = findAirportByCode(from);
                    string toAirport = findAirportByCode(to);

                    try
                    {
                        flight = new Flight(code, airline, fromAirport, toAirport, weekday, time, seatsAvailable, pricePerSeat);

                        flights.Add(flight);
                    }
                    catch (Exception e)//InvalidFlightCodeException
                    {

                    }

                    counter++;
                }
            }
            catch (Exception e)
            {

            }
        }

        /**
         * Populates airports with Airport objects from CSV file.
         */
        private void populateAirports()
        {
            string[] lines = AIRPORTS_TEXT.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                int counter = 0;
                foreach (string line in lines)
                {
                    string[] parts = line.Split(",");
                    string code = parts[0];
                    string name = parts[1];
                    airports.Add(code);
                    Console.WriteLine(code);
                    Console.WriteLine("populateAirports is running");
                    counter++;
                }
            }
            catch (Exception e)
            {
            }
        } 
    }
}
