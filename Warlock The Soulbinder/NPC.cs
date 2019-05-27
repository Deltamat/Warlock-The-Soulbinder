using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    public class NPC : Character
    {
        bool hasQuest;
        bool hasShop;
        int index;       
        int questID;
        Texture2D interact;
        float interactScale;
        float interactDistance = 80;
        Dictionary<int, string> dialogueLines = new Dictionary<int, string>();
        public bool DrawInteract { get; set; }

        public bool Talking { get; set; } = false;
        public override Rectangle CollisionBox
        {
            get
            {
                return new Rectangle((int)(Position.X + Sprite.Width * 0.32 * scale), (int)(Position.Y + Sprite.Height * 0.2 * scale), (int)(Sprite.Width * 0.4 * scale), (int)(Sprite.Height * 0.65 * scale));
            }
        }

        /// <summary>
        /// Creates a NPC
        /// </summary>
        /// <param name="spriteName">The name of the sprite</param>
        /// <param name="position">The position of the NPC</param>
        /// <param name="hasQuest">Does the NPC have a quest</param>
        /// <param name="hasShop">Does the NPC have a shop</param>
        /// <param name="questID">The id of the quest this NPC has</param>
        /// <param name="dialogue">If the NPC does not have a quest or a shop it will say this dialogue</param>
        public NPC(string spriteName, Vector2 position, bool hasQuest, bool hasShop, int questID, string dialogue)
        {
            this.hasQuest = hasQuest;
            this.hasShop = hasShop;
            this.questID = questID;
            Position = position;
            Sprite = GameWorld.ContentManager.Load<Texture2D>(spriteName);
            scale = 0.135f;
            interact = GameWorld.ContentManager.Load<Texture2D>("interact");
            interactScale = 0.3f;

            dialogueLines.Add(1, dialogue);
            UpdateDialogue();
        }

        public override void Update(GameTime gameTime)
        {
            if (Vector2.Distance(Player.Instance.CollisionBox.Center.ToVector2(), CollisionBox.Center.ToVector2()) < interactDistance)
            {
                DrawInteract = true;
            }
            else
            {
                DrawInteract = false;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, null, Color.White, 0f, Vector2.Zero, scale, new SpriteEffects(), 1f);

            if (DrawInteract == true && Talking == false)
            {
                spriteBatch.Draw(interact, new Vector2(Player.Instance.Position.X + 20, Player.Instance.Position.Y - 50), null, Color.White, 0f, Vector2.Zero, interactScale, new SpriteEffects(), 1f);
                spriteBatch.DrawString(GameWorld.Instance.copperFont, "E", new Vector2(Player.Instance.Position.X + 32, Player.Instance.Position.Y - 47), Color.Black);
            }

        }

        /// <summary>
        /// Method that assigns the corrrect dialogue to the npc based on if he it has a quest/shop
        /// </summary>
        public void UpdateDialogue()
        {
            if (hasQuest || hasShop) // remove the default dialogue if NPC has quest or shop
            {
                dialogueLines = new Dictionary<int, string>();
            }

            if (hasQuest && Quest.Instance.Quests.ContainsKey(questID)) // if the quest has not been completed
            {
                dialogueLines.Add(1, "Warlock! I have dire need of your assistance!");
                dialogueLines.Add(2, "The future of the world is at stake!");
                dialogueLines.Add(3, Quest.Instance.QuestDescription[questID]);
            }
            else if (hasQuest && Quest.Instance.OngoingQuests.ContainsKey(questID)) // if the quest is ongoing
            {
                dialogueLines.Add(1, "How is it going with that quest Warlock?");
                dialogueLines.Add(2, "Let me refresh your memory.");
                dialogueLines.Add(3, Quest.Instance.QuestDescription[questID]);
            }
            else if (hasQuest && Quest.Instance.Completed.ContainsKey(questID)) // if  the quest is completed
            {
                dialogueLines.Add(1, "Thank you for your help Warlock.");
            }
            else if (hasShop) // if has a shop
            {
                dialogueLines.Add(1, "Khajit has wares, if you have coins.");
            }
        }

        /// <summary>
        /// Method that makes the npc enter dialogue and sets the gameState to Dialogue
        /// </summary>
        public void EnterDialogue()
        {
            Dialogue.Instance.dialogueLines = dialogueLines;
            Dialogue.Instance.InDialogue = true;
            Talking = true;
            GameWorld.Instance.GameState = "Dialogue";
        }
    }
}
