using CarRentalConsoleClient.CarRentalService;
using Storage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarRentalConsoleClient
{
    class Program
    {
        static CarRentalService.CarRentalServiceClient client = 
            new CarRentalService.CarRentalServiceClient("BasicHttpBinding_ICarRentalService");

        static string[] Commands = new string[] { "Quit", "Clear",
            "AddCar", "RemoveCar",
            "AddCustomer", "ChangeCustomer", "RemoveCustomer",
            "GetAvailableCars", "CreateBooking", "RemoveBooking", "ReturnCar" };

        static readonly string UserId = "Admin";

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("[COMMANDS]");
                foreach (string s in Commands)
                {
                    Console.WriteLine(s);
                }
                Console.WriteLine();
                string input = Console.ReadLine();
                if (!Commands.Any(c => c.ToLower() == input.ToLower()))
                {
                    Console.WriteLine($"\"{input}\" is not a valid command, choose an option from the command list");
                }
                else
                {
                    input = input.ToLower();
                    ExecuteCommand(input);
                }
                if (input != "clear")
                {
                    Console.WriteLine(); // adds some spacing between command input
                }
            }
        }

        static void ExecuteCommand(string command)
        {
            try
            {
                switch (command)
                {
                    case "quit":
                        Environment.Exit(0);
                        return;

                    case "clear":
                        Console.Clear();
                        break;

                    case "addcar":
                        client.AddCar(UserId, new CarInputs
                        {
                            brand = GetStringParameterInput("brand"),
                            model = GetStringParameterInput("model"),
                            registrationNumber = GetStringParameterInput("registration number"),
                            year = GetIntParameterInput("year")
                        });
                        Console.WriteLine("Added car");
                        break;

                    case "removecar":
                        client.RemoveCar(GetObjectFromListInput(client.GetCars().ToList(), "car").Id);
                        Console.WriteLine("Removed car");
                        break;

                    case "addcustomer":
                        client.AddCustomer(UserId, new CustomerInputs
                        {
                            firstName = GetStringParameterInput("first name"),
                            lastName = GetStringParameterInput("last name"),
                            email = GetStringParameterInput("email"),
                            telephoneNumber = GetStringParameterInput("telephone number")
                        });
                        Console.WriteLine("Added customer");
                        break;

                    case "changecustomer":
                        client.ChangeCustomer(UserId, new ChangeCustomerInputs {
                            customerId = GetObjectFromListInput(client.GetCustomers().ToList(), "customer").Id,
                            newDetails = new Customer
                            {
                                FirstName = GetStringParameterInput("first name"),
                                LastName = GetStringParameterInput("last name"),
                                TelephoneNumber = GetStringParameterInput("telephone number"),
                                Email = GetStringParameterInput("email")
                            }
                        });
                        Console.WriteLine("Changed customer");
                        break;

                    case "removecustomer":
                        client.RemoveCustomer(GetObjectFromListInput(client.GetCustomers().ToList(), "customer").Id);
                        Console.WriteLine("Removed customer");
                        break;

                    case "getavailablecars":
                        Car[] cars = client.GetAvailableCars(UserId, new DateInputs {
                            fromDate = GetDateTimeParameterInput("from date"),
                            toDate = GetDateTimeParameterInput("to date")
                        });
                        if (cars.Length == 0)
                        {
                            Console.WriteLine("No available cars");
                        }
                        else
                        {
                            Console.WriteLine("[AVAILABLE CARS]");
                            foreach (Car c in cars)
                            {
                                Console.WriteLine(c.ToString());
                            }
                        }
                        break;

                    case "createbooking":
                        DateTime fromDate = GetDateTimeParameterInput("booking start time");
                        DateTime toDate = GetDateTimeParameterInput("booking end time");
                        client.CreateBooking(UserId, new BookingInputs {
                            customerId = GetObjectFromListInput(client.GetCustomers().ToList(), "customer").Id,
                            carId = GetObjectFromListInput(client.GetAvailableCars(UserId, new DateInputs {
                                fromDate = fromDate, toDate = toDate
                            }).ToList(), "car").Id,
                            startTime = fromDate,
                            endTime = toDate
                        });
                        Console.WriteLine("Created booking");
                        break;

                    case "removebooking":
                        client.RemoveBooking(GetObjectFromListInput(client.GetActiveBookings(false).ToList(), "booking").Id);
                        Console.WriteLine("Removed booking");
                        break;

                    case "returncar":
                        client.ReturnCar(GetObjectFromListInput(client.GetActiveBookings(true).ToList(), "booking").Id);
                        Console.WriteLine("Returned car");
                        break;

                    default:
                        Console.WriteLine($"There's no current support for command \"{command}\"");
                        break;
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid method parameters.");
            }
            catch (EmptyListException e)
            {
                Console.WriteLine("Failed to execute command. " + e.Message);
            }
        }

        public static string GetStringParameterInput(string parameterName)
        {
            while (true)
            {
                Console.WriteLine($"Assign the \"{parameterName}\" parameter (Type: string)");
                string input = Console.ReadLine();
                if (input.Length > 0)
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("A valid input is required");
                }
            }
        }

        public static int GetIntParameterInput(string parameterName)
        {
            while (true)
            {
                Console.WriteLine($"Assign the \"{parameterName}\" parameter (Type: int)");
                string input = Console.ReadLine();
                if (input.Length > 0)
                {
                    if (Int32.TryParse(input, out int number))
                    {
                        return number;
                    }
                    else
                    {
                        Console.WriteLine("Wrong input, a valid number is required");
                    }
                }
                else
                {
                    Console.WriteLine("A valid input is required");
                }
            }
        }

        public static DateTime GetDateTimeParameterInput(string parameterName)
        {
            while (true)
            {
                Console.WriteLine($"Assign the \"{parameterName}\" parameter (Type: DateTime - assign value in format year-month-day)");
                string input = Console.ReadLine();
                if (input.Length > 0)
                {
                    if (DateTime.TryParse(input, out DateTime date))
                    {
                        return date;
                    }
                    else
                    {
                        Console.WriteLine("Wrong input, a valid date is required (format: year-month-day)");
                    }
                }
                else
                {
                    Console.WriteLine("A valid input is required");
                }
            }
        }

        public static T GetObjectFromListInput<T>(List<T> list, string parameterName)
        {
            if (list.Count == 0)
            {
                throw new EmptyListException($"There's no available \"{parameterName}\" choices for this command.");
            }
            while (true)
            {
                Console.WriteLine($"Assign the \"{parameterName}\" parameter by selecting a number from the list");
                for (int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine((i + 1) + ": " + list[i].ToString());
                }
                string input = Console.ReadLine();
                if (input.Length > 0)
                {
                    if (Int32.TryParse(input, out int number))
                    {
                        if (number >= 1 && number <= (list.Count))
                        {
                            return list[number - 1];
                        }
                        else
                        {
                            Console.WriteLine("Wrong input, enter a number from 1 to " + (list.Count));
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wrong input, a valid number is required");
                    }
                }
                else
                {
                    Console.WriteLine("A valid input is required");
                }
            }
        }
    }
}