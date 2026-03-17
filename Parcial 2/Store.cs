using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_2
{
    internal class Store
    {
        public Inventory Inventory { get; }

        public Store(Item firstItem, int firstQuantity)
        {
            Inventory = new Inventory();

            if (firstItem != null && firstQuantity > 0)
            {
                Inventory.AddItem(firstItem, firstQuantity);
            }
        }

        public bool AddItem(Item item, int quantity)
        {
            return Inventory.AddItem(item, quantity);
        }

        public bool HasItemsForSale()
        {
            return Inventory.HasAnyItems();
        }

        public int GetQuantity(string name, string category)
        {
            return Inventory.GetQuantity(name, category);
        }

        public bool BuyItem(Player player, string name, string category, int quantity)
        {
            if (player == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(category))
            {
                return false;
            }

            if (quantity <= 0)
            {
                return false;
            }

            string cleanName = name.Trim();
            string cleanCategory = category.Trim();

            Item item = Inventory.GetItem(cleanName, cleanCategory);

            if (item == null)
            {
                return false;
            }

            if (!Inventory.HasEnough(cleanName, cleanCategory, quantity))
            {
                return false;
            }

            int total = item.Price * quantity;

            if (!player.CanAfford(total))
            {
                return false;
            }

            if (!player.SpendGold(total))
            {
                return false;
            }

            if (!Inventory.RemoveItem(cleanName, cleanCategory, quantity))
            {
                return false;
            }

            return player.AddItem(item, quantity);
        }

        public bool BuyItems(Player player, List<string> names, List<string> categories, List<int> quantities)
        {
            if (player == null || names == null || categories == null || quantities == null)
            {
                return false;
            }

            if (names.Count == 0 || names.Count != categories.Count || names.Count != quantities.Count)
            {
                return false;
            }

            int total = 0;

            for (int i = 0; i < names.Count; i++)
            {
                if (string.IsNullOrWhiteSpace(names[i]) || string.IsNullOrWhiteSpace(categories[i]))
                {
                    return false;
                }

                if (quantities[i] <= 0)
                {
                    return false;
                }

                string cleanName = names[i].Trim();
                string cleanCategory = categories[i].Trim();

                Item item = Inventory.GetItem(cleanName, cleanCategory);

                if (item == null)
                {
                    return false;
                }

                int accumulatedQuantity = 0;

                for (int j = 0; j < names.Count; j++)
                {
                    if (names[j] != null && categories[j] != null)
                    {
                        if (names[j].Trim() == cleanName && categories[j].Trim() == cleanCategory)
                        {
                            accumulatedQuantity += quantities[j];
                        }
                    }
                }

                if (accumulatedQuantity > Inventory.GetQuantity(cleanName, cleanCategory))
                {
                    return false;
                }

                total += item.Price * quantities[i];
            }

            if (!player.CanAfford(total))
            {
                return false;
            }

            if (!player.SpendGold(total))
            {
                return false;
            }

            for (int i = 0; i < names.Count; i++)
            {
                string cleanName = names[i].Trim();
                string cleanCategory = categories[i].Trim();

                Item item = Inventory.GetItem(cleanName, cleanCategory);

                if (!Inventory.RemoveItem(cleanName, cleanCategory, quantities[i]))
                {
                    return false;
                }

                if (!player.AddItem(item, quantities[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
