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
                    // if close to an npc enter dialogue
                    if (npc.Value.DrawInteract == true)
                    {
                        npc.Value.EnterDialogue();
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
