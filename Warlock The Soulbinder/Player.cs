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
            sprite = GameWorld.ContentManager.Load<Texture2D>("keylimepie");
            movementSpeed = 1000;
            damage = 1;
            attackSpeed = 1f;
            health = 100;
        }

        public Player(int index) : base(index)
        {
        }

        public void Move(Vector2 directionInput)
        {
            direction += directionInput; //adds direction vector input to local direction input. This allows direction to take in multiple direction vectors

            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }
        }

        public override void Combat()
        {
        
        }

        public override void Update(GameTime gameTime)
        {
            if (!isInCombat)
            {
                InputHandler.Instance.Execute(this); //gets keys pressed
                direction *= movementSpeed * (float)GameWorld.deltaTime; //adds movement speed to direction keeping in time with deltaTime
                Position += direction; //moves the player based on direction


                foreach (var item in GameWorld.collisionTest) // After the player have moved check if collision has happen. if true move backwards the same direction
                {
                    if (CollisionBox.Intersects(item))
                    {
                        Position -= direction;
                    }
                }
                direction = Vector2.Zero; //resets direction
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
