using Microsoft.Xna.Framework;
using System;

namespace Warcraft.Events
{
    class MouseEventArgs : EventArgs
    {
        public Rectangle SelectRectangle;

        public MouseEventArgs(Rectangle selectRectangle)
        {
            SelectRectangle = selectRectangle;
        }
    }
}
