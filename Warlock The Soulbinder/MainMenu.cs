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
        private Texture2D emptyButton;
        private Button loadGameButton;
        private Button newGameButton;
        private Button loadSlot1;
        private Button loadSlot2;
        private Button loadSlot3;
        List<Button> loadSlots = new List<Button>();
        List<Button> mainMenuButtons = new List<Button>();
        string mainMenuState = "Main";
        private float delay = 0;
        private float cX;
        private float cX2;
        private int selectedIndex = 0;
        //private float cY;
        


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
            emptyButton = GameWorld.ContentManager.Load<Texture2D>("buttons/emptyButton");
            cX = GameWorld.Instance.ScreenSize.Width * 0.5f - emptyButton.Width * 0.5f;
            loadGameButton = new Button(emptyButton, GameWorld.Instance.copperFont, new Vector2(cX, 400), GameWorld.ContentManager);
            loadGameButton.TextForButton = "Load";
            newGameButton = new Button(emptyButton, GameWorld.Instance.copperFont, new Vector2(cX, 300), GameWorld.ContentManager);
            newGameButton.TextForButton = "New game";
            cX2 = cX - arrow.Width;
            loadGameButton.Click += LoadGame;
            newGameButton.Click += NewGame;

            loadSlot1 = new Button(emptyButton, GameWorld.Instance.copperFont, new Vector2(cX, 500), GameWorld.ContentManager);
            loadSlot1.TextForButton = "Save slot 1";
            loadSlot2 = new Button(emptyButton, GameWorld.Instance.copperFont, new Vector2(cX, 600), GameWorld.ContentManager);
            loadSlot2.TextForButton = "Save slot 2";
            loadSlot3 = new Button(emptyButton, GameWorld.Instance.copperFont, new Vector2(cX, 700), GameWorld.ContentManager);
            loadSlot3.TextForButton = "Save slot 3";
            loadSlot1.Click += SaveSlot1;
            loadSlot2.Click += SaveSlot2;
            loadSlot3.Click += SaveSlot3;
            loadSlots.Add(loadSlot1);
            loadSlots.Add(loadSlot2);
            loadSlots.Add(loadSlot3);
            mainMenuButtons.Add(loadGameButton);
            mainMenuButtons.Add(newGameButton);
        }

        public override void Update(GameTime gameTime)
        {
            delay += gameTime.ElapsedGameTime.Milliseconds;
            
            if (mainMenuState == "Main")
            {
                //for mouse
                foreach (var item in mainMenuButtons)
                {
                    item.Update(gameTime);
                }
                //for keyboard
                ChangeSelectedIndex(1);
                ChangeMainMenuState();
            }
            else 
            {
                //for mouse
                foreach (var loadSlot in loadSlots)
                {
                    loadSlot.Update(gameTime);
                }
                //for keyboard
                ChangeSelectedIndex(2);
                ChooseSaveGame();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            spriteBatch.DrawString(GameWorld.Instance.copperFont, "Warlock: The Soulbinder", new Vector2(GameWorld.Instance.ScreenSize.Width * 0.5f - 300 , 160), Color.Red);

            //Draws a selection arrow to see what you are hovering over
            if (mainMenuState == "Main")
            {
                spriteBatch.Draw(arrow, new Vector2(cX2, 320 + 100 * selectedIndex), Color.White);
            }
            else
            {
                spriteBatch.Draw(arrow, new Vector2(cX2, 520 + 100 * selectedIndex), Color.White);
            }

            if (mainMenuState == "Main")
            {
                foreach (var item in mainMenuButtons)
                {
                    item.Draw(spriteBatch);
                }
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

        public void ChangeMainMenuState()
        {
            if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeySelect) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonSelect)) && delay > 200)
            {
                switch (selectedIndex)
                {
                    case 0:
                        mainMenuState = "New Game";
                        break;
                    case 1:
                        mainMenuState = "Load Game";
                        break;
                }
                delay = 0;
                selectedIndex = 0;
            }
        }
        public void ChooseSaveGame()
        {
            if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeySelect) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonSelect)) && delay > 200)
            {
                switch (selectedIndex)
                {
                    case 0:
                        GameWorld.Instance.CurrentSaveFile = "1";
                        GameWorld.Instance.GameState = "Overworld";
                        break;
                    case 1:
                        GameWorld.Instance.CurrentSaveFile = "2";
                        GameWorld.Instance.GameState = "Overworld";
                        break;
                    case 2:
                        GameWorld.Instance.CurrentSaveFile = "3";
                        GameWorld.Instance.GameState = "Overworld";
                        break;
                }
            }
        }

        private void ChangeSelectedIndex(int maxIndex)
        {
            if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeyUp) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonUp)) && delay > 150 && selectedIndex > 0)
            {
                selectedIndex--;
                delay = 0;
            }

            if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeyDown) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonDown)) && delay > 150 && selectedIndex < maxIndex)
            {
                selectedIndex++;
                delay = 0;
            }
        }
    }
}