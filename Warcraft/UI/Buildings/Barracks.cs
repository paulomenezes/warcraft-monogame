using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warcraft.Managers;
using BarracksBuilding = Warcraft.Buildings.Humans.Barracks;

namespace Warcraft.UI.Buildings
{
    class Barracks : UI
    {
        BarracksBuilding barracks;

        public Barracks(ManagerMouse managerMouse, BarracksBuilding barracks)
        {
            buttonPortrait = new Button(2, 4);

            this.barracks = barracks;

            if (managerMouse != null)
                managerMouse.MouseEventHandler += ManagerMouse_MouseEventHandler;
        }

        private void ManagerMouse_MouseEventHandler(object sender, Events.MouseEventArgs e)
        {
            if (barracks.selected && e.SelectRectangle.Width == 0 && e.SelectRectangle.Height == 0)
            {

            }
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            buttonPortrait.LoadContent(content);
        }

        public override void Update()
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (DrawIndividual)
            {
                buttonPortrait.Draw(spriteBatch);

                spriteBatch.DrawString(font, barracks.information.Name, new Vector2(minX + 50, 100), Color.Black);
            }
        }
    }
}
