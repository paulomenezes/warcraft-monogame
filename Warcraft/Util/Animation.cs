using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Warcraft.Util
{
    class Animation
    {
        public Dictionary<string, Frame> animations = new Dictionary<string, Frame>();

        public List<Sprite> sprites = new List<Sprite>();
        private bool play = false;

        public Rectangle rectangle;

        private int speed = 5;
        private int elapsed;
        private int index;

        private int width;
        private int height;

        private string current;

        private bool isLooping = true;
        public bool completed = false;

        public Animation(List<Sprite> sprites, Dictionary<string, Frame> animations, string initial, int width, int height)
        {
            this.sprites = sprites;
            this.animations = animations;
            this.current = initial;

            this.width = width;
            this.height = height;
        }

        public Animation(List<Sprite> sprites, Dictionary<string, Frame> animations, string initial, int width, int height, bool repeat, int speed)
            : this(sprites, animations, initial, width, height)
        {
            isLooping = repeat;
            this.speed = speed;
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
                if (elapsed > speed)
                {
                    index++;
                    elapsed = 0;

                    if (index >= animations[current].sequence.Length)
                    {
                        if (isLooping)
                            index = 0;
                        else
                        {
                            index--;
                            play = false;
                            completed = true;
                        }
                    }
                }
            }
            else
            {
                if (isLooping)
                    index = 0;
            }

            rectangle = new Rectangle(sprites[animations[current].sequence[index]].x - (width - sprites[animations[current].sequence[index]].width) / 2, 
                                      sprites[animations[current].sequence[index]].y - (height - sprites[animations[current].sequence[index]].height) / 2, 
                                      width, height);
        }
    }
}
