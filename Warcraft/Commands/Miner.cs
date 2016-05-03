using System;
using Warcraft.Buildings;
using Warcraft.Buildings.Humans;
using Warcraft.Buildings.Neutral;
using Warcraft.Managers;
using Warcraft.Units;
using Warcraft.Units.Humans;

namespace Warcraft.Commands
{
    class Miner : ICommand
    {
        GoldMine goldMine;
        TownHall townHall;
        Unit worker;

        ManagerBuildings managerBuildings;

        public Miner(ManagerBuildings managerBuildings, Unit worker)
        {
            goldMine = managerBuildings.buildings.Find((b) => (b.information as InformationBuilding).Type == Util.Buildings.GOLD_MINE) as GoldMine;
            townHall = managerBuildings.buildings.Find((b) => (b.information as InformationBuilding).Type == Util.Buildings.TOWN_HALL) as TownHall;

            this.worker = worker;
            this.managerBuildings = managerBuildings;
        }

        public void execute()
        {
            townHall = managerBuildings.buildings.Find((b) => (b.information as InformationBuilding).Type == Util.Buildings.TOWN_HALL) as TownHall;

            if (townHall != null)
            {
                goldMine.workers.Add(worker as Peasant);
                worker.workState = WorkigState.GO_TO_WORK;
                worker.Move((int)goldMine.position.X / 32, (int)goldMine.position.Y / 32);
                worker.selected = false;
            }
        }

        public void Update()
        {
            if (worker.workState == WorkigState.WORKING && goldMine.workers.Count > 0)
            {
                goldMine.animations.Change("working");
            }
        }
    }
}
