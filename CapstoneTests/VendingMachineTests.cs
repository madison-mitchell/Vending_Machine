using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Capstone;

namespace CapstoneTests
{
	[TestClass]
	public class VendingMachineTests
	{
		[TestMethod]
		public void VendingMachineConstructor_ValidParameters_SetProperties()
		{
			Dictionary<string, Item> expectedItems = new Dictionary<string, Item>();
            expectedItems.Add("A1", new Item("A1", "Yellow Duck", .90M, "Duck"));
            expectedItems.Add("A2", new Item("A2", "Cube Duck", 2.50M, "Duck"));
            expectedItems.Add("A3", new Item("A3", "Beach Duck", 1.50M, "Duck"));
            expectedItems.Add("A4", new Item("A4", "Bat Duck", 2.00M, "Duck"));
            expectedItems.Add("B1", new Item("B1", "Emperor Penguin", 2.80M, "Penguin"));
            expectedItems.Add("B2", new Item("B2", "Macaroni Penguin", 2.25M, "Penguin"));
            expectedItems.Add("B3", new Item("B3", "Rockhopper Penguin", 1.50M, "Penguin"));
            expectedItems.Add("B4", new Item("B4", "Galapagos Penguin", 1.25M, "Penguin"));
            expectedItems.Add("C1", new Item("C1", "Black Cat", 2.25M, "Cat"));
            expectedItems.Add("C2", new Item("C2", "White Cat", 2.50M, "Cat"));
            expectedItems.Add("C3", new Item("C3", "Tabby Cat", 2.50M, "Cat"));
            expectedItems.Add("C4", new Item("C4", "Calico Cat", 3.55M, "Cat"));
            expectedItems.Add("D1", new Item("D1", "Unicorn Pony", 1.95M, "Pony"));
            expectedItems.Add("D2", new Item("D2", "Pegasus Pony", 1.85M, "Pony"));
            expectedItems.Add("D3", new Item("D3", "Horse", .90M, "Pony"));
            expectedItems.Add("D4", new Item("D4", "Rainbow Horse", 1.35M, "Pony"));

            VendingMachine vm = new VendingMachine();
            Dictionary<string, Item> actualItems = vm.AllItems;

            Assert.IsTrue(expectedItems.Count == actualItems.Count);
            foreach (KeyValuePair<string, Item> kvp in expectedItems)
            {
                Assert.IsTrue(actualItems.ContainsKey(kvp.Key));
                Assert.AreEqual(kvp.Value.Slot, actualItems[kvp.Key].Slot);
                Assert.AreEqual(kvp.Value.Name, actualItems[kvp.Key].Name);
                Assert.AreEqual(kvp.Value.Price, actualItems[kvp.Key].Price);
                Assert.AreEqual(kvp.Value.Type, actualItems[kvp.Key].Type);
            }
        }
    }
}

