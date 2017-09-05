using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CaseyRPGLib.Models
{
    public class Backpack
    {
        public enum BackpackSlot
        {
            TopLeft = 0,
            TopMid = 1,
            TopRight = 2,
            BottomLeft = 3,
            BottomMid = 4,
            BottomRight = 5
        }

        private Dictionary<BackpackSlot, Item> items;

        internal Dictionary<BackpackSlot, Item> Items { get => items; set => items = value; }

        public Backpack()
        {
            items = new Dictionary<BackpackSlot, Item>(6);
            items.Add(BackpackSlot.TopLeft, null);
            items.Add(BackpackSlot.TopMid, null);
            items.Add(BackpackSlot.TopRight, null);
            items.Add(BackpackSlot.BottomLeft, null);
            items.Add(BackpackSlot.BottomMid, null);
            items.Add(BackpackSlot.BottomRight, null);
        }

        public int Add(Item item)
        {
            for (int i = 0; i < 6; i++)
            {
                if (items[(BackpackSlot)i] == null)
                {
                    return Add(item, (BackpackSlot)i);
                }
            }
            throw new BackpackFullException();
        }

        public int Add(Item item, BackpackSlot slot)
        {
            if (items[slot] != null)
                throw new BackpackSlotOccupiedException();

            items[slot] = item;
            return (int)slot;
        }

        public Item RemoveAt(BackpackSlot slot)
        {
            Item temp = items[slot];
            items[slot] = null;
            return temp;
        }

        public string ToDetailsString()
        {
            string s = string.Empty;
            s += $"\n\tBackpack: {{";
            Items.Where(r => r.Value != null).ToList().ForEach(w => s += $"\n\t\tSlot: {w.Key.ToString()}" + w.Value.ToDetailsString());
            s += $"\n\t}}";
            return s;
        }
    }

    [Serializable]
    internal class BackpackSlotOccupiedException : Exception
    {
        public BackpackSlotOccupiedException()
        {
        }

        public BackpackSlotOccupiedException(string message) : base(message)
        {
        }

        public BackpackSlotOccupiedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    [Serializable]
    internal class BackpackFullException : Exception
    {
        public BackpackFullException()
        {
        }

        public BackpackFullException(string message) : base(message)
        {
        }

        public BackpackFullException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
