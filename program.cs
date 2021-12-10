class Plank
    {
        // Private variables of Plank class 
        private string storeName; // name of the store where the plank is stored
        private string name;      // name of the plank
        private double price;     // price of the plank in euros  
        private int area;         // area of the plank in cm2  

        // Constructor for Plank class
        public Plank(string storeName, string name, double price, int area)
        {
            this.storeName = storeName;
            this.name = name;
            this.price = price;
            this.area = area;
        }

        // Methods to get certain property of plank
        public string GetPlankStoreName() { return storeName; } // returns name of the store where the plank belongs
        public string GetPlankName()  { return name;  } // returns name of the plank
        public double GetPlankPrice() { return price; } // returns the price of the plank in euros
        public int GetPlankArea()     { return area;  } // returns the area of the plank in cm2
        
        // Methods to set value to plank property
        public void SetPlankPrice(double price) { this.price = price; } // sets the new value to plank's price
        public void SetPlankArea(int area)      { this.area = area;   } // sets the new value to plank's area
        
    }

    /// <summary>
    /// Class program to store all needed functions and run the main code
    /// </summary>
    class Program
    {
        // Constant max size
        const int CMaxsize = 20;

        // First initial data file 
        const string CInitialData1 = "L2_71.txt";

        // Second initial data file
        const string CInitialData2 = "L2_72.txt";

        // File to output results
        const string CResults = "Results.txt";

        // Method to read initial data file
        static void Read(string fn, Plank[] p, out int counter)
        {
            string name;
            double price;
            int area;
            string storeName;
            using (StreamReader reader = new StreamReader(fn))
            {
                counter = 0;
                string line = reader.ReadLine();
                storeName = line;
                string[] parts;
                int i = 0;
                while ((line = reader.ReadLine()) != null && (i <= CMaxsize))
                {
                    parts = line.Split(';');
                    name = parts[0];
                    price = double.Parse(parts[1]);
                    area = int.Parse(parts[2]);
                    p[i++] = new Plank(storeName, name, price, area);
                }
                counter = i;
            }
        }

        // Method to print initial data to the result file
        static void InitialDataToFile(string fn, Plank[] p, int counter)
        {
            using (StreamWriter writer = new StreamWriter(CResults, true))
            {
                writer.WriteLine("Initial Data about: " + p[0].GetPlankStoreName());
                writer.WriteLine("--------------------------");
                writer.WriteLine("|   Name | Price |  Area |");
                writer.WriteLine("--------------------------");
                for (int i = 0; i < counter; i++)
                {
                    writer.WriteLine("| {0, 6} | {1, 5:f2} | {2, 5} |",
                        p[i].GetPlankName(),
                        p[i].GetPlankPrice(),
                        p[i].GetPlankArea());
                }
                writer.WriteLine("--------------------------");
                writer.WriteLine();
            }
        }

        // Method to return the first most expensive in certain store
        public static Plank MostExpensive(Plank[] p, int counter)
        {
            Plank mostExpensive = p[0];
            for (int i = 0; i < counter; i++)
            {
                if (p[i].GetPlankPrice() > mostExpensive.GetPlankPrice())
                {
                    mostExpensive = p[i];
                }
            }
            return mostExpensive;
        }

        // Method to count planks that have area larger than the most expensive 
        public static int Comparison(Plank[] p, int counter1)
        {
            int numberOfLarger = 0;
            for (int i = 0; i < counter1; i++)
            {
                if (MostExpensive(p, counter1).GetPlankArea() < p[i].GetPlankArea())
                {
                    numberOfLarger++;
                }
            }
            return numberOfLarger;
        }

        // Method to print the first most expensive plank of the store and number of planks that have larger area than it
        static void WriteExpensiveToFile(string fn, Plank[] p, int counter)
        {
            using (StreamWriter writer = new StreamWriter(CResults, true))
            {
                writer.WriteLine("\nThe most expensive plank of the store: {0}",
                    MostExpensive(p, counter).GetPlankStoreName());
                writer.WriteLine("--------------------------");
                writer.WriteLine("|   Name | Price |  Area |");
                writer.WriteLine("--------------------------");
                writer.WriteLine("| {0, 6} | {1, 5:f2} | {2, 5} | ",
                    MostExpensive(p, counter).GetPlankName(),
                    MostExpensive(p, counter).GetPlankPrice(),
                    MostExpensive(p, counter).GetPlankArea());
                writer.WriteLine("--------------------------");

                writer.WriteLine("{0} plank(s) have got larger area than this plank\n", Comparison(p, counter));
            }
        }

        // Method to find the most expensives among two stores
        static double theMostExpensivePrice(Plank[] p1, Plank[] p2, int counter1, int counter2)
        {
            double priceOfMostExpensive = 0;
            if (MostExpensive(p1, counter1).GetPlankPrice() <= MostExpensive(p2, counter2).GetPlankPrice())
            {
                priceOfMostExpensive = MostExpensive(p2, counter2).GetPlankPrice();
            }
            else
            {
                priceOfMostExpensive = MostExpensive(p1, counter1).GetPlankPrice();
            }
            return priceOfMostExpensive;
        }

        // Method to print the most expensives among two stores in results file
        static void PrintingTheMostExpensives(Plank[] p, int counter, double priceOfMostExpensive)
        {
            using (StreamWriter w = new StreamWriter(CResults, true))
            {
                for (int i = 0; i < counter; i++)
                {
                    if (priceOfMostExpensive == p[i].GetPlankPrice())
                    {
                        w.WriteLine("| {0, 6} | {1, 6} | {2, 5:f2} | {3, 5} |",
                            p[i].GetPlankStoreName(),
                            p[i].GetPlankName(),
                            p[i].GetPlankPrice(),
                            p[i].GetPlankArea());
                    }
                }
            }
        }

        // Method to fill the third collection with planks that undergo minimum requirement for area set by user
        static void LargerThanXCollection(Plank[] p, Plank[] p3, int counter, ref int counter3, double setLargeRequirement)
        {
            for(int i = 0; i < counter; i++)
            {
                if (setLargeRequirement < p[i].GetPlankArea())
                {
                    string storeName = p[i].GetPlankStoreName();
                    string name = p[i].GetPlankName();
                    double price = p[i].GetPlankPrice();
                    int area = p[i].GetPlankArea();
                    p3[counter3] = new Plank(storeName, name, price, area);
                    counter3++;
                }
            }
        }
        
        // Method to print the third collection in results file
        static void LargerThanXCollectionPrint(string fn, Plank[] p, int counter, double setLargeRequirement, int counter3)
        {
            using (StreamWriter writer = new StreamWriter(CResults, true))
            {
                if (counter3 == 0)
                {
                    writer.WriteLine("No plank with the area larger than {0} cm2 is found", setLargeRequirement);
                }
                else 
                {
                    writer.WriteLine("Data about planks that are larger than the entered {0} cm2", setLargeRequirement);
                    writer.WriteLine("-----------------------------------");
                    writer.WriteLine("|  Store |   Name | Price |  Area |");
                    writer.WriteLine("-----------------------------------");
                    for (int i = 0; i < counter; i++)
                    {
                        writer.WriteLine("| {0, 6} | {1, 6} | {2, 5:f2} | {3, 5} |",
                                p[i].GetPlankStoreName(),
                                p[i].GetPlankName(),
                                p[i].GetPlankPrice(),
                                p[i].GetPlankArea());
                    }
                    writer.WriteLine("-----------------------------------");
                    writer.WriteLine();
                }
            }
        }

        // Main part of the program where all methods are used and the whole program runs
        static void Main(string[] args)
        {
            // Creation and filling the first Plank class collection
            int counter1;
            Plank[] p1 = new Plank[CMaxsize];
            Read(CInitialData1, p1, out counter1);

            // Creation and filling the second Plank class collection
            int counter2;
            Plank[] p2 = new Plank[CMaxsize];
            Read(CInitialData2, p2, out counter2);

            // Clearing the results file to avoid overflowing
            if (File.Exists(CResults))
            {
                File.Delete(CResults);
            }

            // Printing the first Plank class collection's initial data
            InitialDataToFile(CResults, p1, counter1);
            // Printing the second Plank class collection's initial data
            InitialDataToFile(CResults, p2, counter2);

            // Printing the first Plank class collection's the first most expensive plank and planks that have larger area than it
            WriteExpensiveToFile(CResults, p1, counter1);
            // Printing the second Plank class collection's the first most expensive plank and planks that have larger area than it
            WriteExpensiveToFile(CResults, p2, counter2);

            // Printing the beginning of the most expensives among two stores table 
            using (StreamWriter writer = new StreamWriter(CResults, true))
            {
                writer.WriteLine("\nThe most expensives in General");
                writer.WriteLine("-----------------------------------");
                writer.WriteLine("|  Store |   Name | Price |  Area |");
                writer.WriteLine("-----------------------------------");
            }

            // Printing the main data about the most expensives among two stores
            PrintingTheMostExpensives(p1, counter1, theMostExpensivePrice(p1, p2, counter1, counter2));
            PrintingTheMostExpensives(p2, counter2, theMostExpensivePrice(p1, p2, counter1, counter2));

            // Printing the end of the most expensives among two stores table
            using (StreamWriter writer = new StreamWriter(CResults, true))
            {
                writer.WriteLine("-----------------------------------\n");
            }

            // Creation of the third Plank class collection
            int counter3 = 0;
            Plank[] p3 = new Plank[CMaxsize];

            // Asking user to enter the minimum area and recording it
            Console.WriteLine("Please enter the minimum Area to sort necessary planks");
            double setLargeRequirement = double.Parse(Console.ReadLine());
            
            // Avoiding negative value area giving by the user
            if (setLargeRequirement >= 0)
            {
                // Filling the third Plank class collection
                LargerThanXCollection(p1, p3, counter1, ref counter3, setLargeRequirement);
                LargerThanXCollection(p2, p3, counter2, ref counter3, setLargeRequirement);

                // Printing the third Plank class collection
                LargerThanXCollectionPrint(CResults, p3, counter3, setLargeRequirement, counter3);
            }
            else 
            {
                Console.WriteLine("No negative set Area is accepted");
            }

            // Printing the console that the program has finished its work
            Console.WriteLine("Program has finished");
        }
    }
