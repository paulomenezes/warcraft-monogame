using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
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
            managerMouse.MouseClickEventHandler += ManagerMouse_MouseClickEventHandler;

            for (int i = 3; i < 15; i++)
                units.Add(new Peasant(3, i, managerMouse, managerMap, managerBuildings));
        }

        private void ManagerMouse_MouseClickEventHandler(object sender, Events.MouseClickEventArgs e)
        {
            List<Unit> selecteds = GetSelected();

            int threshold = (int)Math.Sqrt(selecteds.Count) / 2;
            int x = -threshold;
            int y = x;
            for (int i = 0; i < selecteds.Count; i++)
            {
                selecteds[i].Move(e.XTile + x, e.YTile + y);
                x++;
                if (x > threshold)
                {
                    x = -threshold;
                    y++;
                }
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
