using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Managers;
using Warcraft.Util;
using Warcraft.UI;
using Microsoft.Xna.Framework;

namespace Warcraft.Units
{
    class UnitEnemy : Unit
    {
        int currentPatrol = 0;
        int[,] patrol = new int[4, 2] { { 20, 5 }, { 15, 15 }, { 5, 15 }, { 5, 5 } };

        public Rectangle enemyDetect;
        public Unit target;

        Vector2 lastPosition;
         
        public UnitEnemy(int tileX, int tileY, int width, int height, int speed, ManagerMouse managerMouse, ManagerMap managerMap, ManagerBuildings managerBuildings) 
            : base(tileX, tileY, width, height, speed, managerMouse, managerMap, managerBuildings)
        {
            enemyDetect = new Rectangle(0, 0, 32, 32);
        }

        public override void Update()
        {
            base.Update();

            int x = 0, y = 0;
            if (animations.current.ToLower().Contains("down"))
                y = 32;
            if (animations.current.ToLower().Contains("up"))
                y = -32;
            if (animations.current.ToLower().Contains("left"))
                x = -32;
            if (animations.current.ToLower().Contains("right"))
                x = 32;

            enemyDetect.X = rectangle.X + x;
            enemyDetect.Y = rectangle.Y + y;

            if (target == null)
            {
                if (!transition)
                {
                    Move(patrol[currentPatrol, 0], patrol[currentPatrol, 1]);
                    currentPatrol++;

                    if (currentPatrol > 3)
                        currentPatrol = 0;
                }
            }
            else
            {
                int adjustX = ((int)target.position.X - (int)position.X) / 32;
                int adjustY = ((int)target.position.Y - (int)position.Y) / 32;

                int adjustXX = ((int)target.position.X - (int)lastPosition.X) / 32;
                int adjustYY = ((int)target.position.Y - (int)lastPosition.Y) / 32;

                if (Math.Abs(adjustX) > 1 || Math.Abs(adjustY) > 1)
                {
                    Move((int)target.position.X / 32 + x / 32, (int)target.position.Y / 32 + y / 32);
                    lastPosition = target.position;

                    animations.currentAnimation = AnimationType.WALKING;
                    animations.Play(animations.current);
                } 
                else if (!transition)
                {
                    if (adjustX == 1 && adjustY == 0) animations.Change("right");
                    else if (adjustX == 1 && adjustY == 1) animations.Change("downRight");
                    else if (adjustX == 0 && adjustY == 1) animations.Change("down");
                    else if (adjustX == -1 && adjustY == 1) animations.Change("downLeft");
                    else if (adjustX == -1 && adjustY == 0) animations.Change("left");
                    else if (adjustX == -1 && adjustY == -1) animations.Change("upLeft");
                    else if (adjustX == 0 && adjustY == -1) animations.Change("up");
                    else if (adjustX == 1 && adjustY == -1) animations.Change("upRight");

                    animations.currentAnimation = AnimationType.ATTACKING;
                    animations.Play(animations.current);

                    float reduce = (information.Damage * ((float)information.Precision / 100)) / 30;
                    target.information.HitPoints -= reduce;
                }

                if (Math.Abs(adjustX) > 4 || Math.Abs(adjustY) > 4 || target.information.HitPoints <= 0)
                {
                    target = null;

                    animations.currentAnimation = AnimationType.WALKING;
                    animations.Play(animations.current);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            
            SelectRectangle.Draw(spriteBatch, enemyDetect);
        }
    }
}
