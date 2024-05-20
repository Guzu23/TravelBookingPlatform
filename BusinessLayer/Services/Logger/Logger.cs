using BusinessLayer.Contracts;
using DataAccessLayer.Models;

namespace BusinessLayer.Services.Logger
{
    public class Logger : ILogger
    {
        public void Log(string logPath, string log)
        {
            Console.Write(log);
            File.AppendAllText(logPath, log);
        }
        public string GetLogPath()
        {
            string logPath = $"../BusinessLayer/Logs/{DateTime.Now.Day}.{DateTime.Now.Month}.{DateTime.Now.Year}.log";

            if (!File.Exists(logPath))
            {
                File.WriteAllText(logPath, "");
            }

            string log = "[" + DateTime.Now.ToString() + "]: ";

            Log(logPath, log);

            return logPath;
        }

        public string AirlineToString(Airline airline)
        {
            return
                "     Id:               " + airline.Id + "\n" +
                "     Name:             " + airline.Name + "\n" +
                "     Location:         " + airline.Location + "\n" +
                "     ChairsLeft:       " + airline.ChairsLeft + "\n";
        }
        public string FlightReservationToString(FlightReservation flightReservation)
        {
            return
                "     Id:                         " + flightReservation.Id + "\n" +
                "     IdAirline:                  " + flightReservation.IdAirline + "\n" +
                "     IdUser:                     " + flightReservation.IdUser + "\n" +
                "     Chair:                      " + flightReservation.Chair + "\n" +
                "     FlighDate:                  " + flightReservation.FlightDate + "\n" +
                "     FlightDuration:             " + flightReservation.FlightDuration + "\n" +
                "     CheckInFlightLocation:      " + flightReservation.CheckInFlightLocation + "\n" +
                "     CheckOutFlightLocation:     " + flightReservation.CheckOutFlightLocation + "\n" +
                "     Price:                      " + flightReservation.Price + "\n";
        }
        public string HotelReservationToString(HotelReservation hotelReservation)
        {
            return
                "     Id:             " + hotelReservation.Id + "\n" +
                "     IdAirline:      " + hotelReservation.IdHotel + "\n" +
                "     IdUser:         " + hotelReservation.IdUser + "\n" +
                "     RoomType:       " + hotelReservation.RoomType + "\n" +
                "     CheckIn:        " + hotelReservation.CheckIn + "\n" +
                "     CheckOut:       " + hotelReservation.CheckOut + "\n" +
                "     Price:          " + hotelReservation.Price + "\n";
        }
        public string HotelToString(Hotel hotel)
        {
            return
                "     Id:             " + hotel.Id + "\n" +
                "     Name:           " + hotel.Name + "\n" +
                "     Location:       " + hotel.Location + "\n" +
                "     RoomsLeft:      " + hotel.RoomsLeft + "\n";
        }
        public string UserToString(User user)
        {
            return
                "     Id:               " + user.Id + "\n" +
                "     Username:         " + user.Username + "\n" +
                "     Email:            " + user.Email + "\n" +
                "     Active:           " + user.Active + "\n" +
                "     ImageProfile:     " + user.ImageProfile + "\n";
        }

        //Get Requests from DB
        public void LogAirlineRequestFromDB(Airline airline)
        {
            string logPath = GetLogPath();
            string log =
                "Get Request of an Airline from the database:\n" +
                AirlineToString(airline);
            Log(logPath, log);
        }
        public void LogFlightReservationRequestFromDB(FlightReservation flightReservation)
        {
            string logPath = GetLogPath();
            string log =
                "Get Request of a FlightReservation from the database:\n" +
                FlightReservationToString(flightReservation);
            Log(logPath, log);
        }
        public void LogHotelReservationRequestFromDB(HotelReservation hotelReservation)
        {
            string logPath = GetLogPath();
            string log =
                "Get Request of a HotelReservation from the database:" + "\n" +
                HotelReservationToString(hotelReservation);
            Log(logPath, log);
        }
        public void LogHotelRequestFromDB(Hotel hotel)
        {
            string logPath = GetLogPath();
            string log =
                "Get Request of a Hotel from the database:" + "\n" +
                HotelToString(hotel);
            Log(logPath, log);
        }
        public void LogUserRequestFromDB(User user)
        {
            string logPath = GetLogPath();
            string log =
                "Get Request of an User from the database:" + "\n" +
                UserToString(user);
            Log(logPath, log);
        }



