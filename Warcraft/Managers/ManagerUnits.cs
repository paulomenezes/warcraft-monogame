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

        public ManagerUnits(ManagerMouse managerMouse, ManagerMap managerMap)
        {
            for (int i = 3; i < 15; i++)
            {
                units.Add(new Peasant(3, i, managerMouse, managerMap));
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
    }
}
