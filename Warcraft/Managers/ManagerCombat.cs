namespace Warcraft.Managers
{
    class ManagerCombat
    {
        ManagerUnits managerUnits;
        ManagerEnemies managerEnemies;
        ManagerBuildings managerBuildings;

        public ManagerCombat(ManagerUnits managerUnits, ManagerEnemies managerEnemies, ManagerBuildings managerBuildings)
        {
            this.managerUnits = managerUnits;
            this.managerEnemies = managerEnemies;
            this.managerBuildings = managerBuildings;
        }

        public void Update()
        {
            for (int u = 0; u < managerUnits.units.Count; u++)
            {
                for (int e = 0; e < managerEnemies.enemies.Count; e++)
                {
                    if (managerEnemies.enemies[e].target == null && 
                        managerEnemies.enemies[e].enemyDetect.Intersects(managerUnits.units[u].rectangle) &&
                        managerUnits.units[u].information.HitPoints > 0 &&
                        managerUnits.units[u].workState == Units.WorkigState.NOTHING)
                    {
                        managerEnemies.enemies[e].target = managerUnits.units[u];
                    }
                }
            }
        }
    }
}
