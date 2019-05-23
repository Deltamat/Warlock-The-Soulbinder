using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    class MultiCommand : ICommand
    {
        private MoveCommand moveCommand;
        private CombatCommand combatCommand;

        public MultiCommand(Vector2 direction, int i)
        {
            moveCommand = new MoveCommand(direction);
            combatCommand = new CombatCommand(i);
        }

        public void Execute()
        {
            moveCommand.Execute();
            combatCommand.Execute();
        }
    }
}
