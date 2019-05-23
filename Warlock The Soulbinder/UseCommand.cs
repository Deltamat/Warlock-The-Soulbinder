using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    class UseCommand : ICommand
    {
        float useDistance = 80;

        public UseCommand()
        {

        }

        /// <summary>
        /// Checks if the player is close to a NPC to enter dialogue
        /// if already in dialogue go to next dialogue line
        /// </summary>
        public void Execute()
        {
            if (!Dialogue.Instance.InDialogue && Dialogue.Instance.exitDialogueTimer > 1)
            {
                Dialogue.Instance.exitDialogueTimer = 0;
                foreach (var npc in GameWorld.Instance.CurrentZone().NPCs)
                {
                    // check 
                    if (Vector2.Distance(Player.Instance.CollisionBox.Center.ToVector2(), npc.Value.CollisionBox.Center.ToVector2()) < useDistance)
                    {
                        Dialogue.Instance.InDialogue = true;
                        npc.Value.Talking = true;
                        GameWorld.Instance.GameState = "Dialogue";
                        break;
                    }
                }
            }
            if (Dialogue.Instance.InDialogue)
            {
                Dialogue.Instance.NextDialogue();
                
            }

        }
    }
}
