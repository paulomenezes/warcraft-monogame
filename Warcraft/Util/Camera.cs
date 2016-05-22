using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Warcraft.Util
{
    public class Camera
    {
        public Matrix transform;
        public Vector2 center = new Vector2(32 * 25 - Warcraft.WINDOWS_HEIGHT / 2, 32 * 32 - Warcraft.WINDOWS_HEIGHT / 2);
        Viewport view;

        float speed = 4;

        public Camera(Viewport viewport)
        {
            view = viewport;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            if (keyboard.IsKeyDown(Keys.Left)) center.X -= speed;
            if (keyboard.IsKeyDown(Keys.Right)) center.X += speed;
            if (keyboard.IsKeyDown(Keys.Up)) center.Y -= speed;
            if (keyboard.IsKeyDown(Keys.Down)) center.Y += speed;

            if (mouse.X > Warcraft.WINDOWS_WIDTH + 100 && mouse.X < Warcraft.WINDOWS_WIDTH + 200) center.X += speed;
            if (mouse.Y > Warcraft.WINDOWS_HEIGHT - 100 && mouse.Y < Warcraft.WINDOWS_HEIGHT) center.Y += speed;
            if (mouse.X > 0 && mouse.X < 100) center.X -= speed;
            if (mouse.Y > 0 && mouse.Y < 100) center.Y -= speed;

            center.X = Math.Max(center.X, 0);
            center.Y = Math.Max(center.Y, 0);

            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0));
        }
    }
}
