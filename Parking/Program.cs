using System;

namespace Parking
{
    class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                var menu = new Menu(Parking.Instance);
                while (menu.Show() ) {}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
