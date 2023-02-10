using System;
using System.Collections.Generic;
using System.IO;

namespace VendingMachine
{
    public class VendingMachine
    {
        public Dictionary<string, Item> AllItems
        {
            get
            {
                string fileName = "vendingmachine.csv";
                string fileDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
                string filePath = Path.Combine(fileDirectory, fileName);
                Dictionary<string, Item> items = new Dictionary<string, Item>();

                using (StreamReader reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string lineItem = reader.ReadLine();

                        string[] lineParts = lineItem.Split('|');
                        string slot = lineParts[0];
                        string name = lineParts[1];
                        decimal price = Convert.ToDecimal(lineParts[2]);
                        string type = lineParts[3];

                        // Create individual item object for each item, and set properties
                        Item item = new Item(slot, name, price, type);
                        items.Add(slot, item);
                    }
                }
                return items;
            }
        }

        public decimal MachineBalance { get; set; } = 0.00M;
        public decimal CustomerBalance { get; set; } = 0.00M;
        public int Quarters { get; set; } = 0;
        public int Dimes { get; set; } = 0;
        public int Nickels { get; set; } = 0;
        public decimal CustomerChange
        {
            get
            {
                decimal balance = (.25M * Quarters) + (.10M * Dimes) + (.05M * Nickels);
                return balance;
            }
        }
        public string transactionName = string.Empty;
        public string transactionAmount = string.Empty;
        public string transactionType = string.Empty;


        ////////////////////////////////////////////////////////////////////////////////
        ///                              METHODS                                     ///
        ////////////////////////////////////////////////////////////////////////////////
        public int GetTotalItemInventory(Dictionary<string, Item> items)
        {
            int totalItemInventory = 0;
            foreach (Item item in items.Values)
            {
                totalItemInventory += item.Quantity;
            }
            return totalItemInventory;
        }

        public void DisplayItems(Dictionary<string, Item> items)
        {
            foreach (Item item in items.Values)
            {
                if (item.InStock)
                {
                    Console.WriteLine(item.Slot + " | " + item.Name + " | " + item.Price + "");
                }
                else
                {
                    Console.WriteLine(item.Slot + " | " + item.Name + " | " + item.Price + " ||| SOLD OUT");
                }
            }
            Console.WriteLine("");
        }

        public void PurchaseItem(string slotChoice)
        {
            Item itemObj = AllItems[slotChoice];
            MachineBalance += itemObj.Price;
            CustomerBalance -= itemObj.Price;
            itemObj.Quantity--;

            transactionName = $"{itemObj.Name}";
            transactionAmount = $"{itemObj.Price.ToString()}";
            transactionType = "**Sale**";
            LogTransaction(transactionName, transactionAmount, transactionType);
        }

        public void DispenseChange()
        {
            while (CustomerBalance % .25M != 0 && CustomerBalance >= .25M)
            {
                Quarters++;
                CustomerBalance -= .25M;
            }
            while (CustomerBalance % .10M != 0 && CustomerBalance >= .10M)
            {
                Dimes++;
                CustomerBalance -= .10M;

            }
            while (CustomerBalance % .05M == 0 && CustomerBalance >= .05M)
            {
                Nickels++;
                CustomerBalance -= .05M;
            }

            Console.WriteLine("-----------------------------------------");
            Console.WriteLine($"\tQuarter(s): \t{Quarters}\n" +
                $"\tDime(s): \t{Dimes}\n" +
                $"\tNickel(s): \t{Nickels}\n" +
                $"Change Total Amount: \t${CustomerChange}");
            Console.WriteLine("-----------------------------------------\n");

            transactionName = $"*Customer Funds*";
            transactionAmount = $"{CustomerChange}";
            transactionType = "Withdrawal Funds";
            LogTransaction(transactionName, transactionAmount, transactionType);

            Quarters = 0;
            Dimes = 0;
            Nickels = 0;
        }

        public void LogTransaction(string transactionName, string transactionAmount, string transactionType)
        {
            string fileName = "Log.md";
            string fileDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
            string filePath = Path.Combine(fileDirectory, fileName);

            try
            {

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    writer.WriteLine($"| {DateTime.Now.Date.ToString("MM/dd/yyyy")} | {DateTime.Now.TimeOfDay.ToString("hh\\:mm\\:ss")} | {transactionType} | {transactionName} | ${transactionAmount} | ${CustomerBalance} | ${MachineBalance} |");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("We have a problem. Please try again :)");
                Console.WriteLine(ex.Message);
            }
        }

        public void InsertMoney()
        {
            Console.Write("Insert Money:\n" +
                ">>> $");
            decimal moneyAdded = decimal.Parse(Console.ReadLine());
            Console.WriteLine("");

            CustomerBalance += moneyAdded;

            transactionName = $"*Customer Funds*";
            transactionAmount = $"{moneyAdded.ToString()}";
            transactionType = "Add Funds";
            LogTransaction(transactionName, transactionAmount, transactionType);

            Console.WriteLine("\n");
        }
    }
}

