using Microsoft.Xna.Framework;
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

        public double GracePeriod { get => gracePeriod; set => gracePeriod = value; }
        public bool GraceStart { get => graceStart; set => graceStart = value; }
        public bool AttackStart { get => attackStart; set => attackStart = value; }
        public bool HurtStart { get => hurtStart; set => hurtStart = value; }

        /// <summary>
        /// Returns the player's collision box. Modified to better suit this game's player sprite
        /// </summary>
        public override Rectangle CollisionBox
        {
            get
            {
                return new Rectangle((int)(Position.X + Sprite.Width * 0.2), (int)(Position.Y + Sprite.Height * 0.075), (int)(Sprite.Width * 0.6), (int)(Sprite.Height * 0.8));
            }
        }

        public Player()
        {
            Sprite = GameWorld.ContentManager.Load<Texture2D>("Player/Front - Idle/Front - Idle_0");
            movementSpeed = 250;
            Damage = 5;
            AttackSpeed = 1f;
            MaxHealth = 100;
            CurrentHealth = 100;

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
        }

        /// <summary>
        /// Moves the player for its movementSpeed in the direction of directionInput
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
            if (graceStart)
            {
                gracePeriod += GameWorld.deltaTimeSecond;
            }

            InputHandler.Instance.Execute(this); //gets keys pressed

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
                foreach (Rectangle rectangle in GameWorld.collisionMap) // After the player have moved check if collision has happen. if true move backwards the same direction
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
                foreach (Rectangle rectangle in GameWorld.collisionMap) // After the player have moved check if collision has happen. if true move backwards the same direction
                {
                    if (CollisionBox.Intersects(rectangle))
                    {
                        position.Y -= direction.Y;
                    }
                }
                direction.Y = 0; //resets direction   
            }
            #endregion

            //if the player's 5 second grace period is over and the player collides with an enemy, start combat
            if (gracePeriod > 5) 
            {
                foreach (Enemy enemy in GameWorld.Instance.enemies)
                {
                    if (enemy.CollisionBox.Intersects(CollisionBox))
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

            if (gracePeriod < 5 && graceSwitch < 15)
            {
                spriteBatch.Draw(Sprite, Position, Color.FromNonPremultiplied(255, 255, 255, 175));
            }
            else
            {
                base.Draw(spriteBatch);
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
            if (GameWorld.Instance.GameState == "Overworld")
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
                    attacking = true;
                    attackStart = false;
                }
                else if (HurtStart == true && aniIndex == 0) //once aniIndex is 0 and the player is hurt, starts the hurt animation
                {
                    hurt = true;
                    hurtStart = false;
                }
                else if (GameWorld.Instance.RandomInt(0, 10) == 0 && aniIndex == 0) //once aniIndex is 0, gives the player a 10% chace to start the blinking animation
                {
                    blinking = true;
                }

                if (attacking) //player attacks
                {
                    sprite = GameWorld.ContentManager.Load<Texture2D>($"Player/Right - Slashing/Right - Slashing_{aniIndex}");
                    if (aniIndex == 11)
                    {
                        attacking = false;
                    }
                }
                else if (hurt) //player takes damage
                {
                    sprite = GameWorld.ContentManager.Load<Texture2D>($"Player/Right - Hurt/Right - Hurt_{aniIndex}");
                    if (aniIndex == 11)
                    {
                        hurt = false;
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
    }
}
