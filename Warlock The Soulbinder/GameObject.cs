using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        protected Random rng = new Random();
        protected Texture2D sprite;
        protected string spriteName;
        private Vector2 position;


        public virtual Rectangle CollisionBox
        {
            get
            {
                return new Rectangle((int)(Position.X), (int)(Position.Y), (int)(Sprite.Width), (int)(Sprite.Height));
            }
        }

        public Vector2 Position { get => position; set => position = value; }
        public Texture2D Sprite { get => sprite; set => sprite = value; }

        /// <summary>
        /// For the database
        /// </summary>
        public GameObject()
        {

        }

        /// <summary>
        /// For the game
        /// </summary>
        /// <param name="position"></param>
        /// <param name="SpriteName"></param>
        /// <param name="content"></param>
        public GameObject(Vector2 position, string SpriteName, ContentManager content)
        {

            Position = position;
            spriteName = SpriteName;
            Sprite = content.Load<Texture2D>(SpriteName);
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, Color.White);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(Sprite, Position, color);
        }
    }
}
