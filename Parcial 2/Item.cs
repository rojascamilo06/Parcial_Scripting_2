using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_2
{
    public class Item
    {
        private static string[] validCategories = { "Weapon", "Armor", "Accessory", "Supply" };

        public string Name { get; }
        public int Price { get; }
        public string Category { get; }

        public Item(string name, int price, string category)
        {
            Name = name == null ? "" : name.Trim();
            Price = price;
            Category = category == null ? "" : category.Trim();
        }

        public bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return false;
            }

            if (Price <= 0)
            {
                return false;
            }

            for (int i = 0; i < validCategories.Length; i++)
            {
                if (validCategories[i] == Category)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
