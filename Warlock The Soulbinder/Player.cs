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
            movementSpeed = 5;
        }

        public Player(int index) : base(index)
        {
        }

        public void Move(Vector2 direction)
        {
            if (direction != Vector2.Zero)
            {
                direction.Normalize();
            }

            direction *= movementSpeed;

            Position += direction;

            foreach (var item in GameWorld.collisionTest)
            {
                if (CollisionBox.Intersects(item))
                {
                    Position -= direction;

                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            InputHandler.Instance.Execute(this);            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, Position, Color.White);
        }
    }
}
