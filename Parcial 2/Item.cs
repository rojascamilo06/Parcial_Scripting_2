using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_2
{
    internal class Item
    {
        private static string[] validCategories = { "Weapon", "Armor", "Accessory", "Supply" };

        public string Name { get; }
        public int Price { get; }
        public string Category { get; }

        public Item(string name, int price, string category)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("el nombre del articulo no puede estar vacio");
            }

            if (price <= 0)
            {
                throw new ArgumentException("el precio del articulo debe ser positivo");
            }

            if (!IsValidCategory(category))
            {
                throw new ArgumentException("la categoria no es valida");
            }

            Name = name.Trim();
            Price = price;
            Category = category.Trim();
        }

        private bool IsValidCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                return false;
            }

            string cleanCategory = category.Trim();
            int i;

            for (i = 0; i < validCategories.Length; i++)
            {
                if (validCategories[i] == cleanCategory)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
