using Storage;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace CarRentalService
{
    [DataContract]
    public class CarInputs
    {
        [DataMember]
        public string registrationNumber;

        [DataMember]
        public string brand;

        [DataMember]
        public string model;

        [DataMember]
        public int year;
    }

    [DataContract]
    public class CustomerInputs
    {
        [DataMember]
        public string firstName;

        [DataMember]
        public string lastName;

        [DataMember]
        public string telephoneNumber;

        [DataMember]
        public string email;
    }

    [DataContract]
    public class ChangeCustomerInputs
    {
        [DataMember]
        public int customerId;

        [DataMember]
        public Customer newDetails;
    }

    [DataContract]
    public class DateInputs
    {
        [DataMember]
        public DateTime fromDate;

        [DataMember]
        public DateTime toDate;
    }

    [DataContract]
    public class BookingInputs
    {
        [DataMember]
        public int carId;

        [DataMember]
        public int customerId;

        [DataMember]
        public DateTime startTime;

        [DataMember]
        public DateTime endTime;
    }

    [MessageContract]
    public class CarRequest
    {
        [MessageHeader]
        public string userId;

        [MessageBodyMember]
        public CarInputs inputs;
    }

    [MessageContract]
    public class CustomerRequest
    {
        [MessageHeader]
        public string userId;

        [MessageBodyMember]
        public CustomerInputs inputs;
    }

    [MessageContract]
    public class ChangeCustomerRequest
    {
        [MessageHeader]
        public string userId;

        [MessageBodyMember]
        public ChangeCustomerInputs inputs;
    }

    [MessageContract]
    public class AvailableCarsRequest
    {
        [MessageHeader]
        public string userId;

        [MessageBodyMember]
        public DateInputs inputs;
    }

    [MessageContract]
    public class AvailableCarsResponse
    {
        public AvailableCarsResponse() { }

        public AvailableCarsResponse(List<Car> result)
        {
            this.result = result;
        }

        [MessageBodyMember]
        public List<Car> result;
    }

    [MessageContract]
    public class BookingRequest
    {
        [MessageHeader]
        public string userId;

        [MessageBodyMember]
        public BookingInputs inputs;
    }

    [ServiceContract]
    public interface ICarRentalService
    {
        [OperationContract]
        List<Car> GetCars();

        [OperationContract]
        List<Customer> GetCustomers();

        [OperationContract]
        List<Booking> GetBookings();

        [OperationContract]
        List<Booking> GetActiveBookings(bool isStarted);

        [OperationContract]
        void AddCar(CarRequest request);

        [OperationContract]
        void RemoveCar(int carId);

        [OperationContract]
        void AddCustomer(CustomerRequest request);

        [OperationContract]
        void ChangeCustomer(ChangeCustomerRequest request);

        [OperationContract]
        void RemoveCustomer(int customerId);

        [OperationContract]
        AvailableCarsResponse GetAvailableCars(AvailableCarsRequest request);

        [OperationContract]
        void CreateBooking(BookingRequest request);

        [OperationContract]
        void RemoveBooking(int bookingId);

        [OperationContract]
        void ReturnCar(int bookingId);
    }
}
