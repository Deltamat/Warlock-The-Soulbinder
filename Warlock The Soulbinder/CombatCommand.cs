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

        public CombatCommand(int i)
        {
            select = i;
        }

        public void Execute(Player p)
        {
            if (GameWorld.Instance.GameState == "Combat")
            {
                Combat.Instance.ChangeSelected(select);
            }            
        }
    }
}
