namespace Parking.Cars
{
    internal abstract class Vehicle
    {
        private static int CurrentId { get; set; }
        public int Id { get; }
        public decimal Balance { get; set; }

        protected Vehicle()
        {
            Id = CurrentId++;
        }
        protected Vehicle(decimal balance) : this()
        {
            Balance = balance;
        }

        public override string ToString()
        {
            return base.ToString().Split('.')[2]; ;
        }

    }
}
