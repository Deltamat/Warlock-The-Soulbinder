using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    public class Enemy : CharacterCombat
    {
        private string monster;
        private int level;
        private float moveCDTimer;
        private float movingTimer;
        
        private Thread thread;

        enum EMonster
        {
            sheep, wolf, bear, //neutral (0,1,2)
            plantEater, insectSoldier, slimeEater, //earth (3,4,5)
            tentacle, frog, fish, //water (6,7,8)
            mummy, vampire, banshee, //dark (9,10,11)
            bucketMan, defender, sentry, //metal (12,13,14)
            fireGolem, infernalDemon, ashZombie, //fire (15,16,17)
            falcon, bat, raven //air (18,19,20)
        };

        /// <summary>
        /// Returns the collisionbox for the object
        /// </summary>
        public override Rectangle CollisionBox
        {
            get
            {
                return new Rectangle((int)(Position.X), (int)(Position.Y), (int)(sprite.Width * scale), (int)(sprite.Height * scale));
            }
        }

        public Enemy(int index, Vector2 startPos)
        {
            monster = Enum.GetName(typeof(EMonster), index);
            sprite = GameWorld.ContentManager.Load<Texture2D>($"monsters/{monster}");
            scale = 0.25f;

            movementSpeed = 10;
            Position = startPos;

            level = index + GameWorld.Instance.RandomInt(-1, 2);
            if (level <= 0)
            {
                level = 1;
            }

            //base stats
            #region
            Defense = (int)(10 * ((level + GameWorld.Instance.RandomInt(1, 4)) * 0.1f));
            Damage = (int)(10 * ((level + GameWorld.Instance.RandomInt(1, 5)) * 0.2f));
            maxHealth = (int)(10 * ((level + GameWorld.Instance.RandomInt(1, 6)) * 1.25f));
            currentHealth = 0 + maxHealth;
            attackSpeed = 5 * (level * 0.5f) + GameWorld.Instance.RandomInt(-1, 3);
            metalResistance = (float)Math.Log(10 * (level * 0.15f) + GameWorld.Instance.RandomInt(1, 5));
            earthResistance = (float)Math.Log(10 * (level * 0.15f) + GameWorld.Instance.RandomInt(1, 5));
            airResistance = (float)Math.Log(10 * (level * 0.15f) + GameWorld.Instance.RandomInt(1, 5));
            fireResistance = (float)Math.Log(10 * (level * 0.15f) + GameWorld.Instance.RandomInt(1, 5));
            darkResistance = (float)Math.Log(10 * (level * 0.15f) + GameWorld.Instance.RandomInt(1, 5));
            waterResistance = (float)Math.Log(10 * (level * 0.15f) + GameWorld.Instance.RandomInt(1, 5));
            #endregion

            //switch case to determine special properties based on the monster's element (logistic function)
            switch (monster)
            {
                case "bear":
                case "sheep":
                case "wolf":
                    Defense = (int)(Defense * (level * 0.75f));
                    break;
                case "plantEater":
                case "insectSoldier":
                case "slimeEater":
                    earthResistance *= (float)(20 / (1 + Math.Pow(Math.E, -(level * 0.5f))));
                    darkResistance = (float)(darkResistance * (-20 / (1 + Math.Pow(Math.E, -(level * 0.5f)))) + level * 0.5f);
                    earthDamage = (int)(damage * 0.8f);
                    damage = (int)(damage * 0.2f);
                    break;
                case "tentacle":
                case "frog":
                case "fish":
                    waterResistance *= (float)(20 / (1 + Math.Pow(Math.E, -(level * 0.5f))));
                    airResistance = (float)(airResistance * (-20 / (1 + Math.Pow(Math.E, -(level * 0.5f)))) + level * 0.5f);
                    waterDamage = (int)(damage * 0.8f);
                    damage = (int)(damage * 0.2f);
                    break;
                case "mummy":
                case "vampire":
                case "banshee":
                    darkResistance *= (float)(20 / (1 + Math.Pow(Math.E, -(level * 0.5f))));
                    metalResistance = (float)(metalResistance * (-20 / (1 + Math.Pow(Math.E, -(level * 0.5f)))) + level * 0.5f);
                    darkDamage = (int)(damage * 0.8f);
                    damage = (int)(damage * 0.2f);
                    break;
                case "bucketMan":
                case "defender":
                case "sentry":
                    metalResistance *= (float)(20 / (1 + Math.Pow(Math.E, -(level * 0.5f))));
                    fireResistance = (float)(fireResistance * (-20 / (1 + Math.Pow(Math.E, -(level * 0.5f)))) + level * 0.5f);
                    earthDamage = (int)(damage * 0.8f);
                    damage = (int)(damage * 0.2f);
                    break;
                case "fireGolem":
                case "infernalDemon":
                case "ashZombie":
                    fireResistance *= (float)(20 / (1 + Math.Pow(Math.E, -(level * 0.5f))));
                    waterResistance = (float)(waterResistance * (-20 / (1 + Math.Pow(Math.E, -(level * 0.5f)))) + level * 0.5f);
                    fireDamage = (int)(damage * 0.8f);
                    damage = (int)(damage * 0.2f);
                    break;
                case "falcon":
                case "bat":
                case "raven":
                    airResistance *= (float)(20 / (1 + Math.Pow(Math.E, -(level * 0.5f))));
                    earthResistance = (float)(earthResistance * (-20 / (1 + Math.Pow(Math.E, -(level * 0.5f)))) + level * 0.5f);
                    airDamage = (int)(damage * 0.8f);
                    damage = (int)(damage * 0.2f);
                    break;
            }

            //adds damage and resistances to lists for ease of use
            #region
            ResistanceTypes.Add(earthResistance);
            ResistanceTypes.Add(waterResistance);
            ResistanceTypes.Add(darkResistance);
            ResistanceTypes.Add(metalResistance);
            ResistanceTypes.Add(fireResistance);
            ResistanceTypes.Add(airResistance);
            DamageTypes.Add(earthDamage);
            DamageTypes.Add(waterDamage);
            DamageTypes.Add(darkDamage);
            DamageTypes.Add(metalDamage);
            DamageTypes.Add(fireDamage);
            DamageTypes.Add(airDamage);
            #endregion

            thread = new Thread(() => Update());
            thread.IsBackground = true;
            thread.Start();
        }

        /// <summary>
        /// contructor used to load the database
        /// </summary>
        /// <param name="level"></param>
        /// <param name="startPos"></param>
        /// <param name="defense"></param>
        /// <param name="damage"></param>
        /// <param name="maxHealth"></param>
        /// <param name="attackSpeed"></param>
        /// <param name="metalResistance"></param>
        /// <param name="earthResistance"></param>
        /// <param name="airResistance"></param>
        /// <param name="fireResistance"></param>
        /// <param name="darkResistance"></param>
        /// <param name="waterResistance"></param>
        /// <param name="monster"></param>
        public Enemy(int level, Vector2 startPos, int defense, int damage, int maxHealth, 
            float attackSpeed, float metalResistance, float earthResistance, float airResistance, float fireResistance, float darkResistance, 
            float waterResistance, string monster)
        {
            this.monster = monster;
            sprite = GameWorld.ContentManager.Load<Texture2D>($"monsters/{monster}");
            scale = 0.25f;

            movementSpeed = 10;
            Position = startPos;

            this.level = level;


            //base stats
            #region
            Defense = defense;
            Damage = damage;
            this.maxHealth = maxHealth;
            currentHealth = 0 + maxHealth;
            this.attackSpeed = attackSpeed;
            this.metalResistance = metalResistance;
            this.earthResistance = earthResistance;
            this.airResistance = airResistance;
            this.fireResistance = fireResistance;
            this.darkResistance = darkResistance;
            this.waterResistance = waterResistance;
            #endregion

            //switch case to determine special properties based on the monster's element (logistic function)
            switch (monster)
            {
                case "bear":
                case "sheep":
                case "wolf":
                    break;
                case "plantEater":
                case "insectSoldier":
                case "slimeEater":
                    earthDamage = (int)(Damage * 4f);
                    break;
                case "tentacle":
                case "frog":
                case "fish":
                    waterDamage = (int)(Damage * 4f);
                    break;
                case "mummy":
                case "vampire":
                case "banshee":
                    darkDamage = (int)(Damage * 4f);
                    break;
                case "bucketMan":
                case "defender":
                case "sentry":
                    earthDamage = (int)(Damage * 4f);
                    break;
                case "fireGolem":
                case "infernalDemon":
                case "ashZombie":
                    fireDamage = (int)(Damage * 4f);
                    break;
                case "falcon":
                case "bat":
                case "raven":
                    airDamage = (int)(Damage * 4f);
                    break;
            }
            
            //adds damage and resistances to lists for ease of use
            #region
            ResistanceTypes.Add(this.earthResistance);
            ResistanceTypes.Add(this.waterResistance);
            ResistanceTypes.Add(this.darkResistance);
            ResistanceTypes.Add(this.metalResistance);
            ResistanceTypes.Add(this.fireResistance);
            ResistanceTypes.Add(this.airResistance);
            DamageTypes.Add(earthDamage);
            DamageTypes.Add(waterDamage);
            DamageTypes.Add(darkDamage);
            DamageTypes.Add(metalDamage);
            DamageTypes.Add(fireDamage);
            DamageTypes.Add(airDamage);
            #endregion

            thread = new Thread(() => Update());
            thread.IsBackground = true;
            thread.Start();
        }
        
        public void Update()
        {
            Thread.Sleep(GameWorld.Instance.RandomInt(1, 1000));
            while (Alive)
            {
                if (GameWorld.Instance.GameState == "Overworld")
                {
                    moveCDTimer += (float)GameWorld.deltaTimeSecond;
                    if (moveCDTimer > 10) //time between moving
                    {
                        Move();
                        movingTimer += (float)GameWorld.deltaTimeSecond;
                        if (movingTimer > 10) //for how long the enemy moves
                        {
                            moveCDTimer = 0;
                            movingTimer = 0;
                        }
                    }

                    //enemy collision with gameworld
                    foreach (Rectangle rectangle in GameWorld.Instance.CurrentZone().CollisionRects)
                    {
                        if (CollisionBox.Intersects(rectangle))
                        {
                            Position -= direction;
                        }
                    }

                    //enemy collision with enemies
                    foreach (Enemy enemy in GameWorld.Instance.enemies)
                    {
                        if (CollisionBox.Intersects(enemy.CollisionBox) && enemy != this)
                        {
                            Position -= direction;
                        }
                    }
                }
                Thread.Sleep(1);
            }
        }
        
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, Position, null, Color.White, 0f, Vector2.Zero, scale, new SpriteEffects(), 1f);
        }

        /// <summary>
        /// Moves the enemy in a random octagonal direction
        /// </summary>
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
                direction *= movementSpeed * (float)GameWorld.deltaTimeSecond; //adds movement speed to direction keeping in time with deltaTimeSecond
            }
            Position += direction; //moves the enemy based on direction
        }

        public static int ReturnMonsterIndex(string monster)
        {
            return System.Convert.ToInt32(Enum.Parse(typeof(EMonster), monster));
        }
    }
}
