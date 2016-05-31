using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Warcraft.Units;
using Warcraft.Units.Orcs;
using Warcraft.Util;

namespace Warcraft.Managers
{
    class ManagerEnemies
    {
        public List<UnitEnemy> enemies = new List<UnitEnemy>();

        ContentManager content;

        Random random = new Random(Guid.NewGuid().GetHashCode());
        int wavesEnemies = 5;

        ManagerMouse managerMouse;
        ManagerMap managerMap;
        ManagerBuildings managerBuildings;

        public ManagerEnemies(ManagerMouse managerMouse, ManagerMap managerMap, ManagerBuildings managerBuildings)
        {
            this.managerMap = managerMap;
            this.managerMouse = managerMouse;
            this.managerBuildings = managerBuildings;

            for (int i = 0; i < wavesEnemies; i++)
            {
                int spawn = random.Next(0, 4);
                int sight = random.Next(0, 180);
                int damage = random.Next(1, 5);
                int precision = random.Next(1, 50);

                if (random.Next(0, 100) >= 50)
                {
                    InformationUnit info = new InformationUnit("Grunt", Race.ORC, Faction.HORDE, 60, 6, sight, 10, 600, 1, Util.Buildings.NONE, 60, damage, precision, 1, spawn, Util.Units.GRUNT);
                    enemies.Add(new Grunt(info, managerMouse, managerMap, managerBuildings));
                }
                else
                {
                    InformationUnit info = new InformationUnit("Troll Axethrower", Race.ORC, Faction.HORDE, 60, 6, sight, 10, 600, 1, Util.Buildings.NONE, 60, damage, precision, 5, spawn, Util.Units.TROLL_AXETHROWER);
                    enemies.Add(new TrollAxethrower(info, managerMouse, managerMap, managerBuildings));
                }
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
            int alives = 0;

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Update();

                if (enemies[i].information.HitPoints > 0)
                    alives++;
            }

            if (alives == 0)
            {
                wavesEnemies++;

                for (int i = 0; i < wavesEnemies; i++)
                {
                    int spawn = random.Next(0, 4);
                    int sight = random.Next(0, 360);
                    int damage = random.Next(1, 20);
                    int precision = random.Next(1, 100);

                    if (random.Next(0, 100) >= 50)
                    {
                        InformationUnit info = new InformationUnit("Grunt", Race.ORC, Faction.HORDE, 60, 6, sight, 10, 600, 1, Util.Buildings.NONE, 60, damage, precision, 1, spawn, Util.Units.GRUNT);
                        enemies.Add(new Grunt(info, managerMouse, managerMap, managerBuildings));
                    }
                    else
                    {
                        InformationUnit info = new InformationUnit("Troll Axethrower", Race.ORC, Faction.HORDE, 60, 6, sight, 10, 600, 1, Util.Buildings.NONE, 60, damage, precision, 5, spawn, Util.Units.TROLL_AXETHROWER);
                        enemies.Add(new TrollAxethrower(info, managerMouse, managerMap, managerBuildings));
                    }
                }

                LoadContent();
            }
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