        //Insert Requests to DB
        public void LogAirlineInsertRequestToDB(Airline airline)
        {
            string logPath = GetLogPath();
            string log =
                "Insert Request of an Airline to the database:\n" +
                AirlineToString(airline);
            Log(logPath, log);
        }
        public void LogFlightReservationInsertRequestToDB(FlightReservation flightReservation)
        {
            string logPath = GetLogPath();
            string log =
                "Insert Request of a FlightReservation to the database:\n" +
                FlightReservationToString(flightReservation);
            Log(logPath, log);
        }
        public void LogHotelReservationInsertRequestToDB(HotelReservation hotelReservation)
        {
            string logPath = GetLogPath();
            string log =
                "Insert Request of a HotelReservation to the database:" + "\n" +
                HotelReservationToString(hotelReservation);
            Log(logPath, log);
        }
        public void LogHotelInsertRequestToDB(Hotel hotel)
        {
            string logPath = GetLogPath();
            string log =
                "Insert Request of a Hotel to the database:" + "\n" +
                HotelToString(hotel);
            Log(logPath, log);
        }
        public void LogUserInsertRequestToDB(User user)
        {
            string logPath = GetLogPath();
            string log =
                "Insert Request of an User to the database:" + "\n" +
                UserToString(user);
            Log(logPath, log);
        }


        //Update Requests in DB
        public void LogAirlineUpdateRequestInDB(Airline oldAirline, Airline newAirline)
        {
            string logPath = GetLogPath();
            string log =
                "Update Request of an Airline from the database:\n" +
                "  Old Airline:\n" +
                AirlineToString(oldAirline) +
                "  New Airline:\n" +
                AirlineToString(newAirline); ;
            Log(logPath, log);
        }
        public void LogFlightReservationUpdateRequestInDB(FlightReservation oldFlightReservation, FlightReservation newFlightReservation)
        {
            string logPath = GetLogPath();
            string log =
                "Update Request of a FlightReservation from the database:\n" +
                "  Old Airline:\n" +
                FlightReservationToString(oldFlightReservation) +
                "  New Airline:\n" +
                FlightReservationToString(newFlightReservation);
            Log(logPath, log);
        }
        public void LogHotelReservationUpdateRequestInDB(HotelReservation oldHotelReservation, HotelReservation newHotelReservation)
        {
            string logPath = GetLogPath();
            string log =
                "Update Request of a HotelReservation from the database:" + "\n" +
                "  Old Airline:\n" +
                HotelReservationToString(oldHotelReservation) +
                "  New Airline:\n" +
                HotelReservationToString(newHotelReservation);
            Log(logPath, log);
        }
        public void LogHotelUpdateRequestInDB(Hotel oldHotel, Hotel newHotel)
        {
            string logPath = GetLogPath();
            string log =
                "Update Request of a Hotel from the database:" + "\n" +
                "  Old Airline:\n" +
                HotelToString(oldHotel) +
                "  New Airline:\n" +
                HotelToString(newHotel);
            Log(logPath, log);
        }
        public void LogUserUpdateRequestInDB(User oldUser, User newUser)
        {
            string logPath = GetLogPath();
            string log =
                "Update Request of an User from the database:" + "\n" +
                "  Old Airline:\n" +
                UserToString(oldUser) +
                "  New Airline:\n" +
                UserToString(newUser);
            Log(logPath, log);
        }



        //Delete Requests from DB
        public void LogAirlineDeleteRequestFromDB(Airline airline)
        {
            string logPath = GetLogPath();
            string log =
                "Delete Request of an Airline from the database:\n" +
                AirlineToString(airline);
            Log(logPath, log);
        }
        public void LogFlightReservationDeleteRequestFromDB(FlightReservation flightReservation)
        {
            string logPath = GetLogPath();
            string log =
                "Delete Request of a FlightReservation from the database:\n" +
                FlightReservationToString(flightReservation);
            Log(logPath, log);
        }
        public void LogHotelReservationDeleteRequestFromDB(HotelReservation hotelReservation)
        {
            string logPath = GetLogPath();
            string log =
                "Delete Request of a HotelReservation from the database:" + "\n" +
                HotelReservationToString(hotelReservation);
            Log(logPath, log);
        }
        public void LogHotelDeleteRequestFromDB(Hotel hotel)
        {
            string logPath = GetLogPath();
            string log =
                "Delete Request of a Hotel from the database:" + "\n" +
                HotelToString(hotel);
            Log(logPath, log);
        }
        public void LogUserDeleteRequestFromDB(User user)
        {
            string logPath = GetLogPath();
            string log =
                "Delete Request of an User from the database:" + "\n" +
                UserToString(user);
            Log(logPath, log);
        }


    }
}
