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
        public bool alive;
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
            alive = true;
            movementSpeed = 0.001f;
            direction = new Vector2(1, 0);
            thread = new Thread(() => Update());
            thread.IsBackground = true;
            thread.Start();
            sprite = GameWorld.ContentManager.Load<Texture2D>("monster0");
            Position = Vector2.Zero;
        }

        public void Update()
        {
            while (alive)
            {
                Position += direction * movementSpeed * GameWorld.deltaTime;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, Position, Color.White);
        }

        public override void Combat()
        {

        }
    }
}
