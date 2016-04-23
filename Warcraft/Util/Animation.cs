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
        public Dictionary<string, Frame> animations = new Dictionary<string, Frame>();

        private List<Sprite> sprites = new List<Sprite>();
        private bool play = false;

        public Rectangle rectangle;

        private int elapsed;
        private int index;

        private int width;
        private int height;

        private string current;

        public Animation(List<Sprite> sprites, Dictionary<string, Frame> animations, string initial, int width, int height)
        {
            this.sprites = sprites;
            this.animations = animations;
            this.current = initial;

            this.width = width;
            this.height = height;
        }

        public void Play(string animation)
        {
            current = animation;
            play = true;
        }

        public void Stop()
        {
            play = false;
        }

        public bool FlipX()
        {
            return animations[current].flipX;
        }

        public bool FlipY()
        {
            return animations[current].flipY;
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

            rectangle = new Rectangle(sprites[animations[current].sequence[index]].x - (width - sprites[animations[current].sequence[index]].width) / 2, 
                                      sprites[animations[current].sequence[index]].y - (height - sprites[animations[current].sequence[index]].height) / 2, 
                                      width, height);
        }
    }
}
