using Warcraft.Managers;
using Warcraft.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warcraft.Units
{
    abstract class Unit
    {
        protected Texture2D texture;
        protected Animation animations;

        private Vector2 position;
        private Vector2 goal;

        private bool selected;
        private bool transition;

        private Rectangle rectangle;

        protected int width;
        protected int height;
        protected int speed;

        Pathfinding pathfinding;
        List<Util.Point> path;

        public Unit(int tileX, int tileY, int width, int height, int speed, ManagerMouse managerMouse, ManagerMap managerMap)
        {
            this.width = width;
            this.height = height;
            this.speed = speed;

            pathfinding = new Pathfinding(managerMap);

            position = new Vector2(tileX * Warcraft.TILE_SIZE, tileY * Warcraft.TILE_SIZE);

            managerMouse.MouseEventHandler += ManagerMouse_MouseEventHandler;
            managerMouse.MouseClickEventHandler += ManagerMouse_MouseClickEventHandler;

            rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
        }

        private void ManagerMouse_MouseClickEventHandler(object sender, Events.MouseClickEventArgs e)
        {
            if (selected)
                Move(e.XTile, e.YTile);
        }

        private void ManagerMouse_MouseEventHandler(object sender, Events.MouseEventArgs e)
        {
            if (rectangle.Intersects(e.SelectRectangle))
                selected = true;
            else
                selected = false;
        }

        public abstract void LoadContent(ContentManager content);

        public void Update()
        {
            animations.Update();
            
            if (transition)
            {
                if (position.X < goal.X && position.Y < goal.Y)
                {
                    position.X += speed;
                    position.Y += speed;

                    animations.Play("right");
                }
                else if (position.X < goal.X && position.Y > goal.Y)
                {
                    position.X += speed;
                    position.Y -= speed;
                    animations.Play("left");
                }
                else if (position.X > goal.X && position.Y < goal.Y)
                {
                    position.X -= speed;
                    position.Y += speed;
                    animations.Play("down");
                }
                else if (position.X > goal.X && position.Y > goal.Y)
                {
                    position.X -= speed;
                    position.Y -= speed;
                    animations.Play("up");
                }
                else if (position.X < goal.X)
                {
                    position.X += speed;
                    animations.Play("right");
                }
                else if (position.X > goal.X)
                {
                    position.X -= speed;
                    animations.Play("left");
                }
                else if (position.Y < goal.Y)
                {
                    position.Y += speed;
                    animations.Play("down");
                }
                else if (position.Y > goal.Y)
                {
                    position.Y -= speed;
                    animations.Play("up");
                }

                if (position.X == goal.X && position.Y == goal.Y)
                {
                    if (path.Count > 0)
                    {
                        goal = new Vector2(path.First().x * 32, path.First().y * 32);
                        path.RemoveAt(0);
                    }
                    else
                        transition = false;
                }

                rectangle.X = (int)position.X;
                rectangle.Y = (int)position.Y;
            }
            else
            {
                animations.Stop();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, animations.rectangle, Color.White);

            if (selected)
                SelectRectangle.Draw(spriteBatch, rectangle);
        }

        public void Move(int xTile, int yTile)
        {
            transition = true;

            pathfinding.SetGoal((int)position.X, (int)position.Y, xTile * 32, yTile * 32);
            path = pathfinding.DiscoverPath();

            goal = new Vector2(path.First().x * 32, path.First().y * 32);
            path.RemoveAt(0);
        }
    }
}
