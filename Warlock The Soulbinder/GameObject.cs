using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    public class GameObject
    {
        protected Texture2D sprite;
        private Vector2 position;

        public virtual Rectangle CollisionBox
        {
            get
            {
                return new Rectangle((int)(Position.X - sprite.Width * 0.25), (int)(Position.Y - sprite.Height * 0.25), (int)(sprite.Width * 0.5), (int)(sprite.Height * 0.5));
            }
        }

        public Vector2 Position { get => position; set => position = value; }

        public GameObject()
        {

        }

        public GameObject(Vector2 position, string spriteName)
        {

        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
