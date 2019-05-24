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
        Dictionary<int, string> dialogueLines = new Dictionary<int, string>();

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
        /// <param name="index">NPC id</param>
        /// <param name="position">The position of the NPC</param>
        /// <param name="hasQuest">Does the NPC have a quest</param>
        /// <param name="hasShop">Does the NPC have a shop</param>
        /// <param name="questID">The id of the quest this NPC has</param>
        /// <param name="dialogue">If the NPC does not have a quest or a shop it will say this dialogue</param>
        public NPC(int index, string spriteName, Vector2 position, bool hasQuest, bool hasShop, int questID, string dialogue)
        {
            this.index = index;
            this.hasQuest = hasQuest;
            this.hasShop = hasShop;
            this.questID = questID;
            Position = position;
            Sprite = GameWorld.ContentManager.Load<Texture2D>(spriteName);
            scale = 0.135f;

            dialogueLines.Add(1, dialogue);
            UpdateDialogue();
        }

        public override void Update(GameTime gameTime)
        {
            if (Talking)
            {
                Dialogue.Instance.dialogueLines = dialogueLines;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, null, Color.White, 0f, Vector2.Zero, scale, new SpriteEffects(), 1f);
        }

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
                dialogueLines.Add(1, "How is it coming with that quest Warlock?");
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

        public void EnterDialogue()
        {
            Dialogue.Instance.InDialogue = true;
            Talking = true;
            GameWorld.Instance.GameState = "Dialogue";
        }
    }
}
