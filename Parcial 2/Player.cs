using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parcial_2
{
    internal class Player
    {
        public int Gold { get; private set; }
        public Inventory Inventory { get; }

        public Player(int gold)
        {
            Gold = gold < 0 ? 0 : gold;
            Inventory = new Inventory();
        }

        public bool CanAfford(int amount)
        {
            return amount >= 0 && Gold >= amount;
        }

        public bool SpendGold(int amount)
        {
            if (amount < 0)
            {
                return false;
            }

            if (amount > Gold)
            {
                return false;
            }

            Gold -= amount;
            return true;
        }

        public bool AddItem(Item item, int quantity)
        {
            return Inventory.AddItem(item, quantity);
        }
    }
}
