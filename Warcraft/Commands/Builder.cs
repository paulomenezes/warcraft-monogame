using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warcraft.Managers;
using Warcraft.Units;
using Warcraft.Util;

namespace Warcraft.Commands
{
    class Builder : ICommand
    {
        public Buildings.Building building;

        private ManagerBuildings managerBuildings;
        private ManagerMouse managerMouse;

        private Unit builder;

        public Builder(Util.Buildings building, Unit builder, ManagerMouse managerMouse, ManagerBuildings managerBuildings)
        {
            this.builder = builder;

            this.managerMouse = managerMouse;
            this.managerBuildings = managerBuildings;

            this.building = Buildings.Building.Factory(building, managerMouse, managerBuildings.managerMap);
        }

        public void execute()
        {
            builder.workState = WorkigState.WAITING_PLACE;
            building.builder();
        }

        public void LoadContent(ContentManager content)
        {
            building.LoadContent(content);
        }

        public void Update()
        {
            if (building.isBuilding)
                building.Update();

            if (!building.isBuilding && building.isWorking)
            {
                managerBuildings.AddBuilding(building);
                building = Buildings.Building.Factory((building.information as Buildings.InformationBuilding).Type, managerMouse, managerBuildings.managerMap);

                builder.workState = WorkigState.NOTHING;
                builder.position.Y += 32 * 2;
                builder.MoveTo(0, 1);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (building.isBuilding)
                building.Draw(spriteBatch);
        }
    }
}
