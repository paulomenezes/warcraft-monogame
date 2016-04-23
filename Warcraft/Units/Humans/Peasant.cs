using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Util;
using Warcraft.Managers;

namespace Warcraft.Units.Humans
{
    struct Sprite
    {
        public int x;
        public int y;
        public int width;
        public int height;

        public Sprite(int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }
    }

    class Peasant : Unit
    {        
        public Peasant(int tileX, int tileY, ManagerMouse mouse, ManagerMap managerMap) : base(tileX, tileY, 32, 32, 2, mouse, managerMap)
        {
            List<Sprite> sprites = new List<Sprite>();
            // UP
            sprites.Add(new Sprite(16, 8, 26, 23));
            sprites.Add(new Sprite(18, 46, 24, 28));
            sprites.Add(new Sprite(17, 86, 25, 26));
            sprites.Add(new Sprite(19, 122, 23, 30));
            sprites.Add(new Sprite(18, 159, 24, 27));
            // DOWN
            sprites.Add(new Sprite(193, 8, 25, 26));
            sprites.Add(new Sprite(195, 46, 24, 26));
            sprites.Add(new Sprite(194, 86, 25, 27));
            sprites.Add(new Sprite(195, 122, 23, 26));
            sprites.Add(new Sprite(194, 159, 24, 27));
            // LEFT
            sprites.Add(new Sprite(91, 4, 14, 31));
            sprites.Add(new Sprite(87, 42, 24, 30));
            sprites.Add(new Sprite(90, 82, 16, 31));
            sprites.Add(new Sprite(88, 118, 23, 30));
            sprites.Add(new Sprite(87, 155, 20, 30));
            // RIGHT
            sprites.Add(new Sprite(125, 4, 14, 31));
            sprites.Add(new Sprite(119, 42, 24, 30));
            sprites.Add(new Sprite(124, 82, 16, 31));
            sprites.Add(new Sprite(119, 118, 23, 30));
            sprites.Add(new Sprite(123, 155, 20, 30));

            Dictionary<string, int[]> animations = new Dictionary<string, int[]>();
            animations.Add("up", new int[] { 0, 1, 2, 3, 4 });
            animations.Add("down", new int[] { 5, 6, 7, 8, 9 });
            animations.Add("left", new int[] { 10, 11, 12, 13, 14 });
            animations.Add("right", new int[] { 15, 16, 17, 18, 19 });

            this.animations = new Animation(sprites, animations, "down", width, height);
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Peasant_walking");
        }
    }
}
