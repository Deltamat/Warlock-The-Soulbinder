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
        private FilledStone enemyStone;
        private FilledStone dragonStone;
        public override int CurrentHealth
        {
            get => base.CurrentHealth;
            set
            {
                if (value < base.CurrentHealth)
                {
                    Combat.Instance.EnemyScrolling($"HP -{base.CurrentHealth - value}", Color.Red);
                }
                else if (value > base.CurrentHealth)
                {
                    Combat.Instance.EnemyScrolling($"HP +{value - base.CurrentHealth}", Color.Green);
                }
                base.CurrentHealth = value;
            }            
        }

        private bool dragon = false;
        
        private Thread thread;

        enum EMonster
        {
            sheep, wolf, bear, //neutral (0,1,2)
            plantEater, insectSoldier, slimeSnake, //earth (3,4,5)
            tentacle, frog, fish, //water (6,7,8)
            mummy, vampire, banshee, //dark (9,10,11)
            bucketMan, defender, sentry, //metal (12,13,14)
            fireGolem, infernalDemon, ashZombie, //fire (15,16,17)
            falcon, bat, raven, //air (18,19,20)
            neutralDragon, earthDragon, waterDragon, darkDragon, metalDragon, fireDragon, airDragon //dragons (21,22,23,24,25,26,27)
        };

        public int Level { get => level; set => level = value; }
        public string Monster { get => monster; set => monster = value; }

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

        internal FilledStone EnemyStone { get => enemyStone; set => enemyStone = value; }
        public bool Dragon { get => dragon; set => dragon = value; }
        internal FilledStone DragonStone { get => dragonStone; set => dragonStone = value; }

        public Enemy(int index, Vector2 startPos)
        {
            Monster = Enum.GetName(typeof(EMonster), index); //gets the string value of the enum with the index, index
            sprite = GameWorld.ContentManager.Load<Texture2D>($"monsters/{Monster}");

            //scaling for the sprites. If the enemy is a dragon, the scaling is larger
            if (index >= 21)
            {
                scale = 0.75f;
                dragon = true;
            }
            else
            {
                scale = 0.25f;
            }

            movementSpeed = 10;
            Position = startPos;

            //if the enemy is a dragon, sets their level to 25, else their level is based on their index +/- 1
            if (dragon)
            {
                Level = 25;
            }
            else if (GameWorld.Instance.currentZone == "DragonRealm")
            {
                Level = 20;
            }
            else
            {
                Level = index + GameWorld.Instance.RandomInt(-1, 2);
            }
            
            if (Level <= 0)
            {
                Level = 1;
            }
            
            #region base stats
            Defense = (int)(5 * ((Level + GameWorld.Instance.RandomInt(2, 4)) * 0.2f));
            Damage = (int)(5 * ((Level + GameWorld.Instance.RandomInt(2, 4)) * 0.6f));
            maxHealth = (int)(5 * ((Level + GameWorld.Instance.RandomInt(2, 5)) * 2.5f));
            attackSpeed = (3 * Level + GameWorld.Instance.RandomInt(5, 7));
            EarthResistance = (float)Math.Log(10 * (Level * 0.3f) + GameWorld.Instance.RandomInt(2, 4));
            WaterResistance = (float)Math.Log(10 * (Level * 0.3f) + GameWorld.Instance.RandomInt(2, 4));
            DarkResistance = (float)Math.Log(10 * (Level * 0.3f) + GameWorld.Instance.RandomInt(2, 4));
            MetalResistance = (float)Math.Log(10 * (Level * 0.3f) + GameWorld.Instance.RandomInt(2, 4));
            FireResistance = (float)Math.Log(10 * (Level * 0.3f) + GameWorld.Instance.RandomInt(2, 4));
            AirResistance = (float)Math.Log(10 * (Level * 0.3f) + GameWorld.Instance.RandomInt(2, 4));
            #endregion

            if (dragon)
            {
                //Damage *= 1;
                maxHealth *= 4;
            }

            //switch case to determine special properties based on the monster's element (logistic function)
            switch (Monster)
            {
                case "bear":
                case "sheep":
                case "wolf":
                    Defense = (int)(Defense * (Level * 0.5f));
                    Damage = (int)(Damage * 1.1);
                    break;
                case "plantEater":
                case "insectSoldier":
                case "slimeSnake":
                    EarthResistance *= (float)(20 / (1 + Math.Pow(Math.E, -(Level * 0.5f))));
                    DarkResistance = (float)(DarkResistance * (-20 / (1 + Math.Pow(Math.E, -(Level * 0.5f)))) + Level * 0.5f);
                    earthDamage = (int)(damage * 0.8f);
                    damage = (int)(damage * 0.2f);
                    break;
                case "tentacle":
                case "frog":
                case "fish":
                    WaterResistance *= (float)(20 / (1 + Math.Pow(Math.E, -(Level * 0.5f))));
                    AirResistance = (float)(AirResistance * (-20 / (1 + Math.Pow(Math.E, -(Level * 0.5f)))) + Level * 0.5f);
                    waterDamage = (int)(damage * 0.8f);
                    damage = (int)(damage * 0.2f);
                    break;
                case "mummy":
                case "vampire":
                case "banshee":
                    DarkResistance *= (float)(20 / (1 + Math.Pow(Math.E, -(Level * 0.5f))));
                    MetalResistance = (float)(MetalResistance * (-20 / (1 + Math.Pow(Math.E, -(Level * 0.5f)))) + Level * 0.5f);
                    darkDamage = (int)(damage * 0.8f);
                    damage = (int)(damage * 0.2f);
                    break;
                case "bucketMan":
                case "defender":
                case "sentry":
                    MetalResistance *= (float)(20 / (1 + Math.Pow(Math.E, -(Level * 0.5f))));
                    FireResistance = (float)(FireResistance * (-20 / (1 + Math.Pow(Math.E, -(Level * 0.5f)))) + Level * 0.5f);
                    metalDamage = (int)(damage * 0.8f);
                    damage = (int)(damage * 0.2f);
                    break;
                case "fireGolem":
                case "infernalDemon":
                case "ashZombie":
                    FireResistance *= (float)(20 / (1 + Math.Pow(Math.E, -(Level * 0.5f))));
                    WaterResistance = (float)(WaterResistance * (-20 / (1 + Math.Pow(Math.E, -(Level * 0.5f)))) + Level * 0.5f);
                    fireDamage = (int)(damage * 0.8f);
                    damage = (int)(damage * 0.2f);
                    break;
                case "falcon":
                case "bat":
                case "raven":
                    AirResistance *= (float)(20 / (1 + Math.Pow(Math.E, -(Level * 0.5f))));
                    EarthResistance = (float)(EarthResistance * (-20 / (1 + Math.Pow(Math.E, -(Level * 0.5f)))) + Level * 0.5f);
                    airDamage = (int)(damage * 0.8f);
                    damage = (int)(damage * 0.2f);
                    break;
                    //dragons
                case "neutralDragon":
                    Defense = (int)(Defense * 1.25f);
                    damage = (int)Math.Round(damage * 0.3);
                    break;
                case "earthDragon":
                    EarthResistance *= (float)(20 / (1 + Math.Pow(Math.E, -Level)));
                    DarkResistance = (float)(DarkResistance * (-20 / (1 + Math.Pow(Math.E, -Level))));
                    earthDamage = damage;
                    damage = (int)(damage * 0.15f);
                    break;
                case "waterDragon":
                    WaterResistance *= (float)(20 / (1 + Math.Pow(Math.E, -Level)));
                    AirResistance = (float)(AirResistance * (-20 / (1 + Math.Pow(Math.E, -Level))));
                    waterDamage = damage;
                    damage = (int)(damage * 0.15f);
                    break;
                case "darkDragon":
                    DarkResistance *= (float)(20 / (1 + Math.Pow(Math.E, -Level)));
                    MetalResistance = (float)(MetalResistance * (-20 / (1 + Math.Pow(Math.E, -Level))));
                    darkDamage = damage;
                    damage = (int)(damage * 0.15f);
                    break;
                case "metalDragon":
                    MetalResistance *= (float)(20 / (1 + Math.Pow(Math.E, -Level)));
                    FireResistance = (float)(FireResistance * (-20 / (1 + Math.Pow(Math.E, -Level))));
                    metalDamage = damage;
                    damage = (int)(damage * 0.15f);
                    break;
                case "fireDragon":
                    FireResistance *= (float)(20 / (1 + Math.Pow(Math.E, -Level)));
                    WaterResistance = (float)(WaterResistance * (-20 / (1 + Math.Pow(Math.E, -Level))));
                    fireDamage = damage;
                    damage = (int)(damage * 0.15f);
                    break;
                case "airDragon":
                    AirResistance *= (float)(20 / (1 + Math.Pow(Math.E, -Level)));
                    EarthResistance = (float)(EarthResistance * (-20 / (1 + Math.Pow(Math.E, -Level))));
                    airDamage = damage;
                    damage = (int)(damage * 0.15f);
                    break;
            }

            //adds damage and resistances to lists for ease of use
            #region resistances
            ResistanceTypes.Add(EarthResistance);
            ResistanceTypes.Add(WaterResistance);
            ResistanceTypes.Add(DarkResistance);
            ResistanceTypes.Add(MetalResistance);
            ResistanceTypes.Add(FireResistance);
            ResistanceTypes.Add(AirResistance);
            DamageTypes.Add(earthDamage);
            DamageTypes.Add(waterDamage);
            DamageTypes.Add(darkDamage);
            DamageTypes.Add(metalDamage);
            DamageTypes.Add(fireDamage);
            DamageTypes.Add(airDamage);
            #endregion

            //gives the enemy a FilledStone with its effects
            if (dragon)
            {
                DragonStone = new FilledStone(this);
                if (DragonStone.DragonWeaponEffect1.StatBuff)
                {
                    new Effect(DragonStone.DragonWeaponEffect1.Index, "Weapon", DragonStone, this, 0);
                }
                else if (DragonStone.DragonWeaponEffect2.StatBuff)
                {
                    new Effect(DragonStone.DragonWeaponEffect2.Index, "Weapon", DragonStone, this, 0);
                }
                else if (DragonStone.DragonWeaponEffect3.StatBuff)
                {
                    new Effect(DragonStone.DragonWeaponEffect3.Index, "Weapon", DragonStone, this, 0);
                }

                if (DragonStone.DragonArmorEffect1.StatBuff)
                {
                    new Effect(DragonStone.DragonArmorEffect1.Index, "Weapon", DragonStone, this, 0);
                }
                else if (DragonStone.DragonArmorEffect2.StatBuff)
                {
                    new Effect(DragonStone.DragonArmorEffect2.Index, "Weapon", DragonStone, this, 0);
                }
                else if (DragonStone.DragonArmorEffect3.StatBuff)
                {
                    new Effect(DragonStone.DragonArmorEffect3.Index, "Weapon", DragonStone, this, 0);
                }
            }
            else
            {
                EnemyStone = new FilledStone(this);

                if (EnemyStone.WeaponEffect.StatBuff)
                {
                    new Effect(index, "Weapon", EnemyStone, this, 0);
                }

                if (EnemyStone.ArmorEffect.StatBuff)
                {
                    new Effect(index, "Armor", EnemyStone, this, 0);
                }
            }

            currentHealth = maxHealth;

            thread = new Thread(() => Update())
            {
                IsBackground = true
            };
            thread.Start();
        }

        /// <summary>
        /// Contructor used to load the database
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
            Monster = monster;
            sprite = GameWorld.ContentManager.Load<Texture2D>($"monsters/{monster}");

            if (ReturnMonsterIndex(Monster) >= 21)
            {
                scale = 0.75f;
                Dragon = true;
            }
            else
            {
                scale = 0.25f;
            }

            movementSpeed = 10;
            Position = startPos;

            Level = level;
            
            //base stats
            #region
            Defense = defense;
            Damage = damage;
            this.maxHealth = maxHealth;
            currentHealth = 0 + maxHealth;
            this.attackSpeed = attackSpeed;
            this.MetalResistance = metalResistance;
            this.EarthResistance = earthResistance;
            this.AirResistance = airResistance;
            this.FireResistance = fireResistance;
            this.DarkResistance = darkResistance;
            this.WaterResistance = waterResistance;
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
                    //dragons
                case "neutralDragon":
                    Defense = (int)(Defense * 1.25f);
                    break;
                case "earthDragon":
                    EarthResistance *= (float)(20 / (1 + Math.Pow(Math.E, -Level)));
                    DarkResistance = (float)(DarkResistance * (-20 / (1 + Math.Pow(Math.E, -Level))));
                    earthDamage = damage;
                    damage = (int)(damage * 0.25f);
                    break;
                case "waterDragon":
                    WaterResistance *= (float)(20 / (1 + Math.Pow(Math.E, -Level)));
                    AirResistance = (float)(AirResistance * (-20 / (1 + Math.Pow(Math.E, -Level))));
                    waterDamage = damage;
                    damage = (int)(damage * 0.25f);
                    break;
                case "darkDragon":
                    DarkResistance *= (float)(20 / (1 + Math.Pow(Math.E, -Level)));
                    MetalResistance = (float)(MetalResistance * (-20 / (1 + Math.Pow(Math.E, -Level))));
                    darkDamage = damage;
                    damage = (int)(damage * 0.25f);
                    break;
                case "metalDragon":
                    MetalResistance *= (float)(20 / (1 + Math.Pow(Math.E, -Level)));
                    FireResistance = (float)(FireResistance * (-20 / (1 + Math.Pow(Math.E, -Level))));
                    metalDamage = damage;
                    damage = (int)(damage * 0.25f);
                    break;
                case "fireDragon":
                    FireResistance *= (float)(20 / (1 + Math.Pow(Math.E, -Level)));
                    WaterResistance = (float)(WaterResistance * (-20 / (1 + Math.Pow(Math.E, -Level))));
                    fireDamage = damage;
                    damage = (int)(damage * 0.25f);
                    break;
                case "airDragon":
                    AirResistance *= (float)(20 / (1 + Math.Pow(Math.E, -Level)));
                    EarthResistance = (float)(EarthResistance * (-20 / (1 + Math.Pow(Math.E, -Level))));
                    airDamage = damage;
                    damage = (int)(damage * 0.25f);
                    break;
            }
            
            //adds damage and resistances to lists for ease of use
            #region
            ResistanceTypes.Add(this.EarthResistance);
            ResistanceTypes.Add(this.WaterResistance);
            ResistanceTypes.Add(this.DarkResistance);
            ResistanceTypes.Add(this.MetalResistance);
            ResistanceTypes.Add(this.FireResistance);
            ResistanceTypes.Add(this.AirResistance);
            DamageTypes.Add(earthDamage);
            DamageTypes.Add(waterDamage);
            DamageTypes.Add(darkDamage);
            DamageTypes.Add(metalDamage);
            DamageTypes.Add(fireDamage);
            DamageTypes.Add(airDamage);
            #endregion

            //gives the enemy a FilledStone with its effects
            if (dragon)
            {
                DragonStone = new FilledStone(this);
                if (DragonStone.DragonWeaponEffect1.StatBuff)
                {
                    new Effect(DragonStone.DragonWeaponEffect1.Index, "Weapon", DragonStone, this, 0);
                }
                else if (DragonStone.DragonWeaponEffect2.StatBuff)
                {
                    new Effect(DragonStone.DragonWeaponEffect2.Index, "Weapon", DragonStone, this, 0);
                }
                else if (DragonStone.DragonWeaponEffect3.StatBuff)
                {
                    new Effect(DragonStone.DragonWeaponEffect3.Index, "Weapon", DragonStone, this, 0);
                }

                if (DragonStone.DragonArmorEffect1.StatBuff)
                {
                    new Effect(DragonStone.DragonArmorEffect1.Index, "Weapon", DragonStone, this, 0);
                }
                else if (DragonStone.DragonArmorEffect2.StatBuff)
                {
                    new Effect(DragonStone.DragonArmorEffect2.Index, "Weapon", DragonStone, this, 0);
                }
                else if (DragonStone.DragonArmorEffect3.StatBuff)
                {
                    new Effect(DragonStone.DragonArmorEffect3.Index, "Weapon", DragonStone, this, 0);
                }
            }
            else
            {
                EnemyStone = new FilledStone(this);

                if (EnemyStone.WeaponEffect.StatBuff)
                {
                    new Effect(ReturnMonsterIndex(Monster), "Weapon", EnemyStone, this, 0);
                }

                if (EnemyStone.ArmorEffect.StatBuff)
                {
                    new Effect(ReturnMonsterIndex(Monster), "Armor", EnemyStone, this, 0);
                }
            }

            thread = new Thread(() => Update())
            {
                IsBackground = true
            };
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

                    // because the thread can still be alive when the CurrenZone().Enemies list is being overridden
                    // else you get the "collection changed" exeption
                    // This can happen when changing zones
                    try
                    {
                        //enemy collision with enemies
                        foreach (Enemy enemy in GameWorld.Instance.CurrentZone().Enemies)
                        {
                            if (CollisionBox.Intersects(enemy.CollisionBox) && enemy != this)
                            {
                                Position -= direction;
                            }
                        }
                    }
                    catch (Exception)
                    {

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
