using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    public class Player : CharacterCombat
    {
        private double gracePeriod = 5;
        private bool graceStart = true;
        private int graceSwitch = 0;
        private Vector2 lastDirection = new Vector2(0, 1);
        private double aniIndex;
        private double elapsedTime;
        private const int animationFPS = 30;
        private bool walking;
        private bool attacking;
        private bool attackStart;
        private bool hurt;
        private bool hurtStart;
        private bool blinking;
        
        static Player instance;
        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Player();
                }
                return instance;
            }
        }

        public override int CurrentHealth
        {
            get
            {
                return currentHealth;
            }
            set
            {
                if (GameWorld.Instance.GameState == "Combat")
                {
                    if (value < base.CurrentHealth)
                    {
                        HurtStart = true; //starts player's hurt animation
                        Combat.Instance.PlayerScrolling($"HP -{base.CurrentHealth - value}", Color.Red);
                    }
                    else if (value > base.CurrentHealth)
                    {
                        Combat.Instance.PlayerScrolling($"HP +{value - base.CurrentHealth}", Color.Green);
                    }
                }
                
                currentHealth = value;
                if (currentHealth <= 0) // if the player is dead, teleport back to the town
                {
                    Sound.PlaySound("sound/battleDefeat");
                    Position = new Vector2(1800, 2630);
                    GameWorld.Instance.CurrentZone().KillEnemiesInZone();
                    GameWorld.Instance.currentZone = "Town";
                    GameWorld.Instance.GameState = "Overworld";
                    GameWorld.Instance.SongPosition = TimeSpan.Zero;
                    GameWorld.Instance.ChangeMusic();
                    currentHealth = maxHealth;
                    Combat.Instance.ExitCombat();
                }
                else if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }
            }
        }

        public double GracePeriod { get => gracePeriod; set => gracePeriod = value; }
        public bool GraceStart { get => graceStart; set => graceStart = value; }
        public bool AttackStart { get => attackStart; set => attackStart = value; }
        public bool HurtStart { get => hurtStart; set => hurtStart = value; }
        public int AniIndex { set => aniIndex = value; }

        /// <summary>
        /// Returns the player's collision box. Modified to better suit this game's player sprite
        /// </summary>
        public override Rectangle CollisionBox
        {
            get
            {
                return new Rectangle((int)(Position.X + Sprite.Width * 0.2 * scale), (int)(Position.Y + Sprite.Height * 0.075 * scale), (int)(Sprite.Width * 0.6 * scale), (int)(Sprite.Height * 0.8 * scale));
            }
        }

        public bool Attacking { get => attacking; set => attacking = value; }
        public bool Hurt { get => hurt; set => hurt = value; }

        public Player()
        {
            Sprite = GameWorld.ContentManager.Load<Texture2D>("Player/Front - Idle/Front - Idle_0");
            scale = 0.25f;
            movementSpeed = 500;
#if DEBUG
            movementSpeed = 1000;
#endif
            Position = new Vector2(1200);

            BaseStats();
            CurrentHealth = MaxHealth;

            //adds damage and resistances to lists for ease of use
            #region
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
        }

        /// <summary>
        /// Sets the player's direction
        /// </summary>
        /// <param name="directionInput">The direction the player moves</param>
        public void Move(Vector2 directionInput)
        {
            GraceStart = true;

            direction += directionInput; //adds direction vector input to local direction input. This allows direction to take in multiple direction vectors

            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (graceStart && GameWorld.Instance.GameState == "Overworld")
            {
                gracePeriod += GameWorld.deltaTimeSecond;
            }            

            if (direction != Vector2.Zero)
            {
                lastDirection = direction;
                walking = true;
            }
            else
            {
                walking = false;
            }

            direction *= movementSpeed * (float)GameWorld.deltaTimeSecond; //adds movement speed to direction keeping in time with deltaTime

            //movement region
            #region
            //handles X direction
            position.X += direction.X; //moves the player based on direction
            if (direction.X != 0) // So it does not check for collision if not moving
            {
                foreach (Rectangle rectangle in GameWorld.Instance.CurrentZone().CollisionRects) // After the player have moved check if collision has happen. if true move backwards the same direction
                {
                    if (CollisionBox.Intersects(rectangle))
                    {
                        position.X -= direction.X;
                    }
                }
                direction.X = 0; //resets direction   
            }

            //handles Y direction
            position.Y += direction.Y; //moves the player based on direction
            if (direction.Y != 0) // So it does not check for collision if not moving
            {
                foreach (Rectangle rectangle in GameWorld.Instance.CurrentZone().CollisionRects) // After the player have moved check if collision has happen. if true move backwards the same direction
                {
                    if (CollisionBox.Intersects(rectangle))
                    {
                        position.Y -= direction.Y;
                    }
                }
                direction.Y = 0; //resets direction   
            }
            #endregion

            //if the player's 3 second grace period is over and the player collides with an enemy, start combat
            if (gracePeriod > 3) 
            {
                foreach (Enemy enemy in GameWorld.Instance.CurrentZone().Enemies)
                {
                    if (enemy.CollisionBox.Intersects(CollisionBox) && GameWorld.Instance.GameState == "Overworld")
                    {
                        GameWorld.Instance.GameState = "Combat";
                        Combat.Instance.SelectEnemy(enemy);
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            ChooseAnimationFrame();
            
            if (gracePeriod < 3 && graceSwitch < 15)
            {
                spriteBatch.Draw(sprite, Position, null, Color.FromNonPremultiplied(255, 255, 255, 175), 0f, Vector2.Zero, scale, new SpriteEffects(), 0.1f);
            }
            else
            {
                spriteBatch.Draw(sprite, Position, null, Color.White, 0f, Vector2.Zero, scale, new SpriteEffects(), 0.8f);
            }

            graceSwitch++;
            if (graceSwitch > 30)
            {
                graceSwitch = 0;
            }
        }

        /// <summary>
        /// Changes sprite based on current animation needed
        /// </summary>
        public void ChooseAnimationFrame()
        {
            if (GameWorld.Instance.GameState == "Overworld" || GameWorld.Instance.GameState == "Dialogue")
            {
                if (walking == true && aniIndex > 17)
                {
                    aniIndex = 0;
                    elapsedTime = 0;
                }
                else if (walking == false && aniIndex > 11)
                {
                    aniIndex = 0;
                    elapsedTime = 0;
                }

                if (lastDirection.X < 0) //walks left
                {
                    if (walking == true)
                    {
                        sprite = GameWorld.ContentManager.Load<Texture2D>($"Player/Left - Walking/Left - Walking_{aniIndex}");
                    }
                    else
                    {
                        sprite = GameWorld.ContentManager.Load<Texture2D>($"Player/Left - Idle/Left - Idle_{aniIndex}");
                    }
                }
                else if (lastDirection.X > 0) //walks right
                {
                    if (walking == true)
                    {
                        sprite = GameWorld.ContentManager.Load<Texture2D>($"Player/Right - Walking/Right - Walking_{aniIndex}");
                    }
                    else
                    {
                        sprite = GameWorld.ContentManager.Load<Texture2D>($"Player/Right - Idle/Right - Idle_{aniIndex}");
                    }
                }
                else if (lastDirection.Y < 0) //walks up
                {
                    if (walking == true)
                    { 
                        sprite = GameWorld.ContentManager.Load<Texture2D>($"Player/Back - Walking/Back - Walking_{aniIndex}");
                    }
                    else
                    {
                        sprite = GameWorld.ContentManager.Load<Texture2D>($"Player/Back - Idle/Back - Idle_{aniIndex}");
                    }
                }
                else if (lastDirection.Y > 0) //walks down
                {
                    if (walking == true)
                    {
                        sprite = GameWorld.ContentManager.Load<Texture2D>($"Player/Front - Walking/Front - Walking_{aniIndex}");
                    }
                    else
                    {
                        sprite = GameWorld.ContentManager.Load<Texture2D>($"Player/Front - Idle/Front - Idle_{aniIndex}");
                    }
                }
            }
            else if (GameWorld.Instance.GameState == "Combat")
            {
                if (aniIndex > 11)
                {
                    aniIndex = 0;
                    elapsedTime = 0;
                }

                if (AttackStart == true && aniIndex == 0) //once aniIndex is 0 and the player is attacking, starts the attack animation
                {
                    Attacking = true;
                    attackStart = false;
                }
                else if (HurtStart == true && aniIndex == 0) //once aniIndex is 0 and the player is hurt, starts the hurt animation
                {
                    Hurt = true;
                    hurtStart = false;
                }
                else if (GameWorld.Instance.RandomInt(0, 10) == 0 && aniIndex == 0) //once aniIndex is 0, gives the player a 10% chace to start the blinking animation
                {
                    blinking = true;
                }

                if (Attacking) //player attacks
                {
                    sprite = GameWorld.ContentManager.Load<Texture2D>($"Player/Right - Slashing/Right - Slashing_{aniIndex}");
                    if (aniIndex == 11)
                    {
                        Attacking = false;
                    }
                }
                else if (Hurt) //player takes damage
                {
                    sprite = GameWorld.ContentManager.Load<Texture2D>($"Player/Right - Hurt/Right - Hurt_{aniIndex}");
                    if (aniIndex == 11)
                    {
                        Hurt = false;
                    }
                }
                else if (blinking) //player blinks
                {
                    sprite = GameWorld.ContentManager.Load<Texture2D>($"Player/Right - Idle Blinking/Right - Idle Blinking_{aniIndex}");
                    if (aniIndex == 11)
                    {
                        blinking = false;
                    }
                }
                else //player is idle
                {
                    sprite = GameWorld.ContentManager.Load<Texture2D>($"Player/Right - Idle/Right - Idle_{aniIndex}");
                }
            }

            aniIndex = (int)(elapsedTime * animationFPS);
            elapsedTime += GameWorld.deltaTimeSecond;
        }

        /// <summary>
        /// Updates all stats according to items equipped
        /// </summary>
        public void UpdateStats()
        {
            BaseStats();
            //foreach equipped stone, adds the stone's stats to the player's
            foreach (FilledStone stone in Equipment.Instance.EquippedEquipment)
            {
                if (stone != null)
                {
                    Damage += stone.Damage; //adds damage to the player
                    Defense += stone.Defense; //adds defense to the player
                    AttackSpeed += stone.AttackSpeed; //adds attack speed to the player
                    MaxHealth += stone.MaxHealth; //adds max health to the player
                    
                    if (Equipment.Instance.EquippedEquipment[0] != null && stone == Equipment.Instance.EquippedEquipment[0])
                    {
                        for (int i = 0; i < stone.DamageTypes.Count; i++) //adds damage types to the player
                        {
                            DamageTypes[i] += stone.DamageTypes[i];
                            switch (i)
                            {
                                case 0:
                                    earthDamage = DamageTypes[i];
                                    break;
                                case 1:
                                    waterDamage = DamageTypes[i];
                                    break;
                                case 2:
                                    darkDamage = DamageTypes[i];
                                    break;
                                case 3:
                                    metalDamage = DamageTypes[i];
                                    break;
                                case 4:
                                    fireDamage = DamageTypes[i];
                                    break;
                                case 5:
                                    airDamage = DamageTypes[i];
                                    break;
                            }
                        }
                    }

                    if (Equipment.Instance.EquippedEquipment[1] != null && stone == Equipment.Instance.EquippedEquipment[1])
                    {
                        Defense *= 2;
                        for (int i = 0; i < stone.ResistanceTypes.Count; i++) //adds resistance types to the player
                        {
                            ResistanceTypes[i] += stone.ResistanceTypes[i];
                            switch (i)
                            {
                                case 0:
                                    EarthResistance = ResistanceTypes[i];
                                    break;
                                case 1:
                                    WaterResistance = ResistanceTypes[i];
                                    break;
                                case 2:
                                    DarkResistance = ResistanceTypes[i];
                                    break;
                                case 3:
                                    MetalResistance = ResistanceTypes[i];
                                    break;
                                case 4:
                                    FireResistance = ResistanceTypes[i];
                                    break;
                                case 5:
                                    AirResistance = ResistanceTypes[i];
                                    break;
                            }
                        }
                    }

                    //Increases stats for the player if the quipped stone in weapon has StatBuff == true
                    if (Equipment.Instance.EquippedEquipment[0] != null && Equipment.Instance.EquippedEquipment[0].WeaponEffect.StatBuff && Equipment.Instance.EquippedEquipment[0] == stone)
                    {
                        Effect effect = new Effect(Equipment.Instance.EquippedEquipment[0].WeaponEffect.Index, Equipment.Instance.EquippedEquipment[0].WeaponEffect.Type, Equipment.Instance.EquippedEquipment[0].WeaponEffect.Stone, this, 0);
                    }

                    //Increases stats for the player if the quipped stone in armor has StatBuff == true
                    if (Equipment.Instance.EquippedEquipment[1] != null && Equipment.Instance.EquippedEquipment[1].ArmorEffect.StatBuff && Equipment.Instance.EquippedEquipment[1] == stone)
                    {
                        Effect effect = new Effect(Equipment.Instance.EquippedEquipment[1].ArmorEffect.Index, Equipment.Instance.EquippedEquipment[1].ArmorEffect.Type, Equipment.Instance.EquippedEquipment[1].ArmorEffect.Stone, this, 0);
                    }
                }
            }
        }

        /// <summary>
        /// Sets stats to be base value
        /// </summary>
        public void BaseStats()
        {
            Damage = 10;
            Defense = 0;
            AttackSpeed = 10;
            MaxHealth = 100;
#if DEBUG
            int over9000 = 9001;
            Damage += over9000;
            Defense += over9000;
            AttackSpeed += over9000;
            MaxHealth += over9000;
#endif
            for (int i = 0; i < DamageTypes.Count; i++)
            {
                DamageTypes[i] = 0;
            }
            for (int i = 0; i < ResistanceTypes.Count; i++)
            {
                ResistanceTypes[i] = 0;
            }
        }
    }
}
