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


        //Saved Keys and Buttons
        private  Keys keyDown = Keys.Down;
        private  Keys keyUp = Keys.Up;
        private  Keys keyRight = Keys.Right;
        private  Keys keyLeft = Keys.Left;
        private  Keys keySelect = Keys.Enter;
        private  Keys keyCancel = Keys.RightShift;
        private  Keys keyReturn = Keys.Back;
        private  Keys keyMenu = Keys.D3;

        private  Buttons buttonDown = Buttons.DPadDown;
        private  Buttons buttonUp = Buttons.DPadUp;
        private  Buttons buttonRight = Buttons.DPadRight;
        private  Buttons buttonLeft = Buttons.DPadLeft;
        private  Buttons buttonSelect = Buttons.A;
        private  Buttons buttonCancel = Buttons.B;
        private  Buttons buttonReturn = Buttons.Y;
        private  Buttons buttonMenu = Buttons.Start;

        public  Keys KeyDown { get => keyDown; set => keyDown = value; }
        public  Keys KeyUp { get => keyUp; set => keyUp = value; }
        public  Keys KeyRight { get => keyRight; set => keyRight = value; }
        public  Keys KeyLeft { get => keyLeft; set => keyLeft = value; }
        public  Keys KeySelect { get => keySelect; set => keySelect = value; }
        public  Keys KeyCancel { get => keyCancel; set => keyCancel = value; }
        public  Keys KeyReturn { get => keyReturn; set => keyReturn = value; }
        public  Keys KeyMenu { get => keyMenu; set => keyMenu = value; }
        public  Buttons ButtonDown { get => buttonDown; set => buttonDown = value; }
        public  Buttons ButtonUp { get => buttonUp; set => buttonUp = value; }
        public  Buttons ButtonRight { get => buttonRight; set => buttonRight = value; }
        public  Buttons ButtonLeft { get => buttonLeft; set => buttonLeft = value; }
        public  Buttons ButtonSelect { get => buttonSelect; set => buttonSelect = value; }
        public  Buttons ButtonCancel { get => buttonCancel; set => buttonCancel = value; }
        public  Buttons ButtonReturn { get => buttonReturn; set => buttonReturn = value; }
        public  Buttons ButtonMenu { get => buttonMenu; set => buttonMenu = value; }

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

        public void Execute()
        {
            KeyboardState keystate = Keyboard.GetState();

            foreach (Keys key in keybinds.Keys)
            {
                if (keystate.IsKeyDown(key))
                {
                    keybinds[key].Execute();
                }
            }
        }

        /// <summary>
        /// Checks if a key is pressed
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool keyPressed(Keys key)
        {
            bool pressed = Keyboard.GetState().IsKeyDown(key);
            return pressed;
        }

        /// <summary>
        /// Checks if a button is pressed
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool buttonPressed(Buttons button)
        {
            bool pressed = GamePad.GetState(PlayerIndex.One).IsButtonDown(button);
            return pressed;
        }

        /// <summary>
        /// Returns a keytype: 0 = Keybinds, 1 = Controller
        /// </summary>
        /// <param name="keyOption"></param>
        /// <returns></returns>

    }
}
