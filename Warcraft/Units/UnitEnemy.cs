using System;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Managers;
using Warcraft.Util;
using Warcraft.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace Warcraft.Units
{
    class UnitEnemy : Unit
    {
        int currentPatrol = 0;
        int[,] patrol;
        
        public Unit target;

        Vector2 lastPosition;

        Point oldAdjust;

        Vector2 targetPosition;
        Vector2 missilePosition;
        Texture2D missile;

        bool shoot = false;
        float angle = 0;

        public UnitEnemy(int tileX, int tileY, int width, int height, int speed, ManagerMouse managerMouse, ManagerMap managerMap, ManagerBuildings managerBuildings) 
            : base(tileX, tileY, width, height, speed, managerMouse, managerMap, managerBuildings)
        {

        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            missile = content.Load<Texture2D>("axe");
            
            Random rng = new Random();
            if (information.Spawn == 0)
            {
                position = new Vector2(0, 25 * 32);

                patrol = new int[4, 2] { 
                    { rng.Next(0, 25), rng.Next(0, 25) }, 
                    { rng.Next(25, 50), rng.Next(0, 25) }, 
                    { rng.Next(25, 50), rng.Next(25, 50) }, 
                    { rng.Next(0, 25), rng.Next(25, 50) }
                };
            }
            else if (information.Spawn == 1)
            {
                position = new Vector2(25 * 32, 0);

                patrol = new int[4, 2] {
                    { rng.Next(25, 50), rng.Next(0, 25) },
                    { rng.Next(25, 50), rng.Next(25, 50) },
                    { rng.Next(0, 25), rng.Next(25, 50) },
                    { rng.Next(0, 25), rng.Next(0, 25) }
                };
            }
            else if (information.Spawn == 2)
            {
                position = new Vector2(50 * 32, 25 * 32);

                patrol = new int[4, 2] {
                    { rng.Next(25, 50), rng.Next(25, 50) },
                    { rng.Next(0, 25), rng.Next(25, 50) },
                    { rng.Next(0, 25), rng.Next(0, 25) },
                    { rng.Next(25, 50), rng.Next(0, 25) }
                };
            }
            else if (information.Spawn == 3)
            {
                position = new Vector2(25 * 32, 50 * 32);

                patrol = new int[4, 2] {
                    { rng.Next(0, 25), rng.Next(25, 50) },
                    { rng.Next(0, 25), rng.Next(0, 25) },
                    { rng.Next(25, 50), rng.Next(0, 25) },
                    { rng.Next(25, 50), rng.Next(25, 50) }
                };
            }
        }

        public override void Update()
        {
            base.Update();
            
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
                int x = 0, y = 0;
                if (animations.current.ToLower().Contains("down"))
                    y = -1;
                if (animations.current.ToLower().Contains("up"))
                    y = 1;
                if (animations.current.ToLower().Contains("left"))
                    x = 1;
                if (animations.current.ToLower().Contains("right"))
                    x = -1;

                int adjustX = ((int)target.position.X - (int)position.X) / 32;
                int adjustY = ((int)target.position.Y - (int)position.Y) / 32;

                float distance = Vector2.Distance(target.position, position);

                if (adjustX == 5 && oldAdjust.X == 6 && adjustY == 6 && oldAdjust.Y == 5)
                {
                    adjustX = 4;
                    adjustY = 4;
                }

                if (!shoot)
                    missilePosition = position;
                
                if ((Math.Abs(adjustX) > information.Range || Math.Abs(adjustY) > information.Range) && lastPosition != position)
                {
                    Move((int)Math.Max(0, target.position.X / 32 + information.Range * x), 
                         (int)Math.Max(0, target.position.Y / 32 + information.Range * y));
                    lastPosition = position;

                    animations.currentAnimation = AnimationType.WALKING;
                    animations.Play(animations.current);

                    if (information.Type == Util.Units.TROLL_AXETHROWER)
                    {
                        missilePosition = position;
                        targetPosition = target.position;
                        shoot = false;
                    }
                } 
                else //if (!transition)
                {
                    if (information.Type == Util.Units.TROLL_AXETHROWER)
                    {
                        if (targetPosition == Vector2.Zero)
                            targetPosition = target.position;

                        transition = false;
                        shoot = true;

                        angle += 0.1f;

                        Vector2 difference = targetPosition - missilePosition;
                        difference.Normalize();

                        missilePosition += difference * 5f;

                        if ((int)Vector2.Distance(missilePosition, targetPosition) <= 2)
                        {
                            angle = 0;

                            missilePosition = position;
                            targetPosition = target.position;

                            float reduce = ((information.Damage * ((float)information.Precision / 100)) - target.information.Armor) / 30;
                            target.information.HitPoints -= reduce < 0 ? 0.01f : reduce;
                        }
                    }
                    else
                    {
                        float reduce = ((information.Damage * ((float)information.Precision / 100)) - target.information.Armor) / 30;
                        target.information.HitPoints -= reduce < 0 ? 0.01f : reduce;
                    }
                    
                    if (adjustX > 0 && adjustY == 0) animations.Change("right");
                    else if (adjustX > 0 && adjustY > 0) animations.Change("downRight");
                    else if (adjustX == 0 && adjustY > 0) animations.Change("down");
                    else if (adjustX < 0 && adjustY > 0) animations.Change("downLeft");
                    else if (adjustX < 0 && adjustY == 0) animations.Change("left");
                    else if (adjustX < 0 && adjustY < 0) animations.Change("upLeft");
                    else if (adjustX == 0 && adjustY < 0) animations.Change("up");
                    else if (adjustX > 0 && adjustY < 0) animations.Change("upRight");

                    animations.currentAnimation = AnimationType.ATTACKING;
                    animations.Play(animations.current);
                }

                oldAdjust = new Point(adjustX, adjustY);
                
                if (Math.Abs(adjustX) > 4 + information.Range || Math.Abs(adjustY) > 4 + information.Range || target.information.HitPoints <= 0 || target.workState != WorkigState.NOTHING)
                {
                    target = null;
                    shoot = false;

                    animations.currentAnimation = AnimationType.WALKING;
                    animations.Play(animations.current);
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (shoot && animations.currentAnimation == AnimationType.ATTACKING)
                spriteBatch.Draw(missile, missilePosition + new Vector2(15, 15), new Rectangle(5, 5, 19, 20), Color.White, angle, new Vector2(9.5f, 10), 1f, SpriteEffects.None, 0);

            base.Draw(spriteBatch);
        }
    }
}
