using Warcraft.Map;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

namespace Warcraft.Managers
{
    class ManagerMap
    {
        private Texture2D texture;
        private List<Tile> map = new List<Tile>();
        public List<Tile> walls = new List<Tile>();

        public ManagerMap()
        {
            for (int i = 0; i < Warcraft.MAP_SIZE; i++)
            {
                for (int j = 0; j < Warcraft.MAP_SIZE; j++)
                {
                    map.Add(new Tile(i, j, 0, 14));
                }
            }
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

        public void AddWalls(Vector2 position, Rectangle rectangle)
        {
            int tileX = ((int)position.X / 32);
            int tileY = ((int)position.Y / 32);

            int textureX = rectangle.X / 32;
            int textureY = rectangle.Y / 32;

            var change = false;
            var neighbourhoods = GetNeighbourhood(tileX, tileY);

            if (neighbourhoods.ContainsKey("top"))
            {
                if (neighbourhoods.ContainsKey("topright") && !neighbourhoods.ContainsKey("topleft"))
                    walls[neighbourhoods["top"]].ChangeTexture(0, 1);
                if (neighbourhoods.ContainsKey("topleft") && !neighbourhoods.ContainsKey("topright"))
                    walls[neighbourhoods["top"]].ChangeTexture(7, 1);
                if (neighbourhoods.ContainsKey("topleft") && neighbourhoods.ContainsKey("topright"))
                    walls[neighbourhoods["top"]].ChangeTexture(10, 1);
                
                change = true;
            }
            else if (neighbourhoods.ContainsKey("bottom"))
            {
                if (neighbourhoods.ContainsKey("bottomright") && !neighbourhoods.ContainsKey("bottomleft"))
                    walls[neighbourhoods["bottom"]].ChangeTexture(4, 1);
                if (neighbourhoods.ContainsKey("bottomleft") && !neighbourhoods.ContainsKey("bottomright"))
                    walls[neighbourhoods["bottom"]].ChangeTexture(11, 1);
                if (neighbourhoods.ContainsKey("bottomleft") && neighbourhoods.ContainsKey("bottomright"))
                    walls[neighbourhoods["bottom"]].ChangeTexture(13, 1);

                if (neighbourhoods.ContainsKey("bottomleft") || neighbourhoods.ContainsKey("bottomright"))
                    change = true;
            }

            if (change)
            {
                textureX = 2;
                textureY = 1;
            }

            walls.Add(new Tile(tileX, tileY, textureX, textureY));
        }

        private Dictionary<string, int> GetNeighbourhood(int tileX, int tileY)
        {
            Dictionary<string, int> neighbourhoods = new Dictionary<string, int>();

            for (int i = 0; i < walls.Count; i++)
            {
                if (walls[i].isWall)
                {
                    if (walls[i].TileX == tileX && walls[i].TileY + 1 == tileY) neighbourhoods.Add("top", i);
                    else if (walls[i].TileX == tileX && walls[i].TileY - 1 == tileY) neighbourhoods.Add("bottom", i);
                    else if (walls[i].TileX + 1 == tileX && walls[i].TileY == tileY) neighbourhoods.Add("right", i);
                    else if (walls[i].TileX - 1 == tileX && walls[i].TileY == tileY) neighbourhoods.Add("left", i);
                    else if (walls[i].TileX == tileX + 1 && walls[i].TileY + 1 == tileY) neighbourhoods.Add("topright", i);
                    else if (walls[i].TileX == tileX - 1 && walls[i].TileY + 1 == tileY) neighbourhoods.Add("topleft", i);
                    else if (walls[i].TileX == tileX + 1 && walls[i].TileY - 1 == tileY) neighbourhoods.Add("bottomright", i);
                    else if (walls[i].TileX == tileX - 1 && walls[i].TileY - 1 == tileY) neighbourhoods.Add("bottomleft", i);
                }
            }

            return neighbourhoods;
        }

        public bool CheckWalls(Vector2 position, int xQuantity, int yQuantity)
        {
            int pointX = (int)position.X / 32;
            int pointY = (int)position.Y / 32;

            if (pointX < 0 || pointY < 0 ||
                pointX + 1 > Warcraft.MAP_SIZE||
                pointY + 1 > Warcraft.MAP_SIZE)
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
