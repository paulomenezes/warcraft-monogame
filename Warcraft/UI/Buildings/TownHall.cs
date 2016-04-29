using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warcraft.Managers;
using TownHallBuilding = Warcraft.Buildings.Humans.TownHall;

namespace Warcraft.UI.Buildings
{
    class TownHall : UI
    {
        TownHallBuilding townHall;
        List<Button> builder = new List<Button>();

        public TownHall(ManagerMouse managerMouse, TownHallBuilding townHall)
        {
            buttonPortrait = new Button(0, 4);

            builder.Add(new Button(0, 260, 0, 0));

            this.townHall = townHall;

            if (managerMouse != null)
                managerMouse.MouseEventHandler += ManagerMouse_MouseEventHandler;
        }

        private void ManagerMouse_MouseEventHandler(object sender, Events.MouseEventArgs e)
        {
            if (townHall.selected && e.SelectRectangle.Width == 0 && e.SelectRectangle.Height == 0)
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

                builder.ForEach((b) => b.Draw(spriteBatch));

                spriteBatch.DrawString(font, townHall.information.Name, new Vector2(minX + 50, 100), Color.Black);
                spriteBatch.DrawString(font, "Gold: " + 0, new Vector2(minX, 150), Color.Black);
                spriteBatch.DrawString(font, "Wood: " + 0, new Vector2(minX, 170), Color.Black);
                spriteBatch.DrawString(font, "Oil: " + 0, new Vector2(minX, 190), Color.Black);
            }
        }
    }
}
