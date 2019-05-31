using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Warlock_The_Soulbinder
{
    class MainMenu : Menu
    {
        private Texture2D background;
        private Texture2D arrow;
        private Button loadGameButton;
        private Button newGameButton;
        private Button loadSlot1;
        private Button loadSlot2;
        private Button loadSlot3;
        List<Button> loadSlots = new List<Button>();
        string mainMenuState = "Main";
        


        static MainMenu instance;
        public static MainMenu Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainMenu();
                }
                return instance;
            }
        }

        private MainMenu()
        {
            background = GameWorld.ContentManager.Load<Texture2D>("mainMenuBackground");
            arrow = GameWorld.ContentManager.Load<Texture2D>("Arrow");
            loadGameButton = new Button(GameWorld.ContentManager.Load<Texture2D>("buttons/emptyButton"), GameWorld.Instance.copperFont, new Vector2(1), GameWorld.ContentManager);
            loadGameButton.TextForButton = "Load";
            newGameButton = new Button(GameWorld.ContentManager.Load<Texture2D>("buttons/emptyButton"), GameWorld.Instance.copperFont, new Vector2(500), GameWorld.ContentManager);
            newGameButton.TextForButton = "New game";

            loadGameButton.Click += LoadGame;
            newGameButton.Click += NewGame;

            loadSlot1 = new Button(GameWorld.ContentManager.Load<Texture2D>("buttons/emptyButton"), GameWorld.Instance.copperFont, new Vector2(500), GameWorld.ContentManager);
            loadSlot1.TextForButton = "Save slot 1";
            loadSlot2 = new Button(GameWorld.ContentManager.Load<Texture2D>("buttons/emptyButton"), GameWorld.Instance.copperFont, new Vector2(700), GameWorld.ContentManager);
            loadSlot2.TextForButton = "Save slot 2";
            loadSlot3 = new Button(GameWorld.ContentManager.Load<Texture2D>("buttons/emptyButton"), GameWorld.Instance.copperFont, new Vector2(900), GameWorld.ContentManager);
            loadSlot3.TextForButton = "Save slot 3";
            loadSlot1.Click += SaveSlot1;
            loadSlot2.Click += SaveSlot2;
            loadSlot3.Click += SaveSlot3;
            loadSlots.Add(loadSlot1);
            loadSlots.Add(loadSlot2);
            loadSlots.Add(loadSlot3);
        }

        public override void Update(GameTime gameTime)
        {
            if (mainMenuState == "Main")
            {
                newGameButton.Update(gameTime);
                loadGameButton.Update(gameTime);
            }
            else 
            {
                foreach (var loadSlot in loadSlots)
                {
                    loadSlot.Update(gameTime);
                }
            }


        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.DrawString(GameWorld.Instance.copperFont, "Warlock: The Soulbinder", new Vector2(GameWorld.Instance.ScreenSize.Width * 0.5f - 300 , 0), Color.Red);

            if (mainMenuState == "Main")
            {
                loadGameButton.Draw(spriteBatch);
                newGameButton.Draw(spriteBatch);
            }
            else
            {
                foreach (var loadSlot in loadSlots)
                {
                    loadSlot.Draw(spriteBatch);
                }
            }
        }

        private void NewGame(object sender, EventArgs e)
        {
            mainMenuState = "New Game";
        }

        private void LoadGame(object sender, EventArgs e)
        {
            mainMenuState = "Load Game";
        }

        private void SaveSlot1(object sender, EventArgs e)
        {
            GameWorld.Instance.CurrentSaveFile = "1";

            if (mainMenuState == "New Game")
            {
                GameWorld.Instance.GameState = "Overworld";
            }
            else
            {
                GameWorld.Instance.LoadDB();
                GameWorld.Instance.GameState = "Overworld";
            }
        }
        private void SaveSlot2(object sender, EventArgs e)
        {
            GameWorld.Instance.CurrentSaveFile = "2";

            if (mainMenuState == "New Game")
            {
                GameWorld.Instance.GameState = "Overworld";
            }
            else
            {
                GameWorld.Instance.LoadDB();
                GameWorld.Instance.GameState = "Overworld";
            }
        }
        private void SaveSlot3(object sender, EventArgs e)
        {
            GameWorld.Instance.CurrentSaveFile = "3";

            if (mainMenuState == "New Game")
            {
                GameWorld.Instance.GameState = "Overworld";
            }
            else
            {
                GameWorld.Instance.LoadDB();
                GameWorld.Instance.GameState = "Overworld";
            }
        }
    }
}

