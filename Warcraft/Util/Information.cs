namespace Warcraft.Util
{
    enum Race
    {
        HUMAN,
        ORC,
        HIGH_ELF
    }

    enum Faction
    {
        ALLIANCE,
        HORDE
    }

    class Information
    {
        public string Name;

        public float HitPoints;

        public int CostGold;
        public int CostFood;
        public int CostWood;
        public int BuildTime;
    }
}
