using System.Collections.Generic;
using Warcraft.Managers;
using Warcraft.Util;

namespace Warcraft.Units.Humans
{
    class Footman : Unit
    {
        public Footman(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerBuildings managerBuildings, ManagerUnits managerUnits) 
            : base(tileX, tileY, 52, 52, 2, managerMouse, managerMap, managerBuildings)
        {
            Dictionary<AnimationType, List<Sprite>> sprites = new Dictionary<AnimationType, List<Sprite>>();
            List<Sprite> spriteWalking = new List<Sprite>();
            // UP
            spriteWalking.Add(new Sprite(22, 10, 31, 40));
            spriteWalking.Add(new Sprite(22, 62, 30, 52));
            spriteWalking.Add(new Sprite(22, 119, 30, 49));
            spriteWalking.Add(new Sprite(23, 182, 29, 37));
            spriteWalking.Add(new Sprite(22, 230, 31, 37));
            // DOWN
            spriteWalking.Add(new Sprite(316, 12, 32, 46));
            spriteWalking.Add(new Sprite(315, 72, 29, 42));
            spriteWalking.Add(new Sprite(316, 128, 30, 44));
            spriteWalking.Add(new Sprite(318, 178, 31, 42));
            spriteWalking.Add(new Sprite(316, 226, 32, 45));
            // LEFT
            spriteWalking.Add(new Sprite(176, 12, 32, 37));
            spriteWalking.Add(new Sprite(168, 73, 45, 37));
            spriteWalking.Add(new Sprite(170, 127, 39, 38));
            spriteWalking.Add(new Sprite(170, 179, 32, 33));
            spriteWalking.Add(new Sprite(169, 226, 35, 34));
            // UP-RIGHT
            spriteWalking.Add(new Sprite(99, 10, 25, 39));
            spriteWalking.Add(new Sprite(94, 65, 39, 41));
            spriteWalking.Add(new Sprite(95, 123, 32, 40));
            spriteWalking.Add(new Sprite(97, 179, 33, 38));
            spriteWalking.Add(new Sprite(99, 225, 31, 39));
            // DOWN-RIGHT
            spriteWalking.Add(new Sprite(244, 13, 40, 35));
            spriteWalking.Add(new Sprite(240, 74, 43, 40));
            spriteWalking.Add(new Sprite(242, 129, 41, 37));
            spriteWalking.Add(new Sprite(244, 177, 36, 35));
            spriteWalking.Add(new Sprite(244, 227, 38, 31));

            sprites.Add(AnimationType.WALKING, spriteWalking);

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

            ui = new UI.Units.Footman(managerMouse, this);
            textureName.Add(AnimationType.WALKING, "Footman");

            information = new InformationUnit("Footman", Race.HUMAN, Faction.ALLIANCE, 60, 6, 4, 10, 600, 1, Util.Buildings.BARRACKS, 60, 6, 13, 1);
        }
    }
}
