using Warcraft.Units.Humans;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warcraft.Util
{
    class Animation
    {
        public Dictionary<string, int[]> animations = new Dictionary<string, int[]>();

        private List<Sprite> sprites = new List<Sprite>();
        private bool play = false;

        public Rectangle rectangle;

        private int elapsed;
        private int index;

        private int width;
        private int height;

        private string current;

        public Animation(List<Sprite> sprites, Dictionary<string, int[]> animations, string initial, int width, int height)
        {
            this.sprites = sprites;
            this.animations = animations;
            this.current = initial;

            this.width = width;
            this.height = height;
        }

        public void Play(string animation)
        {
            this.current = animation;
            this.play = true;
        }

        public void Stop()
        {
            this.play = false;
        }

        public void Update()
        {
            if (play)
            {
                elapsed++;
                if (elapsed > 5)
                {
                    index++;
                    elapsed = 0;

                    if (index > 4)
                        index = 0;
                }
            }
            else
            {
                index = 0;
            }

            rectangle = new Rectangle(sprites[animations[current][index]].x - (width - sprites[animations[current][index]].width) / 2, 
                                      sprites[animations[current][index]].y - (height - sprites[animations[current][index]].height) / 2, 
                                      width, height);
        }
    }
}
