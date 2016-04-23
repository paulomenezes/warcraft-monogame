using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using Warcraft.Managers;
using Warcraft.Units.Humans;
using Warcraft.Util;

namespace Warcraft
{
    public class Warcraft : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        ManagerMap managerMap = new ManagerMap();
        ManagerMouse managerMouse = new ManagerMouse();

        ManagerUnits managerUnits;

        public static int WINDOWS_WIDTH = 1216;
        public static int WINDOWS_HEIGHT = 608;
        public static int TILE_SIZE = 32;
        
        public Warcraft()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1216;
            graphics.PreferredBackBufferHeight = 608;

            Content.RootDirectory = "Content";

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            managerUnits = new ManagerUnits(managerMouse, managerMap);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            managerMap.LoadContent(Content);
            managerUnits.LoadContent(Content);
            SelectRectangle.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            managerMouse.Update();
            managerUnits.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            managerMap.Draw(spriteBatch);
            managerMouse.Draw(spriteBatch);
            managerUnits.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
