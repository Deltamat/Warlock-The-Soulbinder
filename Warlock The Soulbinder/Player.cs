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

        public Player()
        {
            Sprite = GameWorld.ContentManager.Load<Texture2D>("tempPlayer");
            movementSpeed = 1000;
            Damage = 1;
            AttackSpeed = 1f;
            MaxHealth = 100;
            CurrentHealth = 100;
        }

        public void Move(Vector2 directionInput)
        {
            direction += directionInput; //adds direction vector input to local direction input. This allows direction to take in multiple direction vectors

            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }
        }

        //public override void Combat()
        //{

        //}

        public override void Update(GameTime gameTime)
        {
            if (!IsInCombat)
            {
                InputHandler.Instance.Execute(this); //gets keys pressed
                direction *= movementSpeed * (float)GameWorld.deltaTime; //adds movement speed to direction keeping in time with deltaTime
                Position += direction; //moves the player based on direction
                if (direction != Vector2.Zero) // So it does not check for collision if not moving
                {
                    foreach (var item in GameWorld.Instance.CurrentZone().CollisionRects) // After the player have moved check if collision has happen. if true move backwards the same direction
                    {
                        if (CollisionBox.Intersects(item))
                        {
                            Position -= direction;
                        }
                    }
                    direction = Vector2.Zero; //resets direction

                    foreach (var enemy in GameWorld.Instance.enemies) // if collide with enemy, go to combat
                    {
                        if (enemy.CollisionBox.Intersects(CollisionBox))
                        {
                            GameWorld.Instance.GameState = "Combat";
                            Combat.Instance.SelectEnemy(enemy);
                            break;
                        }
                    }
                }

            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
