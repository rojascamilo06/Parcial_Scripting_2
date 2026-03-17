using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_2
{
    internal class Inventory
    {
        private List<Item> items = new List<Item>();
        private List<int> quantities = new List<int>();

        public int EquipmentCount
        {
            get
            {
                int count = 0;

                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].Category != "Supply")
                    {
                        count++;
                    }
                }

                return count;
            }
        }

        public int SupplyCount
        {
            get
            {
                int count = 0;

                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].Category == "Supply")
                    {
                        count++;
                    }
                }

                return count;
            }
        }

        public bool AddItem(Item item, int quantity)
        {
            if (item == null || !item.IsValid())
            {
                return false;
            }

            if (quantity < 0)
            {
                return false;
            }

            int index = FindIndex(item.Name, item.Category);

            if (index >= 0)
            {
                if (items[index].Price != item.Price)
                {
                    return false;
                }

                quantities[index] += quantity;
                return true;
            }

            items.Add(item);
            quantities.Add(quantity);
            return true;
        }

        public bool RemoveItem(string name, string category, int quantity)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(category))
            {
                return false;
            }

            if (quantity <= 0)
            {
                return false;
            }

            int index = FindIndex(name.Trim(), category.Trim());

            if (index < 0)
            {
                return false;
            }

            if (quantities[index] < quantity)
            {
                return false;
            }

            quantities[index] -= quantity;
            return true;
        }

        public int GetQuantity(string name, string category)
        {
            int index = FindIndex(name, category);
            return index >= 0 ? quantities[index] : 0;
        }

        public Item GetItem(string name, string category)
        {
            int index = FindIndex(name, category);
            return index >= 0 ? items[index] : null;
        }

        public bool HasEnough(string name, string category, int quantity)
        {
            return quantity > 0 && GetQuantity(name, category) >= quantity;
        }

        public bool HasAnyItems()
        {
            foreach (int quantity in quantities)
            {
                if (quantity > 0)
                {
                    return true;
                }
            }

            return false;
        }

        private int FindIndex(string name, string category)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(category))
            {
                return -1;
            }

            string cleanName = name.Trim();
            string cleanCategory = category.Trim();

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Name == cleanName && items[i].Category == cleanCategory)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
