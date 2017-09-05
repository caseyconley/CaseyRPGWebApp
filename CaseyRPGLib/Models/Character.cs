using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseyRPGLib.Models
{
    public enum CharacterAttackType
    {
        Melee = 0,
        Ranged = 1
    }

    public enum CharacterClassType
    {
        None = 0,
        Warrior = 1,
        Mage = 2,
        Rogue = 3,
        Necromancer = 4,
        Priest = 5,
        Archer = 6
    }

    public abstract class Character
    {
        #region Fields and Properties
        public string Name { get => name; protected set => name = value; }
        public CharacterClass Class
        {
            get { return @class; }
            protected set
            {
                SetCharacterClassAndStats(value);
            }
        }
        public string ClassName { get => Class.ClassName; }
        public uint Level { get => level; protected set => level = value; }
        public decimal MaxHealth { get => stats_Max_Health; private set => stats_Max_Health = value; }
        public decimal MaxMana { get => stats_Max_Mana; private set => stats_Max_Mana = value; }
        public decimal Agility { get => stats_Current_Agility; private set => stats_Current_Agility = value; }
        public decimal Strength { get => stats_Current_Strength; private set => stats_Current_Strength = value; }
        public decimal Intelligence { get => stats_Current_Intelligence; private set => stats_Current_Intelligence = value; }
        public decimal Health { get => stats_Current_Health; private set => stats_Current_Health = value; }
        public decimal Mana { get => stats_Current_Mana; private set => stats_Current_Mana = value; }
        public CharacterClassType ClassType { get => classType; private set => classType = value; }
        public CharacterAttackType AttackType { get => attackType; private set => attackType = value; }
        public Backpack Backpack { get => backpack; set => backpack = value; }

        private string name;
        private uint level;
        private CharacterClass @class = null;
        private Backpack backpack = null;

        private CharacterClassType classType;
        private CharacterAttackType attackType;

        private decimal stats_Max_Health;
        private decimal stats_Max_Mana;
        private decimal stats_Current_Agility;
        private decimal stats_Current_Strength;
        private decimal stats_Current_Intelligence;
        private decimal stats_Current_Health;
        private decimal stats_Current_Mana;

        //public decimal Stats_Base_Agility;
        //public decimal Stats_Base_Strength;
        //public decimal Stats_Base_Intelligence;
        //public decimal Stats_Base_Health;
        //public decimal Stats_Base_Mana;
        //public decimal Stats_RatePerLevel_Agility;
        //public decimal Stats_RatePerLevel_Strength;
        //public decimal Stats_RatePerLevel_Intelligence;
        #endregion

        public abstract void LevelUp();
        internal void SetCharacterClassAndStats(CharacterClass classTemplate)
        {
            if (@class != null)
                return;

            Level = 1;

            ClassType = classTemplate.ClassType;
            AttackType = classTemplate.AttackType;
            @class = classTemplate;

            this.CalculateAndSetStats(true);

            //Health = MaxHealth = CommonFunctions.CalculateHealthGainFromStrength(classTemplate.Stats_Base_Strength);
            //Mana = MaxMana = CommonFunctions.CalculateManaGainFromIntelligence(classTemplate.Stats_Base_Intelligence);

            //Agility = classTemplate.Stats_Base_Agility;
            //Strength = classTemplate.Stats_Base_Strength;
            //Intelligence = classTemplate.Stats_Base_Intelligence;

            
        }
        internal void CalculateAndSetStats(bool fullHealthMana = false)
        {
            decimal oldMaxHealth = MaxHealth,
                    oldMaxMana = MaxMana,
                    oldCurrentHealth = Health,
                    oldCurrentMana = Mana;

            Agility = Class.Stats_RatePerLevel_Agility * (Level - 1) + Class.Stats_Base_Agility;
            Strength = Class.Stats_RatePerLevel_Strength * (Level - 1) + Class.Stats_Base_Strength;
            Intelligence = Class.Stats_RatePerLevel_Intelligence * (Level - 1) + Class.Stats_Base_Intelligence;

            //heal to full health and mana when leveling up
            Mana = MaxMana = CommonFunctions.CalculateManaGainFromIntelligence(Class.Stats_RatePerLevel_Intelligence * (Level - 1) + Class.Stats_Base_Intelligence);
            Health = MaxHealth = CommonFunctions.CalculateHealthGainFromStrength(Class.Stats_RatePerLevel_Strength * (Level - 1) + Class.Stats_Base_Strength);

            foreach (Item item in this.Backpack.Items.Values)
            {
                if (item == null)
                    continue;

                Strength += item.Properties.stats_add_strength ?? 0;
                Agility += item.Properties.stats_add_agility ?? 0;
                Intelligence += item.Properties.stats_add_intelligence ?? 0;

                MaxHealth += item.Properties.stats_add_health ?? 0;
                MaxMana += item.Properties.stats_add_mana ?? 0;
            }

            if (!fullHealthMana)
            {
                Health = (oldCurrentHealth / oldMaxHealth) * MaxHealth;
                Mana = (oldCurrentMana / oldMaxMana) * MaxMana;
            }
        }
        public void AddItemToBackpack(Item item)
        {
            this.Backpack.Add(item);
            this.CalculateAndSetStats();
        }
        public string ToDetailsString()
        {
            string s = string.Empty;
            s = $"Character {{" +
                $"\n\tName: {Name}" +
                $"\n\tClass: {ClassName}" +
                $"\n\tHealth: {Health}/{MaxHealth} ({(Health / MaxHealth) * 100m})" +
                $"\n\tMana: {Mana}/{MaxMana} ({(Mana / MaxMana) * 100m})" +
                $"\n\tStrength: {Strength}" +
                $"\n\tAgility: {Agility}" +
                $"\n\tIntelligence: {Intelligence}";
            s += Backpack.ToDetailsString();
            s += $"\n}}";
            return s;
        }
    }

    public class Rogue : Character
    {
        public Rogue(string characterName)
        {
            Name = characterName;
            Level = 1;
            Backpack = new Backpack();
            Class = CharacterClassProperties.AvailableClasses["Rogue"];
        }
        public override void LevelUp()
        {
            Level++;
            base.CalculateAndSetStats();

            switch (Level)
            {
                //talents? abilities?
                case 5: break;
                case 10: break;
                case 15: break;
                case 20: break;
                case 25: break;
                default: break;
            }
        }
    }
}