using System;
using System.Collections.Generic;

namespace Storage
{
    public class Data
    {
        public List<Car> Cars = new List<Car>();

        public List<Customer> Customers = new List<Customer>();

        public List<Booking> Bookings = new List<Booking>();

        public Data() // initialization method with start values
        {
            Cars.Add(new Car
            {
                Id = 1,
                Brand = "Volvo",
                Model = "S60",
                Year = 2014,
                RegistrationNumber = "DFY435"
            });

            Cars.Add(new Car
            {
                Id = 2,
                Brand = "BMW",
                Model = "X3",
                Year = 2013,
                RegistrationNumber = "ABC378"
            });

            Cars.Add(new Car
            {
                Id = 3,
                Brand = "Volkswagen",
                Model = "Golf GTE",
                Year = 2018,
                RegistrationNumber = "BCT569"
            });

            Cars.Add(new Car
            {
                Id = 4,
                Brand = "Toyota",
                Model = "Yaris",
                Year = 2015,
                RegistrationNumber = "HJD248"

            });

            Customers.Add(new Customer
            {
                Id = 1,
                FirstName = "Robert",
                LastName = "Andersson",
                TelephoneNumber = "0789656489",
                Email = "randersson@yahoo.se"
            });

            Customers.Add(new Customer
            {
                Id = 2,
                FirstName = "Joakim",
                LastName = "Glensson",
                TelephoneNumber = "0765874568",
                Email = "abc@yahoo.se"
            });

            Customers.Add(new Customer
            {
                Id = 3,
                FirstName = "Sara",
                LastName = "Svensson",
                TelephoneNumber = "0765874568",
                Email = "joaklars@gmail.com"
            });

            Customers.Add(new Customer
            {
                Id = 4,
                FirstName = "Victor",
                LastName = "Larsson",
                TelephoneNumber = "0798742356",
                Email = "vlarsson@gmail.com"
            });

            Bookings.Add(new Booking
            {
                Id = 1,
                Customer = Customers[0],
                Car = Cars[0],
                StartTime = new DateTime(2018, 1, 18),
                EndTime = new DateTime(2018, 1, 25),
                ReturnTime = new DateTime(2018, 1, 25)
            });

            Bookings.Add(new Booking
            {
                Id = 2,
                Customer = Customers[1],
                Car = Cars[1],
                StartTime = new DateTime(2018, 5, 1),
                EndTime = new DateTime(2018, 5, 5),
                ReturnTime = new DateTime(2018, 5, 5)
            });

            Bookings.Add(new Booking
            {
                Id = 3,
                Customer = Customers[2],
                Car = Cars[2],
                StartTime = new DateTime(2018, 3, 5),
                EndTime = new DateTime(2018, 3, 8),
                ReturnTime = new DateTime(2018, 3, 8)
            });

            Bookings.Add(new Booking
            {
                Id = 4,
                Customer = Customers[3],
                Car = Cars[3],
                StartTime = new DateTime(2018, 9, 1),
                EndTime = new DateTime(2018, 9, 10),
                ReturnTime = new DateTime(2018, 9, 10)
            });

            Bookings.Add(new Booking
            {
                Id = 5,
                Customer = Customers[0],
                Car = Cars[0],
                StartTime = new DateTime(2018, 10, 17),
                EndTime = new DateTime(2018, 10, 27),
                ReturnTime = default(DateTime)
            });
        }
    }
}