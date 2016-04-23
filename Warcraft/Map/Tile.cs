using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warcraft.Map
{
    class Tile
    {
        private Texture2D texture;
        private Rectangle rectangle;
        private Vector2 position;

        public int TileX;
        public int TileY;

        public Tile(int tileX, int tileY, int textureX, int textureY)
        {
            this.TileX = tileX;
            this.TileY = tileY;

            this.position = new Vector2(tileX * Warcraft.TILE_SIZE, tileY * Warcraft.TILE_SIZE);
            this.rectangle = new Rectangle(textureX * (Warcraft.TILE_SIZE + 1), textureY * (Warcraft.TILE_SIZE + 1), Warcraft.TILE_SIZE, Warcraft.TILE_SIZE);
        }

        public void LoadContent(Texture2D texture)
        {
            this.texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (rectangle.Width >= 0)
                spriteBatch.Draw(texture, position, rectangle, Color.White);
        }
    }
}
