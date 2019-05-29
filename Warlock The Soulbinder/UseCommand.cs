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
        Sound npcTalk = new Sound("npcTalk");

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
                    if (npc.DrawInteract == true)
                    {
                        npcTalk.Play();
                        npc.EnterDialogue();
                        if (npc.HasHeal)
                        {
                            Player.Instance.CurrentHealth = Player.Instance.MaxHealth;
                        }
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
