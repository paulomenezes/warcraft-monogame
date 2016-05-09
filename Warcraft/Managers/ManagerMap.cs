using Warcraft.Map;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;
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

            if (!CheckWalls(tileX, tileY))
                walls.Add(new Tile(tileX, tileY, textureX, textureY));
        }

        private bool ArrayEquals(int[] arr1, int[] arr2)
        {
            if (arr1.Length != arr2.Length) return false;

            for (int i = 0; i < arr1.Length; i++)
                if (arr1[i] != arr2[i]) return false;

            return true;
        }

        public void OrganizeWalls()
        {
            for (int i = 0; i < walls.Count; i++)
            {
                if (walls[i].isWall)
                {
                    int[] n = GetNeighbourhood(i, walls[i].TileX, walls[i].TileY);

                    if (ArrayEquals(n, new int[] { 0, 1, 1, 0 })) walls[i].ChangeTexture(0, 1);
                    else if (ArrayEquals(n, new int[] { 0, 0, 0, 1 })) walls[i].ChangeTexture(1, 1);
                    else if (ArrayEquals(n, new int[] { 0, 1, 0, 1 })) walls[i].ChangeTexture(2, 1);
                    else if (ArrayEquals(n, new int[] { 0, 0, 1, 1 })) walls[i].ChangeTexture(4, 1);
                    else if (ArrayEquals(n, new int[] { 0, 1, 1, 1 })) walls[i].ChangeTexture(5, 1);
                    else if (ArrayEquals(n, new int[] { 1, 0, 0, 0 })) walls[i].ChangeTexture(6, 1);
                    else if (ArrayEquals(n, new int[] { 1, 1, 0, 0 })) walls[i].ChangeTexture(7, 1);
                    else if (ArrayEquals(n, new int[] { 1, 0, 1, 0 })) walls[i].ChangeTexture(8, 1);
                    else if (ArrayEquals(n, new int[] { 1, 1, 1, 0 })) walls[i].ChangeTexture(10, 1);
                    else if (ArrayEquals(n, new int[] { 1, 0, 0, 1 })) walls[i].ChangeTexture(11, 1);
                    else if (ArrayEquals(n, new int[] { 1, 1, 0, 1 })) walls[i].ChangeTexture(12, 1);
                    else if (ArrayEquals(n, new int[] { 1, 0, 1, 1 })) walls[i].ChangeTexture(13, 1);
                    else if (ArrayEquals(n, new int[] { 1, 1, 1, 1 })) walls[i].ChangeTexture(14, 1);
                    else if (ArrayEquals(n, new int[] { 0, 0, 0, 0 })) walls[i].ChangeTexture(16, 0);
                    else if (ArrayEquals(n, new int[] { 0, 1, 0, 0 })) walls[i].ChangeTexture(17, 0);
                    else if (ArrayEquals(n, new int[] { 0, 0, 1, 0 })) walls[i].ChangeTexture(18, 0);
                }
            }
        }

        private int[] GetNeighbourhood(int index, int tileX, int tileY)
        {
            int[] neighbourhood = new int[4];
            
            for (int i = 0; i < walls.Count; i++)
            {
                if (walls[i].isWall)
                {
                    if (walls[i].TileX == tileX && walls[i].TileY + 1 == tileY) neighbourhood[3] = 1;
                    if (walls[i].TileX == tileX && walls[i].TileY - 1 == tileY) neighbourhood[1] = 1;
                    if (walls[i].TileX - 1 == tileX && walls[i].TileY == tileY) neighbourhood[2] = 1;
                    if (walls[i].TileX + 1 == tileX && walls[i].TileY == tileY) neighbourhood[0] = 1;
                }
            }

            return neighbourhood;
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

        public bool CheckWalls(int pointX, int pointY)
        {
            if (pointX < 0 || pointY < 0 ||
                pointX + 1 > Warcraft.MAP_SIZE ||
                pointY + 1 > Warcraft.MAP_SIZE)
                return true;

            for (int k = 0; k < walls.Count; k++)
            {
                if (walls[k].TileX == pointX && walls[k].TileY == pointY)
                    return true;
            }

            return false;
        }
    }
}
