using System;
using System.IO;
using System.Collections.Generic;

namespace VendingMachine
{
    class Program
    {
        public static string fileName = "vendingmachine.csv";
        public static string fileDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
        public static string filePath = Path.Combine(fileDirectory, fileName);


        static void Main(string[] args)
        {
            VendingMachine vm = new VendingMachine();
            Dictionary<string, Item> items = vm.AllItems;
            int totalItemInventory = vm.GetTotalItemInventory(items);
            int input = 0;

            Console.WriteLine("Animal Vending Machine\n" +
                "WELCOME!\n\n");

            // Loops through Main Menu and options while there is inventory
            while (totalItemInventory > 0)
            {
                Console.Write($"BALANCE: ${vm.CustomerBalance}\n" +
                    "-----------------------------------------\n" +
                    "Make your selection: \n" +
                    "(1) Display Vending Machine Items\n" +
                    "(2) Purchase\n" +
                    "(3) Exit\n" +
                    ">>> ");

                input = int.Parse(Console.ReadLine());
                Console.WriteLine("\n\n");

                // Display items
                if (input == 1)
                {
                    vm.DisplayItems(items);
                }
                // Purchase item
                else if (input == 2)
                {
                    // Loop through when input isn't "Finish Transaction"
                    while (input != 3)
                    {
                        // Menu
                        Console.Write($"BALANCE: ${vm.CustomerBalance} \n" +
                            "-----------------------------------------\n" +
                            "(1) Feed Money\n" +
                            "(2) Select Product\n" +
                            "(3) Finish Transaction\n" +
                            ">>>  ");
                        input = int.Parse(Console.ReadLine());
                        Console.WriteLine("\n\n");

                        // Insert Money
                        if (input == 1)
                        {
                            vm.InsertMoney();
                        }
                        // Purchase Product
                        else if (input == 2)
                        {
                            // Display Items
                            vm.DisplayItems(items);

                            // Enter slot choice
                            Console.Write("Choose your item slot:\n" +
                                ">>> ");
                            string slotChoice = Console.ReadLine();
                            Console.WriteLine("\n");

                            // Grab item by slot choice
                            Item item = items[slotChoice];

                            // Checks if item is not in stock and displays message
                            if (!item.InStock)
                            {
                                Console.WriteLine($"{item.Name} OUT OF STOCK.\n Please make a different selection");
                            }
                            // Checks if customer inserted enough money to complete purchase and prompts to add more money
                            else if (vm.CustomerBalance < item.Price)
                            {
                                Console.WriteLine($"Insufficent funds. An additional ${-(vm.CustomerBalance - item.Price)} is need to complete the purchase.");
                                vm.InsertMoney();
                                vm.PurchaseItem(slotChoice);

                                Console.WriteLine("-----------------------------------------");
                                Console.WriteLine(item.Sound());
                                Console.WriteLine("-----------------------------------------");
                                Console.WriteLine($"Remaining Balance:\t\t${vm.CustomerBalance}");
                                Console.WriteLine($"Remaining {item.Name}(s):\t${item.Price} ({item.Quantity})");
                                Console.WriteLine("-----------------------------------------");
                            }
                            // If in stock and enough available funds, completes purchase
                            else
                            {
                                vm.PurchaseItem(slotChoice);

                                Console.WriteLine("-----------------------------------------");
                                Console.WriteLine(item.Sound());
                                Console.WriteLine("-----------------------------------------");
                                Console.WriteLine($"Remaining Balance:\t\t${vm.CustomerBalance}");
                                Console.WriteLine($"Remaining {item.Name}(s):\t${item.Price} ({item.Quantity})");
                                Console.WriteLine("-----------------------------------------");
                            }
                        }

                        // Exit Purchase Menu
                        else
                        {
                            vm.DispenseChange();
                            break;
                        }
                        Console.WriteLine("");
                    }
                }
                // End vending machine menu
                else
                {
                    Console.Write("Thank you for using Animal Vending Machine!");
                    break;
                }
            }
        }
    }
}