using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Capstone;

namespace CapstoneTests
{
	[TestClass]
	public class ItemTests
	{
		[TestMethod]
		public void ItemConstructor() 
		{
			string slot = "A1";
			string name = "Tabby";
			decimal price = 2.50M;
			string type = "cat";
			int initialInventory = 5;

			Item item = new Item(slot, name, price, type);

            Assert.AreEqual("A1", item.Slot);
            Assert.AreEqual("Tabby", item.Name);
            Assert.AreEqual(2.50M, item.Price);
            Assert.AreEqual("cat", item.Type);
            Assert.AreEqual(5, item.Quantity);
        }
	}
}

