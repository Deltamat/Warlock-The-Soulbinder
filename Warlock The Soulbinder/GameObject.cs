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
    /// <summary>
    /// Obbject that needs to be updated and drawn
    /// </summary>
    public class GameObject
    {
        protected Random rng = new Random();
        protected Texture2D sprite;
        protected Vector2 position;
        protected string spriteName;
        private string stringText;
        private Vector2 stringPosition;
        private Color stringColor;

        /// <summary>
        /// Creates a CollissionBox
        /// </summary>
        public virtual Rectangle CollisionBox
        {
            get
            {
                return new Rectangle((int)(Position.X), (int)(Position.Y), (int)(Sprite.Width), (int)(Sprite.Height));
            }
        }

        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public Vector2 Position { get => position; set => position = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public Texture2D Sprite { get => sprite; set => sprite = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public string StringText { get => stringText; set => stringText = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public Vector2 StringPosition { get => stringPosition; set => stringPosition = value; }
        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public Color StringColor { get => stringColor; set => stringColor = value; }

        /// <summary>
        /// For the database
        /// </summary>
        public GameObject()
        {

        }

        /// <summary>
        /// For scrolling strings
        /// </summary>
        /// <param name="position"></param>
        /// <param name="Text"></param>
        /// <param name="color"></param>
        public GameObject(Vector2 position, string Text, Color color)
        {
            stringPosition = position;
            stringText = Text;
            stringColor = color;

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

        /// <summary>
        /// Empty
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
        }

        /// <summary>
        /// Draw method with the base color of whatever is being drawn
        /// </summary>
        /// <param name="spriteBatch"> spritebatch </param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, Color.White);
        }


        /// <summary>
        /// Draw method where the color of the object can be specified
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="color"> color of the object </param>
        public virtual void Draw(SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(Sprite, Position, color);
        }
    }
}