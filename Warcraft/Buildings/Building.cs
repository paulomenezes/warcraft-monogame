using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warcraft.Managers;
using Warcraft.Util;
using Warcraft.Events;
using Microsoft.Xna.Framework.Content;
using Warcraft.Units;
using Microsoft.Xna.Framework.Input;

namespace Warcraft.Buildings
{
    abstract class Building
    {
        public static Texture2D texture;
        public Animation animations;

        public Vector2 position;

        public bool selected;
        protected int width;
        protected int height;

        private Rectangle rectangle;

        public UI.UI ui;
        protected string textureName;

        public Information information;

        public bool isBuilding = false;
        public bool isPlaceSelected = false;
        public bool isStartBuilding = false;
        public bool isWorking = false;

        ManagerMap managerMap;
        
        public Building(int tileX, int tileY, int width, int height, ManagerMouse managerMouse, ManagerMap managerMap)
        {
            this.width = width;
            this.height = height;

            this.managerMap = managerMap;

            position = new Vector2(tileX * Warcraft.TILE_SIZE, tileY * Warcraft.TILE_SIZE);

            managerMouse.MouseEventHandler += ManagerMouse_MouseEventHandler;

            rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
        }

        private void ManagerMouse_MouseEventHandler(object sender, MouseEventArgs e)
        {
            if (!e.UI)
            {
                if (isBuilding && e.SelectRectangle.Width == 0 && e.SelectRectangle.Height == 0 &&
                    !managerMap.CheckWalls(position, width / 32, height / 32))
                {
                    isPlaceSelected = true;
                }

                if (isWorking)
                {
                    if (rectangle.Intersects(e.SelectRectangle))
                        selected = true;
                    else
                        selected = false;
                }
            }
        }

        public void StartBuilding()
        {
            animations.Play("building");
            isStartBuilding = true;

            managerMap.AddWalls(position, width / 32, height / 32);

            rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
        }

        public void LoadContent(ContentManager content)
        {
            if (texture == null)
                texture = content.Load<Texture2D>(textureName);

            ui.LoadContent(content);
        }

        public void Update()
        {
            animations.Update();
            ui.Update();

            if (animations.completed && !isWorking)
            {
                isBuilding = false;
                isPlaceSelected = false;
                isStartBuilding = false;

                isWorking = true;
            }

            if (isBuilding && !isPlaceSelected)
            {
                MouseState mouse = Mouse.GetState();
                position = new Vector2(mouse.X - width / 2, mouse.Y - height / 2);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isBuilding && !isPlaceSelected)
                spriteBatch.Draw(texture, position, new Rectangle(animations.sprites.Last().x, animations.sprites.Last().y, animations.sprites.Last().width, animations.sprites.Last().height), Color.White);
            else if ((isBuilding && isPlaceSelected && isStartBuilding) || isWorking)
                spriteBatch.Draw(texture, position, animations.rectangle, Color.White);

            if (selected)
            {
                SelectRectangle.Draw(spriteBatch, rectangle);
                ui.Draw(spriteBatch);
            }
        }

        public void builder()
        {
            isBuilding = true;
        }
    }
}
