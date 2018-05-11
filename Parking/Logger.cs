using System;
using System.IO;

namespace Parking
{
    internal class Logger
    {
        private readonly InternalTimer timer;
        private Parking Parking { get; }
        private readonly string fileName = @"Transactions.log";

        public Logger(Parking parking, int interaval)
        {
            timer = new InternalTimer(interaval, WriteLogFile);

            Parking = parking;

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }

        private void WriteLogFile()
        {
            try
            {
                using (var file = new StreamWriter(fileName, true))
                {
                    var tmpQueue = Parking.Trasactions.ToArray();

                    foreach (var list in tmpQueue)
                    {
                        if (list.Count == 0) return;
                        var dateTime = list[0].DateTime;
                        var sum = 0M;

                        foreach (var trasaction in list)
                        {
                            sum += trasaction.Debit;
                        }

                        file.WriteLine(dateTime + "     {0:C}", sum);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void PrintLogFile()
        {
            if (File.Exists(fileName))
            {
                try
                {
                    var readText = File.ReadAllText(fileName);

                    if (!string.IsNullOrEmpty(readText))
                    {
                        Console.WriteLine("\nTransactions.log:");
                        Console.WriteLine(readText);
                    }
                    else
                    {
                        Console.WriteLine("Log-file has not been formed yet.Try again later");
                    }
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("File {0} was not found", fileName);
                }
                catch (IOException)
                {
                    Console.WriteLine("IO exception");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("\nTransactions.log is empty. Try to print it later\n");
            }

        }
    }
}