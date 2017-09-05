using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseyRPGLib.Models
{
    public static class CommonFunctions
    {
        public static decimal CalculateHealthGainFromStrength(decimal strengthVal)
        {
            return strengthVal * 8m;
        }
        public static decimal CalculateManaGainFromIntelligence(decimal intelligenceVal)
        {
            return intelligenceVal * 6m;
        }
    }
}
