using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Warcraft.Util
{
    class Camera
    {
        public Matrix transform;
        Viewport view;
        Vector2 center;

        float speed = 4;

        public Camera(Viewport viewport)
        {
            view = viewport;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Left)) center.X -= speed;
            if (keyboard.IsKeyDown(Keys.Right)) center.X += speed;
            if (keyboard.IsKeyDown(Keys.Up)) center.Y -= speed;
            if (keyboard.IsKeyDown(Keys.Down)) center.Y += speed;

            center.X = Math.Max(center.X, 0);
            center.Y = Math.Max(center.Y, 0);

            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0));
        }
    }
}
