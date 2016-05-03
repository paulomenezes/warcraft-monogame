using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using Warcraft.Buildings.Neutral;
using Warcraft.Events;
using Warcraft.Managers;

namespace Warcraft.UI.Buildings
{
    internal class GoldMine : UI
    {
        private global::Warcraft.Buildings.Neutral.GoldMine goldMine;
        private ManagerMouse managerMouse;

        public GoldMine(ManagerMouse managerMouse, global::Warcraft.Buildings.Neutral.GoldMine goldMine)
        {
            buttonPortrait = new Button(4, 7);

            this.managerMouse = managerMouse;
            this.goldMine = goldMine;

            managerMouse.MouseEventHandler += ManagerMouse_MouseEventHandler;
        }

        private void ManagerMouse_MouseEventHandler(object sender, MouseEventArgs e)
        {
            
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            buttonPortrait.LoadContent(content);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (DrawIndividual)
            {
                buttonPortrait.Draw(spriteBatch);

                spriteBatch.DrawString(font, goldMine.information.Name, new Vector2(minX + 50, 100), Color.Black);
            }
        }
    }
}