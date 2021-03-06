﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Warlock_The_Soulbinder
{
    /// <summary>
    /// Class for Dialogue
    /// </summary>
    public class Dialogue : Menu
    {
        int currentDialogue = 1;
        Texture2D dialogueBar;
        static Dialogue instance;

        /// <summary>
        /// The NPC that is talking
        /// </summary>
        public NPC TalkingNPC { get; set; }
        /// <summary>
        /// A collection of dialogue to cycle through
        /// </summary>
        public Dictionary<int, string> DialogueLines { get; set; } = new Dictionary<int, string>();
        /// <summary>
        /// Used to limit the speed the player can cycle thorugh dialogue
        /// </summary>
        public double DialogueTimer { get; private set; }
        /// <summary>
        /// Is the player in dialogue
        /// </summary>
        public bool InDialogue { get; set; } = false;
        /// <summary>
        /// Used to set a time limit to enter dialogue
        /// </summary>
        public double ExitDialogueTimer { get; set; }

        /// <summary>
        /// Returns an instance of the Dialogue class
        /// </summary>
        public static Dialogue Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Dialogue();
                }
                return instance;
            }
        }

        /// <summary>
        /// Creates a new Dialogue
        /// </summary>
        private Dialogue()
        {
            dialogueBar = GameWorld.ContentManager.Load<Texture2D>("dialogueBar");
        }

        /// <summary>
        /// Updates the Dialogue
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (InDialogue)
            {
                DialogueTimer += GameWorld.deltaTimeSecond;
            }

            if (!InDialogue && ExitDialogueTimer <= 1)
            {
                ExitDialogueTimer += GameWorld.deltaTimeSecond;
            }

            // if the dialogue cycles through all lines exit dialogue and reset
            if (currentDialogue > DialogueLines.Count && DialogueLines.Count > 0)
            {
                currentDialogue = 1;
                InDialogue = false;
                foreach (NPC npc in GameWorld.Instance.CurrentZone().NPCs)
                {
                    npc.Talking = false;
                }
                DialogueTimer = 0;
                GameWorld.Instance.GameState = "Overworld";
                if (TalkingNPC.IsShrine == true) // if the player is talking to a shrine, teleport to the dragonRealm
                {
                    GameWorld.Instance.currentZone = "DragonRealm";
                    GameWorld.Instance.CurrentZone().GenerateZone();
                    Player.Instance.Position = new Vector2(3300, 6100);
                }
            }
        }

        /// <summary>
        /// Draws the dialogue text and the dialogue bar
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 dialogueBarPos = new Vector2(-GameWorld.Instance.camera.ViewMatrix.Translation.X + GameWorld.Instance.ScreenSize.Width * 0.5f - dialogueBar.Width * 0.5f, -GameWorld.Instance.camera.ViewMatrix.Translation.Y + GameWorld.Instance.ScreenSize.Height - dialogueBar.Height);
            spriteBatch.Draw(dialogueBar, dialogueBarPos, Color.White);
            spriteBatch.DrawString(GameWorld.Instance.copperFont, DialogueLines[currentDialogue], new Vector2(dialogueBarPos.X + 20, dialogueBarPos.Y + dialogueBar.Height * 0.5f - 15), Color.Black);
        }

        /// <summary>
        /// Method that makes the dialogue go to the next line
        /// </summary>
        public void NextDialogue()
        {
            if (DialogueTimer >= 1)
            {
                currentDialogue++;
                DialogueTimer = 0;
            }
        }
    }
}