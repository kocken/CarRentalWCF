using Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class BusinessLogic
    {
        Data Data = new Data();

        public List<Car> GetCars()
        {
            return Data.Cars;
        }

        public List<Customer> GetCustomers()
        {
            return Data.Customers;
        }

        public List<Booking> GetBookings()
        {
            return Data.Bookings;
        }

        public List<Booking> GetActiveBookings(bool isStarted)
        {
            return Data.Bookings.Where(b =>
            b.ReturnTime == default(DateTime) && (isStarted ? b.IsStarted() : !b.IsStarted())).ToList();
        }

        public void AddCar(string registrationNumber, string brand, string model, int year)
        {
            if (registrationNumber == null || registrationNumber.Length == 0 ||
                brand == null || brand.Length == 0 ||
                model == null || model.Length == 0 ||
                year < 1900 || year > DateTime.Now.Year)
            {
                throw new ArgumentException();
            }

            List<Car> cars = GetCars();
            int highestCarId = cars.Count > 0 ? cars.Max(i => i.Id) : 0;

            Data.Cars.Add(
                new Car
                {
                    Id = highestCarId+1,
                    RegistrationNumber = registrationNumber,
                    Brand = brand,
                    Model = model,
                    Year = year
                }
            );
        }

        public void RemoveCar(int carId)
        {
            Car car = Data.Cars.SingleOrDefault(c => c.Id == carId);
            if (car == null)
            {
                throw new ArgumentException();
            }

            Data.Cars.Remove(car);
        }

        public void AddCustomer(string firstName, string lastName, string telephoneNumber, string email)
        {
            List<Customer> customers = GetCustomers();
            int highestCustomerId = customers.Count > 0 ? customers.Max(i => i.Id) : 0;

            Customer customer = new Customer
            {
                Id = highestCustomerId+1,
                FirstName = firstName,
                LastName = lastName,
                TelephoneNumber = telephoneNumber,
                Email = email
            };
            if (!customer.IsValid())
            {
                throw new ArgumentException();
            }

            Data.Customers.Add(customer);
        }

        public void ChangeCustomer(int customerId, Customer newDetails)
        {
            Customer customer = Data.Customers.SingleOrDefault(c => c.Id == customerId);
            newDetails.Id = customerId;

            List<Customer> customers = GetCustomers();

            if (customer == null || !customers.Contains(customer) || newDetails == null || !newDetails.IsValid())
            {
                throw new ArgumentException();
            }
            
            int index = customers.FindIndex(c => c == customer);
            customers[index] = newDetails;
        }

        public void RemoveCustomer(int customerId)
        {
            Customer customer = Data.Customers.SingleOrDefault(c => c.Id == customerId);

            if (customer == null || !Data.Customers.Contains(customer))
            {
                throw new ArgumentException();
            }

            Data.Customers.Remove(customer);
        }

        public List<Car> GetAvailableCars(DateTime fromDate, DateTime toDate)
        {
            if (fromDate == null || toDate == null || fromDate > toDate)
            {
                throw new ArgumentException();
            }

            List<Car> tmp = Data.Cars.ToList(); // all cars, ToList() to make a new temporary & seperate list and not refer to the real list
            // the LINQ expression below gets bookings that are during the "fromDate" and "toDate" parameters
            List<Booking> currentActiveBookings = Data.Bookings.Where(b =>
                (b.StartTime >= fromDate && b.StartTime <= toDate || // if booking start time is within the fromDate to toDate timespan
                b.EndTime >= fromDate && b.EndTime <= toDate || // if booking end time is within the fromDate to toDate timespan
                b.StartTime <= fromDate && b.EndTime >= toDate || // if booking covers the whole timespan
                b.EndTime <= fromDate) && // if booking should be over and car returned
                b.ReturnTime == default(DateTime)) // if customer have not returned car
                .ToList();
            foreach (Booking b in currentActiveBookings)
            {
                if (tmp.Contains(b.Car)) // failsafe
                {
                    tmp.Remove(b.Car);
                }
            }
            return tmp; // available cars (with occupied ones from bookings removed)
        }

        public void CreateBooking(int carId, int customerId, DateTime startTime, DateTime endTime)
        {
            Car car = Data.Cars.SingleOrDefault(c => c.Id == carId);

            Customer customer = Data.Customers.SingleOrDefault(c => c.Id == customerId);

            List<Car> availableCars = GetAvailableCars(startTime, endTime);

            if (car == null || customer == null || startTime == null || endTime == null || startTime > endTime ||
                !Data.Cars.Contains(car) || !Data.Customers.Contains(customer) ||
                !availableCars.Contains(car)) // makes sure car is available
            {
                throw new ArgumentException();
            }

            List<Booking> bookings = GetBookings();
            int highestBookingId = bookings.Count > 0 ? bookings.Max(i => i.Id) : 0;

            Data.Bookings.Add(
                new Booking
                {
                    Id = highestBookingId+1,
                    Car = car,
                    Customer = customer,
                    StartTime = startTime,
                    EndTime = endTime,
                    ReturnTime = default(DateTime)
                }
            );
        }

        public void RemoveBooking(int bookingId)
        {
            Booking booking = Data.Bookings.SingleOrDefault(b => b.Id == bookingId);

            if (booking == null || !Data.Bookings.Contains(booking) ||
                DateTime.Now >= booking.StartTime && booking.ReturnTime == default(DateTime)) // booking is active and did not return car
            {
                throw new ArgumentException();
            }

            Data.Bookings.Remove(booking);
        }

        public void ReturnCar(int bookingId)
        {
            Booking booking = Data.Bookings.SingleOrDefault(b => b.Id == bookingId);

            if (booking == null || !Data.Bookings.Contains(booking) ||
                booking.ReturnTime != default(DateTime)) // already returned car
            {
                throw new ArgumentException();
            }

            booking.ReturnTime = DateTime.Now;
        }
    }
}