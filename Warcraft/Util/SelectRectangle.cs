using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warcraft.Util
{
    class SelectRectangle
    {
        private static Texture2D texture;

        public static void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Cursor");
        }

        public static void Draw(SpriteBatch spriteBatch, Rectangle rectangle)
        {
            spriteBatch.Draw(texture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, 1), Color.White);
            spriteBatch.Draw(texture, new Rectangle(rectangle.X, rectangle.Y, 1, rectangle.Height), Color.White);
            spriteBatch.Draw(texture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width, 1), Color.White);
            spriteBatch.Draw(texture, new Rectangle(rectangle.X + rectangle.Width, rectangle.Y, 1, rectangle.Height), Color.White);
        }
    }
}
