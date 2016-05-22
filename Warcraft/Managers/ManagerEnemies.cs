using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Warcraft.Units;
using Warcraft.Units.Orcs;

namespace Warcraft.Managers
{
    class ManagerEnemies
    {
        public List<UnitEnemy> enemies = new List<UnitEnemy>();

        ContentManager content;
        
        public ManagerEnemies(ManagerMouse managerMouse, ManagerMap managerMap, ManagerBuildings managerBuildings)
        {
            Random rng = new Random();
            for (int i = 0; i < 5; i++)
            {
                if (rng.Next(0, 100) > 50)
                    enemies.Add(new Grunt(managerMouse, managerMap, managerBuildings));
                else
                    enemies.Add(new TrollAxethrower(managerMouse, managerMap, managerBuildings));
            }
        }

        public void LoadContent()
        {
            enemies.ForEach((u) => u.LoadContent(content));
        }

        public void LoadContent(ContentManager content)
        {
            if (this.content == null)
                this.content = content;

            enemies.ForEach((u) => u.LoadContent(content));
        }

        public void Update()
        {
            enemies.ForEach((u) => u.Update());
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            enemies.ForEach((u) => u.Draw(spriteBatch));
        }

        public void DrawUI(SpriteBatch spriteBatch)
        {
            enemies.ForEach((u) => u.DrawUI(spriteBatch));
        }

        public List<Unit> GetSelected()
        {
            List<Unit> selecteds = new List<Unit>(); ;
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].selected)
                    selecteds.Add(enemies[i]);
            }

            return selecteds;
        }
    }
}
