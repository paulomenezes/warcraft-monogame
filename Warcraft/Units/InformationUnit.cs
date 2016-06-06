using System;
using Warcraft.Util;

namespace Warcraft.Units
{
    class InformationUnit : Information
    {
        public Util.Buildings ProduceAt;

        public Race Race;
        public Faction Faction;

        public float Damage;
        public float Precision;

        public float Armor;
        public float Sight;
        public int MovementSpeed;

        public int Range;
        public int Spawn;
        public Util.Units Type;

        public float Fitness;

        public InformationUnit(string name, Race race, Faction faction, float hitPoints, float armor, float sight, int movementSpeed,
                            int costGold, int costFood, Util.Buildings produceAt, int buildTime, float damage, float precision, int range, 
                            int spawn, Util.Units type)
        {
            Name = name;

            Race = race;
            Faction = faction;

            HitPoints = Math.Min(200, Math.Max(hitPoints, 1));
            HitPointsTotal = HitPoints;
            Armor = Math.Min(20, Math.Max(armor, 0));
            Sight = Math.Min(360, Math.Max(sight, 1));
            MovementSpeed = movementSpeed;
            Range = range;

            CostGold = costGold;
            CostFood = costFood;
            ProduceAt = produceAt;
            BuildTime = buildTime;

            Damage = Math.Min(20, Math.Max(damage, 0));
            Precision = Math.Min(100, Math.Max(precision, 1));

            Spawn = (int)Math.Min(0, Math.Max(spawn, 4));

            Type = type;
        }

        public override string ToString()
        {
            return "Type: " + Type + 
                 "\nHitpoints: " + HitPointsTotal + 
                 "\nArmor: " + Armor + 
                 "\nSight: " + Sight +
                 "\nDamage: " + Damage + 
                 "\nPrecision: " + Precision +
                 "\n\n";
        }
    }
}
