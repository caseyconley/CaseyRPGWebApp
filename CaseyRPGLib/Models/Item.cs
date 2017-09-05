using System;
using System.Collections.Generic;
using System.Text;

namespace CaseyRPGLib.Models
{
    public abstract class Item
    {
        private string name;
        private string description;
        private ItemProperties properties;

        public ItemProperties Properties { get => properties; protected set => properties = value; }
        public string Description { get => description; protected set => description = value; }
        public string Name { get => name; protected set => name = value; }

        public string ToDetailsString()
        {
            string s = string.Empty;

            s += $"\n\t\tItem {{" +
                $"\n\t\t\tName: {Name}" +
                $"\n\t\t\tDescription: {Description}";
            if (Properties.stats_add_health != null)
                s += $"\n\t\t\tHealth: {DecimalWithSign(Properties.stats_add_health)}";
            if (Properties.stats_mult_healthregen != null)
                s += $"\n\t\t\tHealth Regen: {DecimalWithSign(Properties.stats_mult_healthregen)}";
            if (Properties.stats_add_mana != null)
                s += $"\n\t\t\tMana: {DecimalWithSign(Properties.stats_add_mana)}";
            if (Properties.stats_mult_manaregen != null)
                s += $"\n\t\t\tMana Regen: {DecimalWithSign(Properties.stats_mult_manaregen)}";
            if (Properties.stats_add_damage != null)
                s += $"\n\t\t\tDamage: {DecimalWithSign(Properties.stats_add_damage)}";
            if (Properties.stats_add_strength != null)
                s += $"\n\t\t\tStrength: {DecimalWithSign(Properties.stats_add_strength)}";
            if (Properties.stats_add_agility != null)
                s += $"\n\t\t\tAgility: {DecimalWithSign(Properties.stats_add_agility)}";
            if (Properties.stats_add_intelligence != null)
                s += $"\n\t\t\tIntelligence: {DecimalWithSign(Properties.stats_add_intelligence)}";

            s += $"\n\t\t}}";

            return s;
        }

        private string DecimalWithSign(decimal? d)
        {
            if (!d.HasValue)
                return "NULL";
            return d > 0 ? $"+{d}" : (d < 0 ? $"-{d}" : $"{d}");
        }
    }

    public class ItemProperties
    {
        public decimal? stats_add_health;
        public decimal? stats_add_mana;

        public decimal? stats_mult_healthregen;
        public decimal? stats_mult_manaregen;

        public decimal? stats_add_agility;
        public decimal? stats_add_strength;
        public decimal? stats_add_intelligence;

        public decimal? stats_add_damage;

        public ItemProperties()
        {

        }
    }

    public class ShortSword : Item
    {
        public ShortSword()
        {
            Name = "Short Sword";
            Description = "A sword that is shorter than normal.";
            Properties = new ItemProperties
            {
                stats_add_strength = 5m,
                stats_add_damage = 3m
            };
        }
    }

}
