using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        public static int WINDOWS_WIDTH = 1216;
        public static int WINDOWS_HEIGHT = 608;
        public static int TILE_SIZE = 32;

        Peasant peasant;

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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            peasant = new Peasant(4, 5, managerMouse, managerMap);

            managerMap.LoadContent(Content);
            SelectRectangle.LoadContent(Content);

            peasant.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            managerMouse.Update();
            peasant.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            managerMap.Draw(spriteBatch);
            managerMouse.Draw(spriteBatch);
            peasant.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
