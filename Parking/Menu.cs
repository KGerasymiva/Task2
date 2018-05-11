using System;
using Parking.Cars;

namespace Parking
{
    internal class Menu
    {
        private Parking Parking { get; }

        public Menu(Parking parking)
        {
            Parking = parking;
        }

        public bool Show()
        {
            Console.WriteLine("Welcome! You are working with PARKING SYSTEM\n");
            Console.WriteLine("Please, enter your commnand:\n");
            Console.WriteLine("{0,-41}\t{1,-55}\t{2,-33}", "1 - Add car", "2 - Remove car", "3 - Top up car balance");
            Console.WriteLine("{0,-41}\t{1,-55}\t{2,-33}", "4 - List of transactions for last minute", "5 - Total parking income", "6 - Number of free parking spaces");
            Console.WriteLine("{0,-41}\t{1,-55}\t{2,-33}", "7 - Print Transactions.log", "8 - The amount of money earned for the last minute", "9 - Exit\n");
            if (int.TryParse(Console.ReadLine(), out var result) && result >= 1 && result <= 9)
            {
                switch (result)
                {
                    case 1: //Add car
                        Console.WriteLine("\nPlease, enter number of car type: 1 - Truck, 2 - Passenger, 3 - Bus, 4 - Motorcycle");
                        if (int.TryParse(Console.ReadLine(), out result) && result >= 1 && result <= 4)
                        {
                            CreateCarMenu(result);
                        }
                        else
                        {
                            Console.WriteLine("\nEntered invalid value");
                        }

                        break;
                    case 2: // Remove car
                        Console.WriteLine("\nPlease, enter number car's Id");
                        if (int.TryParse(Console.ReadLine(), out var id) && id >= 0)
                        {
                            var balance = Parking.RemoveCar(id);
                            if (balance.HasValue && balance.Value > 0)
                            {
                                Console.WriteLine("\nThe car with Id {0} has been successfully removed", id);
                            }
                            else if (balance.HasValue && balance.Value < 0)
                            {
                                Console.WriteLine("\nThe car with Id {0} has negative balance=-{1:C} can't be removed", id, balance.Value);
                            }
                            else
                            {
                                Console.WriteLine("\nThere is no car with Id {0} in the database", id);
                            }

                        }
                        else
                        {
                            Console.WriteLine("\nEntered invalid car's Id");
                        }
                        break;
                    case 3: // Top up balance
                        Console.WriteLine("\nPlease, enter number car's Id");
                        if (int.TryParse(Console.ReadLine(), out id) && id >= 0)
                        {
                            Console.WriteLine("\nPlease, enter the amount of balance topping up (from 1$ to 10 000$)");
                            if (decimal.TryParse(Console.ReadLine(), out var toppingup) && toppingup >= 1 &&
                                toppingup <= 10000)
                            {
                                var balance = Parking.TopUpBalance(id, toppingup);
                                Console.WriteLine(

                                    balance.HasValue
                                        ? "\nThe balance of the car with Id {0} has been successfully topped up. Balance = {1:C}"
                                        : "\nThere is no car with Id {0} in the database", id, balance);
                            }
                            else
                            {
                                Console.WriteLine("\nEntered invalid amount of balance topping up");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nEntered invalid car's Id");
                        }
                        break;
                    case 4: // Вывести истории транзакций за последнюю минуту.
                        if (Parking.Trasactions.Count == Parking.TransactionCapacity)
                        {
                            Console.WriteLine("\nList of transactions for the last minute:");
                            Parking.PrintLogForMinute();
                        }
                        else
                        {
                            var estTime = (Parking.TransactionCapacity - Parking.Trasactions.Count) * Parking.Settings.Timeout / 1000;
                            Console.WriteLine("\nList of transactions for the last minute has not been formed yet. Try again later in {0} seconds ", estTime);
                        }

                        break;
                    case 5: // Вывести общий доход парковки.
                        Console.WriteLine("\nTotal parking income = {0:C}", Parking.Balance);
                        break;
                    case 6: // Вывести количество свободных мест на парковке.
                        Console.WriteLine("\nNumber of free parking spaces ={0}", Parking.FreeSpaces());
                        break;
                    case 7: //Вывести Transactions.log (отформатировать вывод)
                        Parking.Logger.PrintLogFile();
                        break;
                    case 8: //сумму заработанных средств за последнюю минуту
                        Console.WriteLine("\nThe amount of money earned for the last minute = {0:C}", Parking.CalcBananceForMinute());

                        break;
                    case 9: //Exit;
                        return false;
                    default:
                        return true;

                }
            }
            else
            {
                Console.WriteLine("\nEntered invalid command");

            }
            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
            Console.Clear();
            return true;

        }

        private void CreateCarMenu(int result)
        {
            Console.WriteLine("Please, enter car balance (from 10$ to 10 000$)");
            if (decimal.TryParse(Console.ReadLine(), out var balance) && balance >= 10 && balance <= 10000)
            {
                Vehicle vehicle = null;

                switch (result)
                {
                    case 1:
                        vehicle = new Truck(balance);
                        NewMethod(vehicle, balance);

                        break;
                    case 2:
                        vehicle = new Passenger(balance);
                        NewMethod(vehicle, balance);
                        break;
                    case 3:
                        vehicle = new Bus(balance);
                        NewMethod(vehicle, balance);
                        break;
                    case 4:
                        vehicle = new Motorcycle(balance);
                        NewMethod(vehicle, balance);
                        break;
                }
            }
            else
            {
                Console.WriteLine("Entered invalid balance value ");
            }

        }

        private void NewMethod(Vehicle vehicle, decimal balance)
        {
            Console.WriteLine("The {2} with ID {0} has been added with balance {1:C}", Parking.AddCar(vehicle), balance, vehicle);
        }
    }
}
