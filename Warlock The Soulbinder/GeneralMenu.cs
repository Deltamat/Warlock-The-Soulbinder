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
        private string inventoryState = "GeneralMenu";
        private float delay = 0;
        private int filledStoneInt = 0;

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

        public int FilledStoneInt { get => filledStoneInt; set => filledStoneInt = value; }

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

            switch (inventoryState)
            {
                case "GeneralMenu":
                    ChangeSelected(6);
                    break;
                case "Inventory":
                    ChangeSelected(1);
                    break;
                case "FilledStones":
                    if (FilledStoneInt < FilledStone.StoneListPages)
                    {
                        ChangeSelected(8);
                    }
                    else
                    {
                        ChangeSelected(FilledStone.StoneList.Count - (FilledStone.StoneListPages * 9) - 1);
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.Right) && delay > 200 && filledStoneInt < FilledStone.StoneListPages)
                    {
                        filledStoneInt++;
                        delay = 0;
                        selectedInt = 0;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.Left) && delay > 200 && filledStoneInt > 0)
                    {
                        filledStoneInt--;
                        delay = 0;
                        selectedInt = 0;
                    }
                    break;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && delay > 100)
            {
                ChangeState();
                delay = 0;
                selectedInt = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Back) && delay > 100)
            {
                inventoryState = "GeneralMenu";
                selectedInt = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(book, new Vector2(50,20), Color.White);

            if (inventoryState == "GeneralMenu")
            { 
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Character", new Vector2(200, 120), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Equipment", new Vector2(200, 200), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Inventory", new Vector2(200, 280), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Log", new Vector2(200, 360), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Save", new Vector2(200, 440), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Options", new Vector2(200, 520), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Quit", new Vector2(200, 600), Color.White);

                switch (selectedInt)
                { 
                    case 2:
                    spriteBatch.DrawString(Combat.Instance.CombatFont, "Consumables", new Vector2(1100, 120), Color.White);
                    spriteBatch.DrawString(Combat.Instance.CombatFont, "Monster Stones", new Vector2(1100, 200), Color.White);
                    break;
                }
            }

            if (inventoryState == "Inventory")
            {
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Consumables", new Vector2(200, 120), Color.White);
                spriteBatch.DrawString(Combat.Instance.CombatFont, "Monster Stones", new Vector2(200, 200), Color.White);

                switch (selectedInt)
                {
                    //Consumables
                    case 0:
                        break;
                    
                    //Filled Stones
                    case 1:
                        for (int i = 0 + FilledStoneInt * 9; i < FilledStone.StoneList.Count; i++)
                        {
                            if ((i + FilledStoneInt * 9 < FilledStoneInt * 9 + 9))
                            {
                                spriteBatch.DrawString(Combat.Instance.CombatFont, FilledStone.StoneList[i].Monster, new Vector2(1100, 120 + i * 80), Color.White);
                                spriteBatch.DrawString(Combat.Instance.CombatFont, FilledStone.StoneList[i].Element, new Vector2(1300, 120 + i * 80), Color.White);
                                spriteBatch.DrawString(Combat.Instance.CombatFont, "lvl" + FilledStone.StoneList[i].Level, new Vector2(1600, 120 + i * 80), Color.White);
                            }
                        }

                        spriteBatch.DrawString(Combat.Instance.CombatFont, (filledStoneInt + 1) + " / " + (FilledStone.StoneListPages + 1), new Vector2(1300, 870), Color.White);
                        break;
                }
            }

            if (inventoryState == "FilledStones")
            {
                if (FilledStoneInt < FilledStone.StoneListPages)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        spriteBatch.DrawString(Combat.Instance.CombatFont, FilledStone.StoneList[i + (filledStoneInt * 9)].Monster, new Vector2(200, 120 + i * 80), Color.White);
                        spriteBatch.DrawString(Combat.Instance.CombatFont, FilledStone.StoneList[i + (filledStoneInt * 9)].Element, new Vector2(400, 120 + i * 80), Color.White);
                        spriteBatch.DrawString(Combat.Instance.CombatFont, "lvl" + FilledStone.StoneList[i + (filledStoneInt * 9)].Level, new Vector2(700, 120 + i * 80), Color.White);
                    }
                }
                else
                {
                    for (int i = 0; i < FilledStone.StoneList.Count - (FilledStone.StoneListPages * 9); i++)
                    {
                        spriteBatch.DrawString(Combat.Instance.CombatFont, FilledStone.StoneList[i + (filledStoneInt * 9)].Monster, new Vector2(200, 120 + i * 80), Color.White);
                        spriteBatch.DrawString(Combat.Instance.CombatFont, FilledStone.StoneList[i + (filledStoneInt * 9)].Element, new Vector2(400, 120 + i * 80), Color.White);
                        spriteBatch.DrawString(Combat.Instance.CombatFont, "lvl" + FilledStone.StoneList[i + (filledStoneInt * 9)].Level, new Vector2(700, 120 + i * 80), Color.White);
                    }
                }

                if (FilledStone.StoneList.Count != 0)
                {
                    spriteBatch.Draw(FilledStone.StoneList[selectedInt + FilledStoneInt * 9].Sprite, new Vector2(1250, 100), Color.White);
                    spriteBatch.DrawString(Combat.Instance.CombatFont, FilledStone.StoneList[selectedInt + FilledStoneInt * 9].Monster, new Vector2(1300, 350), Color.White);
                }

                spriteBatch.DrawString(Combat.Instance.CombatFont, (filledStoneInt + 1 ) + " / " + (FilledStone.StoneListPages + 1), new Vector2(400, 900), Color.White);
            }

            spriteBatch.Draw(arrow, new Vector2(155, 120 + 80 * selectedInt), Color.White);
        }

        public void ChangeState()
        {
            if (inventoryState == "GeneralMenu")
            {
                switch (selectedInt)
                {
                    case 0:
                        inventoryState = "Character";
                        break;
                    case 1:
                        inventoryState = "Equipment";
                        break;
                    case 2:
                        inventoryState = "Inventory";
                        break;
                    case 3:
                        inventoryState = "Log";
                        break;
                    case 4:
                        inventoryState = "Save";
                        break;
                    case 5:
                        inventoryState = "Options";
                        break;
                    case 6:
                        inventoryState = "Quit";
                        break;
                }
            }

            if (inventoryState == "Inventory")
            {
                switch (selectedInt)
                {
                    case 0:
                        inventoryState = "Consumables";
                        break;
                    case 1:
                        inventoryState = "FilledStones";
                        break;
                }
            }
        }

        public void ChangeSelected(int max)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && delay > 100 && SelectedInt > 0)
            {
                SelectedInt--;
                delay = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down) && delay > 100 && SelectedInt < max)
            {
                SelectedInt++;
                delay = 0;
            }
        }
    }
}
