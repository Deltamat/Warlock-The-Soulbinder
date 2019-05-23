using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    public class InputHandler
    {
        private Dictionary<Keys, ICommand> keybinds = new Dictionary<Keys, ICommand>();

        static InputHandler instance;
        public static InputHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputHandler();
                }
                return instance;
            }
        }

        private InputHandler()
        {
            keybinds.Add(Keys.A, new MoveCommand(new Vector2(-1, 0)));
            keybinds.Add(Keys.D, new MoveCommand(new Vector2(1, 0)));
            keybinds.Add(Keys.Left, new MoveCommand(new Vector2(-1, 0)));
            keybinds.Add(Keys.Right, new MoveCommand(new Vector2(1, 0)));

            keybinds.Add(Keys.W, new MultiCommand(new Vector2(0, -1), -1));
            keybinds.Add(Keys.S, new MultiCommand(new Vector2(0, 1), 1));

            keybinds.Add(Keys.Up, new MultiCommand(new Vector2(0, -1), -1));
            keybinds.Add(Keys.Down, new MultiCommand(new Vector2(0, 1), 1));

            keybinds.Add(Keys.E, new UseCommand());
        }

        public void Execute(Player p)
        {
            KeyboardState keystate = Keyboard.GetState();

            foreach (Keys key in keybinds.Keys)
            {
                if (keystate.IsKeyDown(key))
                {
                    keybinds[key].Execute(p);
                }
            }
        }
    }
}
