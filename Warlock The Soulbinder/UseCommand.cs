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
        float useDistance = 60;

        public UseCommand()
        {

        }

        public void Execute()
        {
            if (!Dialogue.Instance.InDialogue && Dialogue.Instance.exitDialogueTimer > 1)
            {
                Dialogue.Instance.exitDialogueTimer = 0;
                foreach (var npc in GameWorld.Instance.CurrentZone().NPCs)
                {
                    if (Vector2.Distance(Player.Instance.Position, npc.Value.Position) < useDistance)
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
