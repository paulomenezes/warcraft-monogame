using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Managers;
using Warcraft.Units;

namespace Warcraft.Commands
{
    class BuilderBuildings : ICommand
    {
        public Buildings.Building building;

        private ManagerBuildings managerBuildings;
        private ManagerMouse managerMouse;
        ManagerUnits managerUnits;

        private Unit builder;

        public BuilderBuildings(Util.Buildings building, Unit builder, ManagerMouse managerMouse, ManagerBuildings managerBuildings, ManagerUnits managerUnits)
        {
            this.builder = builder;

            this.managerMouse = managerMouse;
            this.managerUnits = managerUnits;
            this.managerBuildings = managerBuildings;

            this.building = Buildings.Building.Factory(building, managerMouse, managerBuildings.managerMap, managerUnits);
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
                building = Buildings.Building.Factory((building.information as Buildings.InformationBuilding).Type, managerMouse, managerBuildings.managerMap, managerUnits);

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
