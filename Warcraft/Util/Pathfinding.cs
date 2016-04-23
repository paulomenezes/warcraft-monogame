﻿using Warcraft.Managers;
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
    class Point
    {
        public int x;
        public int y;

        public int F;
        public int G;
        public int H;

        public Point parent;

        public Point(int x, int y, int g, int h, Point parent)
        {
            this.x = x;
            this.y = y;

            this.F = g + h;
            this.G = g;
            this.H = h;

            this.parent = parent;
        }
    }

    class Pathfinding
    {
        List<Point> openList = new List<Point>();
        List<Point> closeList = new List<Point>();

        ManagerMap managerMap;

        int[][] neighbourhood = { new int[] { -1, 0 }, new int[] { 1, 0 }, new int[] { 0, -1 }, new int[] { 0, 1 },
                                  new int[] { -1, -1 }, new int[] { -1, 1 }, new int[] { 1, -1 }, new int[] { 1, 1 } };

        int goalX;
        int goalY;

        public Pathfinding(ManagerMap managerMap)
        {
            this.managerMap = managerMap;
        }

        public bool SetGoal(int posX, int posY, int goalX, int goalY)
        {
            posX = posX / 32;
            posY = posY / 32;

            goalX = goalX / 32;
            goalY = goalY / 32;

            this.goalX = goalX;
            this.goalY = goalY;

            Point current = new Point(posX, posY, 0, 0, null);

            openList.Clear();
            closeList.Clear();

            openList.Add(current);
            closeList.Add(current);

            FindNeighbourhood(0);

            return false;
        }

        public List<Point> DiscoverPath()
        {
            while (openList.Count > 0)
            {
                int F = openList[0].F;
                int index = 0;
                for (int i = 1; i < openList.Count; i++)
                {
                    if (openList[i].F < F)
                    {
                        index = i;
                        F = openList[i].F;
                    }
                }

                closeList.Add(openList[index]);
                Point current = openList[index];

                if (current.x == goalX && current.y == goalY)
                    break;

                FindNeighbourhood(index);
            }

            return FindPath();
        }

        public void FindNeighbourhood(int index)
        {
            int count = openList.Count;
            for (int j = 0; j < count; j++)
            {
                for (int i = 0; i < neighbourhood.Length; i++)
                {
                    int x = openList[j].x + neighbourhood[i][0];
                    int y = openList[j].y + neighbourhood[i][1];

                    int h = Math.Abs(x - goalX) + Math.Abs(y - goalY);

                    if (!CheckWalls(x, y) && !CheckOpen(x, y))
                    {
                        Point c = new Point(x, y, i > 3 ? 14 : 10, h, openList[j]);
                        openList.Add(c);
                    }
                }
            }

            openList.RemoveAt(index);
        }

        public List<Point> FindPath()
        {
            List<Point> path = new List<Point>();
            Point p = closeList.Last();
            while (p.parent != null)
            {
                path.Add(p);
                p = p.parent;
            }

            path.Reverse();

            return path;
        }

        private bool CheckWalls(int pointX, int pointY)
        {
            for (int i = 0; i < managerMap.walls.Count; i++)
            {
                if ((managerMap.walls[i].TileX == pointX && managerMap.walls[i].TileY == pointY) || pointX < 0 || pointY < 0 || 
                    pointX + 1 > Warcraft.WINDOWS_WIDTH / Warcraft.TILE_SIZE || 
                    pointY + 1 > Warcraft.WINDOWS_HEIGHT / Warcraft.TILE_SIZE)
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckOpen(int pointX, int pointY)
        {
            return openList.Any(i => i.x == pointX && i.y == pointY) || closeList.Any(i => i.x == pointX && i.y == pointY);
        }
    }
}