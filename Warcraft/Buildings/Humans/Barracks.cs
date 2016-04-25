using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warcraft.Managers;
using Warcraft.Util;

namespace Warcraft.Buildings.Humans
{
    class Barracks : Building
    {
        public Barracks(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap) : 
            base(tileX, tileY, 128, 128, managerMouse, managerMap)
        {
            information = new InformationBuilding("Barracks", 800, 700, 450, Util.Units.PEASANT, 500);

            List<Sprite> sprites = new List<Sprite>();
            // BUILDING
            sprites.Add(new Sprite(576, 708, 48, 39));
            sprites.Add(new Sprite(572, 836, 61, 52));
            sprites.Add(new Sprite(135, 132, 116, 128));
            sprites.Add(new Sprite(135, 4, 128, 128));

            Dictionary<string, Frame> animations = new Dictionary<string, Frame>();
            animations.Add("building", new Frame(0, 4));

            this.animations = new Animation(sprites, animations, "building", width, height, false, information.BuildTime / sprites.Count);

            ui = new UI.Buildings.Barracks(managerMouse, this);
            textureName = "Human Buildings (Summer)";
        }
    }
}
