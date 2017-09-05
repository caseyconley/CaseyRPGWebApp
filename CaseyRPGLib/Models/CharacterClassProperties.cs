using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace CaseyRPGLib.Models
{
    public static class CharacterClassProperties
    {
        #region Fields and Properties
        private static Dictionary<string, CharacterClass> availableClasses;

        public static Dictionary<string, CharacterClass> AvailableClasses { get => availableClasses; private set => availableClasses = value; }
        #endregion

        public static void Initialize()
        {
            AvailableClasses = new Dictionary<string, CharacterClass>();
            LoadCharacterClassesFromConfig();
        }
        internal static void LoadCharacterClassesFromConfig()
        {
            List<CharacterClass> classes = new List<CharacterClass>();

            using (TextReader file = File.OpenText("characterClassConfig.json"))
            {
                classes = JObject.Parse(file.ReadToEnd())
                    .First
                    .First(o => o.Type == JTokenType.Array && o.Path.Contains("classes"))
                    .Select(cl => new CharacterClass
                    {
                        ClassName = (string)cl["ClassName"],
                        AttackType = (CharacterAttackType)Enum.Parse(typeof(CharacterAttackType), (string)cl["AttackType"]),
                        ClassType = (CharacterClassType)Enum.Parse(typeof(CharacterClassType), (string)cl["ClassName"]),
                        Stats_Base_Health = (decimal)cl["Stats_Base_Health"],
                        Stats_Base_Mana = (decimal)cl["Stats_Base_Mana"],
                        Stats_Base_Agility = (decimal)cl["Stats_Base_Agility"],
                        Stats_Base_Intelligence = (decimal)cl["Stats_Base_Intelligence"],
                        Stats_Base_Strength = (decimal)cl["Stats_Base_Strength"],
                        Stats_RatePerLevel_Agility = (decimal)cl["Stats_RatePerLevel_Agility"],
                        Stats_RatePerLevel_Strength = (decimal)cl["Stats_RatePerLevel_Strength"],
                        Stats_RatePerLevel_Intelligence = (decimal)cl["Stats_RatePerLevel_Intelligence"]
                    }).ToList();
            }
            
            classes.ForEach(c => AvailableClasses.Add(c.ClassName, c));
        }
        public static CharacterClass GetClassInfoByName(string className)
        {
            return AvailableClasses[className];
        }
        public static CharacterClass GetClassInfoByCharacterClassType(CharacterClassType classType)
        {
            return AvailableClasses[classType.ToString()];
        }
    }

    public class CharacterClass
    {
        public string ClassName;

        public decimal Stats_Base_Agility;
        public decimal Stats_Base_Strength;
        public decimal Stats_Base_Intelligence;

        public decimal Stats_RatePerLevel_Agility;
        public decimal Stats_RatePerLevel_Strength;
        public decimal Stats_RatePerLevel_Intelligence;

        public decimal Stats_Base_Health;
        public decimal Stats_Base_Mana;

        public CharacterClassType ClassType;
        public CharacterAttackType AttackType;

        public string ToDetailsString()
        {
            string s = string.Empty;
            s = $"Class {{" +
                $"\n\tName: {ClassName}" +
                $"\n\tAttack Type: {AttackType.ToString()}" +
                $"\n\tBase Health: {Stats_Base_Health}" +
                $"\n\tBase Mana: {Stats_Base_Mana}" +
                $"\n\tBase Strength: {Stats_Base_Strength}" +
                $"\n\tBase Agility: {Stats_Base_Agility}" +
                $"\n\tBase Intelligence: {Stats_Base_Intelligence}" +
                $"\n\tStrength Increase Per Level: {Stats_RatePerLevel_Strength}" +
                $"\n\tAgility Increase Per Level: {Stats_RatePerLevel_Agility}" +
                $"\n\tIntelligence Increase Per Level: {Stats_RatePerLevel_Intelligence}" +
                $"\n}}";
            return s;
        }
    }
}
