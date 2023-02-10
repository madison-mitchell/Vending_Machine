using System;

namespace VendingMachine
{
	public class Item
	{
        public string Slot { get; set; }
		public string Name { get; set; }
		public decimal Price { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }
        public bool InStock
        {
            get
            {
                if(Quantity > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Item(string slot, string name, decimal price, string type)
        {
            Slot = slot;
            Name = name;
            Price = price;
            Type = type;
            Quantity = 5;
        }

        public string Sound()
        {
            string sound = string.Empty;
            sound = Type.ToLower().Equals("duck") ? "Quack, Quack, Splash!" :
                Type.ToLower().Equals("penguin") ? "Squawk, Squawk, Whee!" :
                Type.ToLower().Equals("cat") ? "Meow, Meow, Meow!" :
                "Neigh, Neigh, Yay!";

            return sound;
        }
    }
}

