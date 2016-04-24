using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Util;
using Warcraft.Managers;

namespace Warcraft.Units.Humans
{
    class Peasant : Unit
    {        
        public Peasant(int tileX, int tileY, ManagerMouse mouse, ManagerMap managerMap) 
            : base(tileX, tileY, 32, 32, 2, mouse, managerMap)
        {
            List<Sprite> sprites = new List<Sprite>();
            // UP
            sprites.Add(new Sprite(16, 8, 26, 23));
            sprites.Add(new Sprite(18, 46, 24, 28));
            sprites.Add(new Sprite(17, 86, 25, 26));
            sprites.Add(new Sprite(19, 122, 23, 30));
            sprites.Add(new Sprite(18, 159, 24, 27));
            // DOWN
            sprites.Add(new Sprite(166, 7, 25, 26));
            sprites.Add(new Sprite(168, 45, 24, 26));
            sprites.Add(new Sprite(167, 85, 25, 27));
            sprites.Add(new Sprite(168, 121, 23, 26));
            sprites.Add(new Sprite(167, 158, 24, 27));
            // LEFT
            sprites.Add(new Sprite(97, 4, 14, 31));
            sprites.Add(new Sprite(91, 42, 24, 30));
            sprites.Add(new Sprite(96, 82, 16, 31));
            sprites.Add(new Sprite(91, 118, 23, 30));
            sprites.Add(new Sprite(95, 155, 20, 30));
            // UP-RIGHT
            sprites.Add(new Sprite(56, 6, 22, 26));
            sprites.Add(new Sprite(55, 44, 26, 30));
            sprites.Add(new Sprite(56, 84, 24, 29));
            sprites.Add(new Sprite(59, 119, 23, 29));
            sprites.Add(new Sprite(57, 156, 21, 28));
            // DOWN-RIGHT
            sprites.Add(new Sprite(127, 3, 22, 31));
            sprites.Add(new Sprite(128, 40, 20, 27));
            sprites.Add(new Sprite(130, 80, 19, 28));
            sprites.Add(new Sprite(126, 119, 26, 29));
            sprites.Add(new Sprite(126, 156, 26, 28));

            Dictionary<string, Frame> animations = new Dictionary<string, Frame>();
            animations.Add("up", new Frame(new int[] { 0, 1, 2, 3, 4 }));
            animations.Add("down", new Frame(new int[] { 5, 6, 7, 8, 9 }));
            animations.Add("right", new Frame(new int[] { 10, 11, 12, 13, 14 }));
            animations.Add("left", new Frame(new int[] { 10, 11, 12, 13, 14 }, true));
            animations.Add("upRight", new Frame(new int[] { 15, 16, 17, 18, 19 }));
            animations.Add("downRight", new Frame(new int[] { 20, 21, 22, 23, 24 }));
            animations.Add("upLeft", new Frame(new int[] { 15, 16, 17, 18, 19 }, true));
            animations.Add("downLeft", new Frame(new int[] { 20, 21, 22, 23, 24 }, true));

            this.animations = new Animation(sprites, animations, "down", width, height);
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Peasant_walking");
        }
    }
}
