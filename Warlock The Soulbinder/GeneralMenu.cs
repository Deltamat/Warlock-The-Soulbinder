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
        private List<String> menuList = new List<string>();
        private Texture2D arrow;
        private float delay = 0;

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
            arrow = content.Load<Texture2D>("Arrow");

        }

        public override void Update(GameTime gameTime)
        {
            delay += gameTime.ElapsedGameTime.Milliseconds;

            if (Keyboard.GetState().IsKeyDown(Keys.Up) && delay > 100 && SelectedInt > 0)
            {
                SelectedInt--;
                delay = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down) && delay > 100 && SelectedInt < 6)
            {
                SelectedInt++;
                delay = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(book, new Vector2(50,20), Color.White);
            spriteBatch.DrawString(Combat.Instance.CombatFont, "Character", new Vector2(200, 120), Color.White);
            spriteBatch.DrawString(Combat.Instance.CombatFont, "Equipment", new Vector2(200, 200), Color.White);
            spriteBatch.DrawString(Combat.Instance.CombatFont, "Inventory", new Vector2(200, 280), Color.White);
            spriteBatch.DrawString(Combat.Instance.CombatFont, "Log", new Vector2(200, 360), Color.White);
            spriteBatch.DrawString(Combat.Instance.CombatFont, "Save", new Vector2(200, 440), Color.White);
            spriteBatch.DrawString(Combat.Instance.CombatFont, "Options", new Vector2(200, 520), Color.White);
            spriteBatch.DrawString(Combat.Instance.CombatFont, "Quit", new Vector2(200, 600), Color.White);

            spriteBatch.Draw(arrow, new Vector2(155, 120 + 80 * selectedInt), Color.White);

        }
    }
}
