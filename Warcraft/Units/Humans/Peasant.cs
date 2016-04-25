using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Util;
using Warcraft.Managers;
using Warcraft.Commands;

namespace Warcraft.Units.Humans
{
    class Peasant : Unit
    {
        public List<ICommand> commands = new List<ICommand>();

        public Peasant(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerBuildings managerBuildings) 
            : base(tileX, tileY, 32, 32, 2, managerMouse, managerMap, managerBuildings)
        {
            List<Sprite> sprites = new List<Sprite>();
            // UP
            sprites.Add(new Sprite(16, 8, 26, 23));
            sprites.Add(new Sprite(18, 46, 24, 28));
            sprites.Add(new Sprite(17, 86, 25, 26));
            sprites.Add(new Sprite(19, 122, 23, 30));
            sprites.Add(new Sprite(18, 159, 24, 27));
            // DOWN
            sprites.Add(new Sprite(166, 7, 25, 26));
            sprites.Add(new Sprite(168, 45, 24, 26));
            sprites.Add(new Sprite(167, 85, 25, 27));
            sprites.Add(new Sprite(168, 121, 23, 26));
            sprites.Add(new Sprite(167, 158, 24, 27));
            // LEFT
            sprites.Add(new Sprite(97, 4, 14, 31));
            sprites.Add(new Sprite(91, 42, 24, 30));
            sprites.Add(new Sprite(96, 82, 16, 31));
            sprites.Add(new Sprite(91, 118, 23, 30));
            sprites.Add(new Sprite(95, 155, 20, 30));
            // UP-RIGHT
            sprites.Add(new Sprite(56, 6, 22, 26));
            sprites.Add(new Sprite(55, 44, 26, 30));
            sprites.Add(new Sprite(56, 84, 24, 29));
            sprites.Add(new Sprite(59, 119, 23, 29));
            sprites.Add(new Sprite(57, 156, 21, 28));
            // DOWN-RIGHT
            sprites.Add(new Sprite(127, 3, 22, 31));
            sprites.Add(new Sprite(128, 40, 20, 27));
            sprites.Add(new Sprite(130, 80, 19, 28));
            sprites.Add(new Sprite(126, 119, 26, 29));
            sprites.Add(new Sprite(126, 156, 26, 28));

            Dictionary<string, Frame> animations = new Dictionary<string, Frame>();
            animations.Add("up", new Frame(0, 5));
            animations.Add("down", new Frame(5, 5));
            animations.Add("right", new Frame(10, 5));
            animations.Add("left", new Frame(10, 5, true));
            animations.Add("upRight", new Frame(15, 5));
            animations.Add("downRight", new Frame(20, 5));
            animations.Add("upLeft", new Frame(15, 5, true));
            animations.Add("downLeft", new Frame(20, 5, true));

            this.animations = new Animation(sprites, animations, "down", width, height);

            ui = new UI.Units.Peasant(managerMouse, this);
            textureName = "Peasant_walking";

            information = new InformationUnit("Peasant", Race.HUMAN, Faction.ALLIANCE, 30, 0, 4, 10, 400, 1, Util.Buildings.TOWN_HALL, 45, 1, 5, 1);

            commands.Add(new Builder(Util.Buildings.TOWN_HALL, this, managerMouse, managerBuildings));
            commands.Add(new Builder(Util.Buildings.BARRACKS, this, managerMouse, managerBuildings));
            commands.Add(new Builder(Util.Buildings.CHICKEN_FARM, this, managerMouse, managerBuildings));
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            for (int i = 0; i < commands.Count; i++)
                (commands[i] as Builder).LoadContent(content);
        }

        public override void Update()
        {
            base.Update();

            for (int i = 0; i < commands.Count; i++)
            {
                if (workState == WorkigState.WORKING && !(commands[i] as Builder).building.isStartBuilding)
                    (commands[i] as Builder).building.StartBuilding();

                if (workState == WorkigState.WAITING_PLACE && (commands[i] as Builder).building.isPlaceSelected && !(commands[i] as Builder).building.isStartBuilding)
                {
                    Move((int)(commands[i] as Builder).building.position.X / 32, (int)(commands[i] as Builder).building.position.Y / 32);
                    workState = WorkigState.GO_TO_WORK;
                    selected = false;
                }

                (commands[i] as Builder).Update();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            for (int i = 0; i < commands.Count; i++)
                (commands[i] as Builder).Draw(spriteBatch);
        }
    }
}
