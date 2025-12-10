using System;
using System.Collections.Generic;
using System.IO;

namespace CakeProject
{
    // -------------------------
    //  CLASSES
    // -------------------------

    public class Design
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Design(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class Decoration
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Decoration(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class CakeOrder
    {
        public string CustomerName { get; set; }
        public int DesignId { get; set; }
        public int DecorationId { get; set; }

        public CakeOrder(string customerName, int designId, int decorationId)
        {
            CustomerName = customerName;
            DesignId = designId;
            DecorationId = decorationId;
        }
    }

    // -------------------------
    //  PROGRAM
    // -------------------------

    class Program
    {
        static void Main(string[] args)
        {
            string designFile = "Design.csv";
            string decorationFile = "Decorations.csv";

            List<Design> designs = LoadDesigns(designFile);
            List<Decoration> decorations = LoadDecorations(decorationFile);
            List<CakeOrder> orders = new List<CakeOrder>();

            Console.WriteLine("=== Available Designs ===");
            foreach (var d in designs)
                Console.WriteLine($"{d.Id}: {d.Name}");

            Console.WriteLine("\n=== Available Decorations ===");
            foreach (var d in decorations)
                Console.WriteLine($"{d.Id}: {d.Name}");

            // -------------------------
            //  TAKE ORDERS LOOP
            // -------------------------

            string again = "y";

            while (again.ToLower() == "y")
            {
                Console.Write("\nEnter customer name: ");
                string? name = Console.ReadLine();

                Console.Write("Enter Design ID: ");
                int designId = SafeIntInput();

                Console.Write("Enter Decoration ID: ");
                int decorationId = SafeIntInput();

                orders.Add(new CakeOrder(name ?? "Unknown", designId, decorationId));

                Console.Write("Add another order? (y/n): ");
                again = Console.ReadLine() ?? "n";
            }

            // -------------------------
            // DISPLAY ALL ORDERS
            // -------------------------

            Console.WriteLine("\n=== All Cake Orders ===");
            foreach (var order in orders)
            {
                string designName = designs.Find(d => d.Id == order.DesignId)?.Name ?? "Unknown";
                string decorationName = decorations.Find(d => d.Id == order.DecorationId)?.Name ?? "Unknown";

                Console.WriteLine(
                    $"\nCustomer: {order.CustomerName}\nDesign: {designName}\nDecoration: {decorationName}"
                );
            }

            Console.WriteLine("\nProgram finished. Press any key to exit...");
            Console.ReadKey();
        }

        // -------------------------
        //  LOADING CSV FUNCTIONS
        // -------------------------

        static List<Design> LoadDesigns(string fileName)
        {
            List<Design> list = new List<Design>();

            if (!File.Exists(fileName))
            {
                Console.WriteLine($"ERROR: File {fileName} not found.");
                return list;
            }

            foreach (var line in File.ReadAllLines(fileName))
            {
                var parts = line.Split(',');
                if (parts.Length == 2 &&
                    int.TryParse(parts[0], out int id))
                {
                    list.Add(new Design(id, parts[1]));
                }
            }

            return list;
        }

        static List<Decoration> LoadDecorations(string fileName)
        {
            List<Decoration> list = new List<Decoration>();

            if (!File.Exists(fileName))
            {
                Console.WriteLine($"ERROR: File {fileName} not found.");
                return list;
            }

            foreach (var line in File.ReadAllLines(fileName))
            {
                var parts = line.Split(',');
                if (parts.Length == 2 &&
                    int.TryParse(parts[0], out int id))
                {
                    list.Add(new Decoration(id, parts[1]));
                }
            }

            return list;
        }

        // -------------------------
        //  SAFE INTEGER INPUT
        // -------------------------

        static int SafeIntInput()
        {
            int number;
            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.Write("Invalid number. Try again: ");
            }
            return number;
        }
    }
}
