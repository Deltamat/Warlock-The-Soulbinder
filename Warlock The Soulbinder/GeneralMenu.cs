using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Warlock_The_Soulbinder
{
    class GeneralMenu : Menu
    {
        static GeneralMenu instance;
        private Texture2D book;

        public static GeneralMenu Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GeneralMenu();
                }
                return instance;
            }
        }
        private GeneralMenu()
        {

        }

    
        public void LoadContent(ContentManager content)
        {
            book = content.Load<Texture2D>("Book");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(book, Vector2.Zero, Color.White);
        }
    }
}
