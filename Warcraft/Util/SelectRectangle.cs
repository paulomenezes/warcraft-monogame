using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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
            Draw(spriteBatch, rectangle, Color.Black);
        }

        public static void Draw(SpriteBatch spriteBatch, Rectangle rectangle, Color color)
        {
            spriteBatch.Draw(texture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, 1), color);
            spriteBatch.Draw(texture, new Rectangle(rectangle.X, rectangle.Y, 1, rectangle.Height), color);
            spriteBatch.Draw(texture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width, 1), color);
            spriteBatch.Draw(texture, new Rectangle(rectangle.X + rectangle.Width, rectangle.Y, 1, rectangle.Height), color);
        }
    }
}
