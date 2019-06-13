using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    class CombatCommand : ICommand
    {
        private int select;

        /// <summary>
        /// Commbat command
        /// </summary>
        public CombatCommand(int i)
        {
            select = i;
        }

        /// <summary>
        /// Execute combatCommand
        /// </summary>
        public void Execute()
        {
            if (GameWorld.Instance.GameState == "Combat" && !GameWorld.Instance.Saving)
            {
                Combat.Instance.ChangeSelected(select);
            }            
        }
    }
}