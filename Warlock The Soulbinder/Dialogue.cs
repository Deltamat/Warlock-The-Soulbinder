using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Warlock_The_Soulbinder
{
    public class Dialogue : Menu
    {
        int currentDialogue = 1;

        public Dictionary<int, string> dialogueLines { get; set; } = new Dictionary<int, string>();
        public double dialogueTimer { get; set; }
        public bool InDialogue { get; set; } = false;
        public double exitDialogueTimer { get; set; }

        static Dialogue instance;
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

        private Dialogue()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            if (InDialogue)
            {
                dialogueTimer += GameWorld.deltaTimeSecond;
            }
            if (!InDialogue && exitDialogueTimer <= 1)
            {
                exitDialogueTimer += GameWorld.deltaTimeSecond;
            }

            // if the dialogue cycle through all lines exit dialogue and reset
            if (currentDialogue > dialogueLines.Count && dialogueLines.Count > 0)
            {
                currentDialogue = 1;
                InDialogue = false;
                foreach (var npc in GameWorld.Instance.CurrentZone().NPCs)
                {
                    npc.Value.Talking = false;
                }
                dialogueTimer = 0;
                GameWorld.Instance.GameState = "Overworld";
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(GameWorld.Instance.font, dialogueLines[currentDialogue], GameWorld.Instance.camera.Position, Color.White);
        }

        /// <summary>
        /// Method that makes the dialogue go to the next line
        /// </summary>
        public void NextDialogue()
        {
            if (dialogueTimer >= 1)
            {
                currentDialogue++;
                dialogueTimer = 0;
            }
            
        }
    }
}
