﻿using Warcraft.Util;

namespace Warcraft.Units
{
    class InformationUnit : Information
    {
        public Util.Buildings ProduceAt;

        public Race Race;
        public Faction Faction;

        public int Damage;
        public int Precision;

        public int Armor;
        public int Sight;
        public int MovementSpeed;

        public int Range;
        public int Spawn;
        public Util.Units Type;

        public InformationUnit(string name, Race race, Faction faction, int hitPoints, int armor, int sight, int movementSpeed,
                            int costGold, int costFood, Util.Buildings produceAt, int buildTime, int damage, int precision, int range, 
                            int spawn, Util.Units type)
        {
            Name = name;

            Race = race;
            Faction = faction;

            HitPoints = hitPoints;
            Armor = armor;
            Sight = sight;
            MovementSpeed = movementSpeed;
            Range = range;

            CostGold = costGold;
            CostFood = costFood;
            ProduceAt = produceAt;
            BuildTime = buildTime;

            Damage = damage;
            Precision = precision;

            Spawn = spawn;

            Type = type;
        }
    }
}
