using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Warlock_The_Soulbinder
{
    public class Enemy : CharacterCombat
    {
        private string monster;
        private int level;
        private float moveCDTimer;
        private float movingTimer;

        public bool alive = true;
        Thread thread;
        enum EMonster
        {
            bear, sheep, wolf, //neutral (0,1,2)
            bucketMan, defender, sentry, //metal (3,4,5)
            plantEater, insectSoldier, slimeEater, //earth (6,7,8)
            falcon, bat, raven, //air (9,10,11)
            fireGolem, infernalGolem, ashZombie, //fire (12,13,14)
            mummy, vampire, banshee, //dark (15,16,17)
            tentacle, frog, fish //water (18,19,20)
        };

        public Enemy(int index) : base(index)
        {
            monster = Enum.GetName(typeof(EMonster), index);
            sprite = GameWorld.ContentManager.Load<Texture2D>($"monsters/{monster}");
            movementSpeed = 10;
            Position = new Vector2(500);
            thread = new Thread(() => Update());
            thread.IsBackground = true;
            thread.Start();
        }

        public void Update()
        {
            
            while (alive)
            {
                if (!isInCombat)
                {
                    moveCDTimer += (float)GameWorld.deltaTime;
                    if (moveCDTimer > 10)
                    {
                        Move();
                        movingTimer += (float)GameWorld.deltaTime;
                        if (movingTimer > 10)
                        {
                            moveCDTimer = 0;
                            movingTimer = 0;
                        }
                    }
                }
                Thread.Sleep(1);
            }
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        private void Move()
        {
            if (movingTimer == 0)
            {
                direction.X = GameWorld.Instance.RandomInt(-1, 2); //adds a random direction vector to X
                direction.Y = GameWorld.Instance.RandomInt(-1, 2); //adds a random direction vector to Y
                if (direction != Vector2.Zero)
                {
                    direction.Normalize();
                }
                direction *= movementSpeed * (float)GameWorld.deltaTime; //adds movement speed to direction keeping in time with deltaTime
            }
            Position += direction; //moves the enemy based on direction
        }
    }
}
