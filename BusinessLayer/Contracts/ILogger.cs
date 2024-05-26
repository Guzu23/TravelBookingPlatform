using DataAccessLayer.Models;

namespace BusinessLayer.Contracts
{
    public interface ILogger
    {
        public void Log(string logPath, string log);
        public string GetLogPath();

        //Error handling
        public void LogErrorSomethingWentWrongWithAirline(Guid id);
        public void LogErrorSomethingWentWrongWithFlightReservation(Guid id);
        public void LogErrorSomethingWentWrongWithHotelReservation(Guid id);
        public void LogErrorSomethingWentWrongWithHotel(Guid id);
        public void LogErrorSomethingWentWrongWithUser(Guid id);

        //Warning handling
        public void LogWarningAirlineNotExisting(Guid id);
        public void LogWarningFlightReservationNotExisting(Guid id);
        public void LogWarningHotelReservationNotExisting(Guid id);
        public void LogWarningHotelNotExisting(Guid id);
        public void LogWarningUserNotExisting(Guid id);

        //Get Requests from DB
        public void LogAirlineRequestFromDB(Airline airline);
        public void LogFlightReservationRequestFromDB(FlightReservation flightReservation);
        public void LogHotelReservationRequestFromDB(HotelReservation hotelReservation);
        public void LogHotelRequestFromDB(Hotel hotel);
        public void LogUserRequestFromDB(User user);

        //Insert Requests to DB
        public void LogAirlineInsertRequestToDB(Airline airline);
        public void LogFlightReservationInsertRequestToDB(FlightReservation flightReservation);
        public void LogHotelReservationInsertRequestToDB(HotelReservation hotelReservation);
        public void LogHotelInsertRequestToDB(Hotel hotel);
        public void LogUserInsertRequestToDB(User user);


        //Update Requests in DB
        public void LogAirlineUpdateRequestInDB(Airline oldAirline, Airline newAirline);
        public void LogFlightReservationUpdateRequestInDB(FlightReservation oldFlightReservation, FlightReservation newFlightReservation);
        public void LogHotelReservationUpdateRequestInDB(HotelReservation oldHotelReservation, HotelReservation newHotelReservation);
        public void LogHotelUpdateRequestInDB(Hotel oldHotel, Hotel newHotel);
        public void LogUserUpdateRequestInDB(User oldUser, User newUser);


        //Delete Requests from DB
        public void LogAirlineDeleteRequestFromDB(Airline airline);
        public void LogFlightReservationDeleteRequestFromDB(FlightReservation flightReservation);
        public void LogHotelReservationDeleteRequestFromDB(HotelReservation hotelReservation);
        public void LogHotelDeleteRequestFromDB(Hotel hotel);
        public void LogUserDeleteRequestFromDB(User user);


    }
}
