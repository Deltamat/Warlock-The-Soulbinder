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
        int graceSwitch = 0;
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

        public Player()
        {
            Sprite = GameWorld.ContentManager.Load<Texture2D>("keylimepie");
            movementSpeed = 250;
            Damage = 1;
            AttackSpeed = 1f;
            MaxHealth = 100;
            CurrentHealth = 100;
        }

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
                gracePeriod += GameWorld.deltaTime;
            }

            InputHandler.Instance.Execute(this); //gets keys pressed
            direction *= movementSpeed * (float)GameWorld.deltaTime; //adds movement speed to direction keeping in time with deltaTime
            Position += direction; //moves the player based on direction

            if (direction != Vector2.Zero) // So it does not check for collision if not moving
            {
                foreach (Rectangle rectangle in GameWorld.collisionMap) // After the player have moved check if collision has happen. if true move backwards the same direction
                {
                    if (CollisionBox.Intersects(rectangle))
                    {
                        Position -= direction;
                    }
                }
                direction = Vector2.Zero; //resets direction   
            }

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
            if (gracePeriod < 5 && graceSwitch < 15)
            {                
                spriteBatch.Draw(Sprite, Position, Color.LightGray);                
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
    }
}
