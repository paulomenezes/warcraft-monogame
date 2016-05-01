using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warcraft.Commands;
using Warcraft.Managers;
using Warcraft.Units;
using Warcraft.Util;

namespace Warcraft.Buildings.Humans
{
    class TownHall : Building
    {
        ManagerUnits managerUnits;

        public TownHall(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits) : 
            base(tileX, tileY, 128, 128, managerMouse, managerMap)
        {
            information = new InformationBuilding("Town Hall", 1200, 1200, 800, Util.Units.PEASANT, 300, Util.Buildings.TOWN_HALL);

            List<Sprite> sprites = new List<Sprite>();
            // BUILDING
            sprites.Add(new Sprite(576, 708, 48, 39));
            sprites.Add(new Sprite(572, 836, 61, 52));
            sprites.Add(new Sprite(270, 154, 111, 95));
            sprites.Add(new Sprite(270, 17, 119, 104));

            Dictionary<string, Frame> animations = new Dictionary<string, Frame>();
            animations.Add("building", new Frame(0, 4));

            this.animations = new Animation(sprites, animations, "building", width, height, false, information.BuildTime / sprites.Count);

            ui = new UI.Buildings.TownHall(managerMouse, this);
            textureName = "Human Buildings (Summer)";

            commands.Add(new BuilderUnits(Util.Units.PEASANT, information.BuildTime));

            this.managerUnits = managerUnits;
        }

        public override void Update()
        {
            base.Update();

            for (int i = 0; i < commands.Count; i++)
            {
                var c = (commands[i] as BuilderUnits);
                c.Update();

                if (c.completed)
                {
                    var p = new Point(((int)position.X / 32) + ((width / Warcraft.TILE_SIZE) / 2), ((int)position.Y / 32) + ((height / Warcraft.TILE_SIZE)));
                    managerUnits.Factory(c.type, p.X, p.Y, target.X, target.Y);
                    c.completed = false;
                    c.remove = true;
                }
            }
        }
    }
}
