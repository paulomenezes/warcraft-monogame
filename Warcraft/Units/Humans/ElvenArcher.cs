using System.Collections.Generic;
using Warcraft.Managers;
using Warcraft.Util;

namespace Warcraft.Units.Humans
{
    class ElvenArcher : Unit
    {
        public ElvenArcher(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerBuildings managerBuildings, ManagerUnits managerUnits) 
            : base(tileX, tileY, 48, 48, 2, managerMouse, managerMap, managerBuildings)
        {
            List<Sprite> sprites = new List<Sprite>();
            // UP
            sprites.Add(new Sprite(6, 11, 40, 47));
            sprites.Add(new Sprite(5, 85, 42, 49));
            sprites.Add(new Sprite(5, 159, 41, 47));
            sprites.Add(new Sprite(8, 234, 36, 47));
            sprites.Add(new Sprite(5, 308, 41, 46));
            // DOWN
            sprites.Add(new Sprite(241, 17, 44, 42));
            sprites.Add(new Sprite(239, 92, 43, 41));
            sprites.Add(new Sprite(241, 165, 43, 42));
            sprites.Add(new Sprite(247, 238, 38, 41));
            sprites.Add(new Sprite(244, 312, 41, 41));
            // LEFT
            sprites.Add(new Sprite(128, 16, 38, 40));
            sprites.Add(new Sprite(128, 92, 40, 41));
            sprites.Add(new Sprite(129, 164, 39, 41));
            sprites.Add(new Sprite(129, 240, 35, 38));
            sprites.Add(new Sprite(130, 312, 35, 41));
            // UP-RIGHT
            sprites.Add(new Sprite(67, 12, 42, 45));
            sprites.Add(new Sprite(70, 88, 38, 41));
            sprites.Add(new Sprite(68, 161, 40, 44));
            sprites.Add(new Sprite(65, 230, 41, 46));
            sprites.Add(new Sprite(67, 306, 40, 43));
            // DOWN-RIGHT
            sprites.Add(new Sprite(186, 17, 42, 37));
            sprites.Add(new Sprite(183, 92, 38, 37));
            sprites.Add(new Sprite(184, 165, 40, 38));
            sprites.Add(new Sprite(187, 237, 43, 36));
            sprites.Add(new Sprite(187, 312, 42, 37));

            Dictionary<string, Frame> animations = new Dictionary<string, Frame>();
            animations.Add("up", new Frame(0, 5));
            animations.Add("down", new Frame(5, 5));
            animations.Add("right", new Frame(10, 5));
            animations.Add("left", new Frame(10, 5, true));
            animations.Add("upRight", new Frame(15, 5));
            animations.Add("downRight", new Frame(20, 5));
            animations.Add("upLeft", new Frame(15, 5, true));
            animations.Add("downLeft", new Frame(20, 5, true));

            this.animations = new Animation(sprites, animations, "down", width, height);

            ui = new UI.Units.ElvenArcher(managerMouse, this);
            textureName = "Elven Archer";

            information = new InformationUnit("Elven Archer", Race.HIGH_ELF, Faction.ALLIANCE, 50, 2, 9, 10, 500, 1, Util.Buildings.BARRACKS, 70, 5, 11, 4);
        }
    }
}
