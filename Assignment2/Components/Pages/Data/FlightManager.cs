using System;
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
        * The location of the flights text database file.
        */
        public static string FLIGHTS_TEXT = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\..\Resources\Files\flights.csv");
        /**
         * The location of the airports text database file.
         */
        /* Example of absolute and relative path */
        // TODO
        // define the airports file path  
        // ...................................
        public static string AIRPORTS_TEXT = "";    // TODO (Update the path)

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
        public List<string> getAirports()
        {
            return airports;
        }

        /**
         * Gets all of the flights.
         * @return ArrayList of Flight objects.
         */
        public static List<Flight> getFlights()
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

        /**
         * Finds flights going between airports on a specified weekday.
         * @param from From airport code.
         * @param to To airport code.
         * @param weekday Day of week (one of WEEKDAY_* constants). Use WEEKDAY_ANY for any day of the week.
         * @return Any found Flight objects.
         */
        public static List<Flight> findFlights(string from, string to, string weekday)
        {
            List<Flight> found = new List<Flight>();

           // TODO
           // find all flights that match the input arguments  
           // ...................................

            return found;
        }

        /**
         * Populates flights ArrayList with Flight objects from CSV file.
         */
        private void populateFlights()
        {
            flights.Clear();
            try
            {
                int counter = 0;
                Flight flight;
                // Read the file and display it line by line.  
                foreach (string line in File.ReadLines(FLIGHTS_TEXT))
                {
                    Console.WriteLine(line);

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
            try
            {
                int counter = 0;
                foreach (string line in File.ReadLines(AIRPORTS_TEXT))
                {
                    string[] parts = line.Split(",");

                    string code = parts[0];
                    string name = parts[1];
                    airports.Add(code);

                    counter++;
                }
            }
            catch (Exception e)
            {
            }
        } 
    }
}
