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
        Texture2D interact;
        float interactScale;
        float interactDistance = 100;
        bool dragonShrine;
        Dictionary<int, string> dialogueLines = new Dictionary<int, string>();

        public string DragonElement { get; set; }
        public bool HasHeal { get; private set; }
        public bool IsShrine { get; private set; }
        public bool DrawInteract { get; private set; }
        public bool Talking { get; set; } = false;
        public override Rectangle CollisionBox
        {
            get
            {
                if (!IsShrine)
                {
                    return new Rectangle((int)(Position.X + Sprite.Width * 0.32 * scale), (int)(Position.Y + Sprite.Height * 0.2 * scale), (int)(Sprite.Width * 0.4 * scale), (int)(Sprite.Height * 0.65 * scale));
                }
                else
                {
                    return new Rectangle((int)(Position.X), (int)(Position.Y), (int)(sprite.Width * scale), (int)(sprite.Height * scale));
                }
            }
        }

        /// <summary>
        /// Creates an NPC
        /// </summary>
        /// <param name="spriteName">The name of the sprite</param>
        /// <param name="position">The position of the NPC</param>
        /// <param name="isPillar">Is the npc a dragon pillar</param>
        /// <param name="hasShop">Does the NPC have a shop. Now used currently</param>
        /// <param name="isShrine">Is the npc a dragon shrine</param>
        /// <param name="questID">The id of the quest this NPC has. Not used currently</param>
        /// <param name="dialogue">If the NPC does not have a quest or a shop it will say this dialogue</param>
        public NPC(string spriteName, Vector2 position, bool isPillar, bool hasHeal, bool isShrine, string dialogue)
        {
            IsShrine = isShrine;
            dragonShrine = isShrine;
            HasHeal = hasHeal;
            Position = position;
            Sprite = GameWorld.ContentManager.Load<Texture2D>(spriteName);
            if (IsShrine)
            {
                scale = 0.7f;
            }

            else if(isPillar)
            {
                scale = 0.35f;
            }

            else
            {
                scale = 0.135f;
            }

            
            interact = GameWorld.ContentManager.Load<Texture2D>("interact");
            interactScale = 0.3f;

            dialogueLines.Add(1, dialogue);
            UpdateDialogue();
        }

        /// <summary>
        /// Update the npc and check the distance between the npc and player.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (Vector2.Distance(Player.Instance.CollisionBox.Center.ToVector2(), CollisionBox.Center.ToVector2()) < interactDistance)
            {
                DrawInteract = true;
            }

            else if (dragonShrine == true && Vector2.Distance(Player.Instance.CollisionBox.Center.ToVector2(), CollisionBox.Center.ToVector2()) < (interactDistance * (8/3)))
            {
                DrawInteract = true;
            }
            else
            {
                DrawInteract = false;
            }
        }

        /// <summary>
        /// Draws the npc and if player is within talking range, draw the interact icon.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsShrine)
            {
                spriteBatch.Draw(Sprite, Position, null, Color.White, 0f, Vector2.Zero, scale, new SpriteEffects(), 1f);
            }
            else
            {
                spriteBatch.Draw(Sprite, Position, null, Color.White, 0f, Vector2.Zero, scale, new SpriteEffects(), 1f);
            }
            
            if (DrawInteract == true && Talking == false)
            {
                spriteBatch.Draw(interact, new Vector2(Player.Instance.Position.X + 20, Player.Instance.Position.Y - 50), null, Color.White, 0f, Vector2.Zero, interactScale, new SpriteEffects(), 1f);
                spriteBatch.DrawString(GameWorld.Instance.copperFont, "E", new Vector2(Player.Instance.Position.X + 32, Player.Instance.Position.Y - 47), Color.Black);
            }
        }

        /// <summary>
        /// Method that assigns the corrrect dialogue to the npc.
        /// </summary>
        public void UpdateDialogue()
        {
            if (HasHeal || IsShrine) // remove the default dialogue
            {
                dialogueLines = new Dictionary<int, string>();
            }
            if (HasHeal) // if a healer
            {
                dialogueLines.Add(1, "You look hurt. Let me heal you right up!");
            }
            else if (IsShrine) // if a dragon shrine
            {
                dialogueLines.Add(1, $"Prepare to face the {DragonElement} Dragon!");
            }
        }

        /// <summary>
        /// Method that makes the npc enter dialogue and sets the gameState to Dialogue
        /// </summary>
        public void EnterDialogue()
        {
            Dialogue.Instance.DialogueLines = dialogueLines;
            Dialogue.Instance.InDialogue = true;
            Talking = true;
            GameWorld.Instance.GameState = "Dialogue";
            Dialogue.Instance.TalkingNPC = this;
        }

        /// <summary>
        /// Method that adds dialogue to the npc
        /// </summary>
        /// <param name="dialogue">The dialogue to be added</param>
        public void AddDialogue(string dialogue)
        {
            dialogueLines.Add(dialogueLines.Count + 1, dialogue);
        }
    }
}
