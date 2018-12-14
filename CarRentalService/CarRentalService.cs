using Logic;
using Storage;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace CarRentalService
{
    public class CarRentalService : ICarRentalService, ICarRentalServiceRest
    {
        static BusinessLogic logic = new BusinessLogic();

        public List<Car> GetCars()
        {
            return logic.GetCars();
        }

        public List<Customer> GetCustomers()
        {
            return logic.GetCustomers();
        }

        public List<Booking> GetBookings()
        {
            return logic.GetBookings();
        }

        public List<Booking> GetActiveBookings(bool isStarted)
        {
            return logic.GetActiveBookings(isStarted);
        }

        public void AddCar(CarRequest request)
        {
            if (request.userId == null || request.userId != "Admin")
            {
                throw new FaultException("You don't have permission to execute this method");
            }

            try
            {
                logic.AddCar(request.inputs.registrationNumber, request.inputs.brand, request.inputs.model, request.inputs.year);
            }
            catch (ArgumentException)
            {
                throw new FaultException("Invalid request inputs");
            }
        }

        public void RemoveCar(int carId)
        {
            try
            {
                logic.RemoveCar(carId);
            }
            catch (ArgumentException)
            {
                if (carId <= 0)
                {
                    throw new FaultException("Invalid ID input, value needs to be 1 or higher");
                }
                else
                {
                    throw new FaultException($"Car with ID {carId} was not found");
                }
            }
        }

        public void AddCustomer(CustomerRequest request)
        {
            try
            {
                logic.AddCustomer(request.inputs.firstName, request.inputs.lastName, request.inputs.telephoneNumber, request.inputs.email);
            }
            catch (ArgumentException)
            {
                throw new FaultException("Invalid request inputs");
            }
        }

        public void ChangeCustomer(ChangeCustomerRequest request)
        {
            try
            {
                logic.ChangeCustomer(request.inputs.customerId, request.inputs.newDetails);
            }
            catch (ArgumentException)
            {
                throw new FaultException("Invalid request inputs");
            }
        }

        public void RemoveCustomer(int customerId)
        {
            try
            {
                logic.RemoveCustomer(customerId);
            }
            catch (ArgumentException)
            {
                if (customerId <= 0)
                {
                    throw new FaultException("Invalid ID input, value needs to be 1 or higher");
                }
                else
                {
                    throw new FaultException($"Customer with ID {customerId} was not found");
                }
            }
        }

        public AvailableCarsResponse GetAvailableCars(AvailableCarsRequest request)
        {
            try
            {
                return new AvailableCarsResponse(logic.GetAvailableCars(request.inputs.fromDate, request.inputs.toDate));
            }
            catch (ArgumentException)
            {
                throw new FaultException("Invalid request inputs");
            }
        }

        public void CreateBooking(BookingRequest request)
        {
            try
            {
                logic.CreateBooking(request.inputs.carId, request.inputs.customerId, request.inputs.startTime, request.inputs.endTime);
            }
            catch (ArgumentException)
            {
                throw new FaultException("Invalid request inputs");
            }
        }

        public void RemoveBooking(int bookingId)
        {
            try
            {
                logic.RemoveBooking(bookingId);
            }
            catch (ArgumentException)
            {
                if (bookingId <= 0)
                {
                    throw new FaultException("Invalid ID input, value needs to be 1 or higher");
                }
                else
                {
                    throw new FaultException($"No active booking with ID {bookingId} was found");
                }
            }
        }

        public void ReturnCar(int bookingId)
        {
            try
            {
                logic.ReturnCar(bookingId);
            }
            catch (ArgumentException)
            {
                if (bookingId <= 0)
                {
                    throw new FaultException("Invalid ID input, value needs to be 1 or higher");
                }
                else
                {
                    throw new FaultException($"No active booking with ID {bookingId} was found");
                }
            }
        }

        public List<Car> GetCarsRest()
        {
            return logic.GetCars();
        }
    }
}
