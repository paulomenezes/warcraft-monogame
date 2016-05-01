using Warcraft.Map;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;

namespace Warcraft.Managers
{
    class ManagerMap
    {
        private Texture2D texture;
        private List<Tile> map = new List<Tile>();
        public List<Tile> walls = new List<Tile>();

        public ManagerMap()
        {
            for (int i = 0; i < Warcraft.WINDOWS_WIDTH / 32; i++)
            {
                for (int j = 0; j < Warcraft.WINDOWS_HEIGHT / 32; j++)
                {
                    map.Add(new Tile(i, j, 0, 14));
                }
            }

            for (var i = 0; i < 8; i++)
            {
                walls.Add(new Tile(5, i, 2, 1));
            }

            for (var i = 0; i < 12; i++)
            {
                walls.Add(new Tile(8, i + 7, 2, 1));
            };

            for (var i = 0; i < 10; i++)
            {
                walls.Add(new Tile(12, i, 2, 1));
            };
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Summer Tiles");

            map.ForEach((item) => item.LoadContent(texture));
            walls.ForEach((item) => item.LoadContent(texture));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            map.ForEach((item) => item.Draw(spriteBatch));
            walls.ForEach((item) => item.Draw(spriteBatch));
        }

        public void AddWalls(Vector2 position, int xQuantity, int yQuantity)
        {
            for (int i = 0; i < xQuantity; i++)
                for (int j = 0; j < yQuantity; j++)
                    walls.Add(new Tile(((int)position.X / 32) + i, ((int)position.Y / 32) + j));
        }

        public bool CheckWalls(Vector2 position, int xQuantity, int yQuantity)
        {
            int pointX = (int)position.X / 32;
            int pointY = (int)position.Y / 32;

            if (pointX < 0 || pointY < 0 ||
                pointX + 1 > Warcraft.WINDOWS_WIDTH / Warcraft.TILE_SIZE ||
                pointY + 1 > Warcraft.WINDOWS_HEIGHT / Warcraft.TILE_SIZE)
                return true;

            for (int k = 0; k < walls.Count; k++)
            {
                for (int i = 0; i < xQuantity; i++)
                    for (int j = 1; j < yQuantity; j++)
                        if (walls[k].TileX == pointX + i && walls[k].TileY == pointY + j)
                            return true;
            }

            return false;
        }
    }
}
