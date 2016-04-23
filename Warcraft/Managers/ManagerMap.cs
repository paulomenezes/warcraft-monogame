using Warcraft.Map;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                for (int j = 0; j < Warcraft.WINDOWS_WIDTH / 32; j++)
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
    }
}
