using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Warcraft.Map
{
    class Tile
    {
        public static Texture2D texture;
        private Rectangle rectangle;
        private Vector2 position;

        public int TileX;
        public int TileY;

        public bool isWall = false;

        public Tile(int tileX, int tileY)
        {
            TileX = tileX;
            TileY = tileY;

            isWall = false;

            rectangle = new Rectangle(0, 0, -1, -1);
        }

        public Tile(int tileX, int tileY, int textureX, int textureY)
        {
            TileX = tileX;
            TileY = tileY;

            isWall = true;

            position = new Vector2(tileX * Warcraft.TILE_SIZE, tileY * Warcraft.TILE_SIZE);
            rectangle = new Rectangle(textureX * (Warcraft.TILE_SIZE + 1), textureY * (Warcraft.TILE_SIZE + 1), Warcraft.TILE_SIZE, Warcraft.TILE_SIZE);
        }

        public void ChangeTexture(int textureX, int textureY)
        {
            rectangle = new Rectangle(textureX * (Warcraft.TILE_SIZE + 1), textureY * (Warcraft.TILE_SIZE + 1), Warcraft.TILE_SIZE, Warcraft.TILE_SIZE);
        }

        public void LoadContent(Texture2D _texture)
        {
            if (texture == null)
                texture = _texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (rectangle.Width >= 0)
                spriteBatch.Draw(texture, position, rectangle, Color.White);
        }
    }
}
