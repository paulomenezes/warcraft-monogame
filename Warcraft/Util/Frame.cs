using System.Linq;

namespace Warcraft.Util
{
    struct Frame
    {
        public int[] sequence;
        public bool flipX;
        public bool flipY;

        public Frame(int start, int count)
        {
            this.sequence = Enumerable.Range(start, count).ToArray();
            this.flipX = false;
            this.flipY = false;
        }

        public Frame(int start, int count, bool flipX) : this(start, count)
        {
            this.flipX = flipX;
        }

        public Frame(int start, int count, bool flipX, bool flipY) : this(start, count, flipX)
        {
            this.flipY = flipY;
        }
    }
}
