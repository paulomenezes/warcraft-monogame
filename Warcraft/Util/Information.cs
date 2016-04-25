using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warcraft.Util;

namespace Warcraft.Util
{
    enum Race
    {
        HUMAN,
        ORC
    }

    enum Faction
    {
        ALLIANCE,
        HORDE
    }

    class Information
    {
        public string Name;

        public int HitPoints;

        public int CostGold;
        public int CostFood;
        public int CostWood;
        public int BuildTime;
    }
}
