using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Warcraft.Units;
using Warcraft.Units.Humans;

namespace Warcraft.Managers
{
    class ManagerUnits
    {
        List<Unit> units = new List<Unit>();

        public ManagerUnits(ManagerMouse managerMouse, ManagerMap managerMap, ManagerBuildings managerBuildings)
        {
            for (int i = 3; i < 5; i++)
            {
                units.Add(new Peasant(3, i, managerMouse, managerMap, managerBuildings));
            }
        }
        
        public void LoadContent(ContentManager content)
        {
            units.ForEach((u) => u.LoadContent(content));
        }

        public void Update()
        {
            units.ForEach((u) => u.Update());
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            units.ForEach((u) => u.Draw(spriteBatch));
        }

        public List<Unit> GetSelected()
        {
            List<Unit> selecteds = new List<Unit>(); ;
            for (int i = 0; i < units.Count; i++)
            {
                if (units[i].selected)
                    selecteds.Add(units[i]);
            }

            return selecteds;
        }
    }
}
