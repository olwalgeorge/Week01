using System;
using System.IO;
using System.Text;

namespace FilesAndDirectories
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create directory if it doesn't exist
            var storesDirectory = "stores";
            if (!Directory.Exists(storesDirectory))
            {
                Directory.CreateDirectory(storesDirectory);
            }

            // Display all files in the stores directory
            Console.WriteLine("Files in stores directory:");
            foreach (string file in Directory.EnumerateFiles(storesDirectory))
            {
                Console.WriteLine(file);
            }

            // Find total of all sales files
            var totalSales = FindSalesTotal(storesDirectory);
            Console.WriteLine($"\nTotal Sales: {totalSales:C}");

            // Generate sales summary report
            GenerateSalesSummaryReport(storesDirectory);
            Console.WriteLine("\nSales summary report generated: SalesSummary.txt");
        }

        static double FindSalesTotal(string salesDirectory)
        {
            double salesTotal = 0;
            
            // Get all .txt files in the directory
            string[] salesFiles = Directory.GetFiles(salesDirectory, "*.txt");

            foreach (string file in salesFiles)
            {
                string salesText = File.ReadAllText(file);
                if (double.TryParse(salesText, out double salesAmount))
                {
                    salesTotal += salesAmount;
                }
            }

            return salesTotal;
        }

        static void GenerateSalesSummaryReport(string salesDirectory)
        {
            var reportBuilder = new StringBuilder();
            double totalSales = 0;
            
            // Header
            reportBuilder.AppendLine("Sales Summary");
            reportBuilder.AppendLine("----------------------------");

            // Get all sales files and calculate total
            string[] salesFiles = Directory.GetFiles(salesDirectory, "*.txt");
            var fileDetails = new List<(string fileName, double amount)>();

            foreach (string file in salesFiles)
            {
                string salesText = File.ReadAllText(file);
                if (double.TryParse(salesText, out double salesAmount))
                {
                    totalSales += salesAmount;
                    string fileName = Path.GetFileName(file);
                    fileDetails.Add((fileName, salesAmount));
                }
            }

            // Add total to report
            reportBuilder.AppendLine($" Total Sales: {totalSales:C}");
            reportBuilder.AppendLine();
            reportBuilder.AppendLine(" Details:");

            // Add individual file details
            foreach (var (fileName, amount) in fileDetails)
            {
                reportBuilder.AppendLine($"  {fileName}: {amount:C}");
            }

            // Write report to file
            File.WriteAllText("SalesSummary.txt", reportBuilder.ToString());
        }
    }
}
