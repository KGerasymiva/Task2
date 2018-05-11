using System;
using System.Collections.Generic;
using Parking.Cars;

namespace Parking
{
    internal class Parking
    {
        private static readonly Lazy<Parking> _lazy = new Lazy<Parking>(() => new Parking());

        public static Parking Instance => _lazy.Value;

        public Settings Settings { get; }

        private List<Vehicle> Cars { get; }
        public Queue<List<Trasaction>> Trasactions { get; }
        public decimal Balance { get; private set; }

        private InternalTimer InternalTimer { get; }

        public int TransactionCapacity { get; }
        public Logger Logger { get; }

        private Parking()
        {
            Settings = new Settings();
            Cars = new List<Vehicle>();
            InternalTimer = new InternalTimer(Settings.Timeout, UpdateBalance);
            TransactionCapacity = Settings.TransactionSaveTime / Settings.Timeout;
            Trasactions = new Queue<List<Trasaction>>();
            Logger = new Logger(this, Settings.TransactionSaveTime);
        }

        private void UpdateBalance()
        {

            var listTransactions = new List<Trasaction>();
            foreach (var car in Cars)
            {
                var carType = car.ToString();
                var carPrice = Settings.Prices[carType];
                decimal carUnitPayment;

                if (car.Balance >= carPrice)
                {
                    carUnitPayment = carPrice;
                }
                else if ((car.Balance > 0) && (car.Balance < carPrice))
                {
                    carUnitPayment = carPrice * (Settings.Fine + 1) - car.Balance * Settings.Fine;
                }
                else
                {
                    carUnitPayment = carPrice * (Settings.Fine + 1);
                }

                car.Balance -= carUnitPayment;
                Balance += carUnitPayment;


                var transaction = new Trasaction
                {
                    DateTime = DateTime.Now,
                    Id = car.Id,
                    Debit = carUnitPayment
                };

                listTransactions.Add(transaction);
            }

            Trasactions.Enqueue(listTransactions);
            if (Trasactions.Count > TransactionCapacity)
            {
                Trasactions.Dequeue();
            }

        }

        internal int FreeSpaces()
        {
            return Settings.ParkingSpace - Cars.Count;
        }

        public int AddCar(Vehicle vehicle)
        {
            Cars.Add(vehicle);
            return vehicle.Id;
        }

        public decimal? RemoveCar(int id)
        {
            for (var i = 0; i < Cars.Count; i++)
            {
                if (Cars[i].Id != id) continue;
                if (Cars[i].Balance > 0)
                {
                    var balance = Cars[i].Balance;
                    Cars.Remove(Cars[i]);
                    return balance;
                }

                return Cars[i].Balance;

            }

            return null;
        }

        public decimal? TopUpBalance(int id, decimal toppingup)
        {
            foreach (var car in Cars)
            {
                if (car.Id != id) continue;
                car.Balance += toppingup;
                return car.Balance;
            }

            return null;
        }

        public decimal CalcBananceForMinute()
        {
            decimal balance = 0;

            foreach (var list in Trasactions)
            {
                foreach (var transaction in list)
                {
                    balance += transaction.Debit;
                }

            }

            return balance;
        }

        public void PrintLogForMinute()
        {
            foreach (var list in Trasactions)
            {

                foreach (var trasaction in list)

                {
                    Console.WriteLine(trasaction.DateTime + " " + trasaction.Id + " {0:C} ", trasaction.Debit);
                }

            }
        }

    }
}


