using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warcraft.Managers;
using Warcraft.Util;

namespace Warcraft.Buildings.Humans
{
    class ChickenFarm : Building
    {
        public ChickenFarm(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits) : 
            base(tileX, tileY, 64, 64, managerMouse, managerMap, managerUnits)
        {
            information = new InformationBuilding("Chicken Farm", 800, 700, 450, Util.Units.PEASANT, 300, Util.Buildings.CHICKEN_FARM);

            List<Sprite> sprites = new List<Sprite>();
            // BUILDING
            sprites.Add(new Sprite(576, 708, 48, 39));
            sprites.Add(new Sprite(572, 836, 61, 52));
            sprites.Add(new Sprite(398, 73, 63, 59));
            sprites.Add(new Sprite(398, 4, 64, 64));

            Dictionary<string, Frame> animations = new Dictionary<string, Frame>();
            animations.Add("building", new Frame(0, 4));

            this.animations = new Animation(sprites, animations, "building", width, height, false, information.BuildTime / sprites.Count);

            ui = new UI.Buildings.ChickenFarm(managerMouse, this);
            textureName = "Human Buildings (Summer)";
        }
    }
}
