using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warcraft.Util;

namespace Warcraft.Units
{
    class InformationUnit : Information
    {
        public Util.Buildings ProduceAt;

        public Race Race;
        public Faction Faction;

        public int DamageMin;
        public int DamageMax;

        public int Range;
        public int Armor;
        public int Sight;
        public int MovementSpeed;

        public InformationUnit(string name, Race race, Faction faction, int hitPoints, int armor, int sight, int movementSpeed,
                            int costGold, int costFood, Util.Buildings produceAt, int buildTime, int damageMin, int damageMax, int range)
        {
            Name = name;

            Race = race;
            Faction = faction;

            HitPoints = hitPoints;
            Armor = armor;
            Sight = sight;
            MovementSpeed = movementSpeed;

            CostGold = costGold;
            CostFood = costFood;
            ProduceAt = produceAt;
            BuildTime = buildTime;

            DamageMin = damageMin;
            DamageMax = damageMax;

            Range = range;
        }
    }
}
