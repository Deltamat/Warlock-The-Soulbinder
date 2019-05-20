using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    class MoveCommand : ICommand
    {
        private Vector2 direction;

        public MoveCommand(Vector2 direction)
        {
            this.direction = direction;
        }

        public void Execute(Player p)
        {
            if (GameWorld.Instance.GameState == "Overworld")
            {
                p.Move(direction);
            }
        }
    }
}
