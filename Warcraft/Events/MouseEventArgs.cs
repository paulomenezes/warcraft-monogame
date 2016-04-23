using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
