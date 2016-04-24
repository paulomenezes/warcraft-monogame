namespace Warcraft.Util
{
    struct Frame
    {
        public int[] sequence;
        public bool flipX;
        public bool flipY;

        public Frame(int[] sequence)
        {
            this.sequence = sequence;
            this.flipX = false;
            this.flipY = false;
        }

        public Frame(int[] sequence, bool flipX) : this(sequence)
        {
            this.flipX = flipX;
        }

        public Frame(int[] sequence, bool flipX, bool flipY) : this(sequence, flipX)
        {
            this.flipY = flipY;
        }
    }
}
