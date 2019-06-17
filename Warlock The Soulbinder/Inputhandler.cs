using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    /// <summary>
    /// Inputhandler
    /// </summary>
    public class InputHandler
    {
        private Dictionary<Keys, ICommand> keybinds = new Dictionary<Keys, ICommand>();
        private Dictionary<Buttons, ICommand> buttonbinds = new Dictionary<Buttons, ICommand>();

        static InputHandler instance;

        /// <summary>
        /// Creates an instance for the singleton
        /// </summary>
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

        KeyboardState state = Keyboard.GetState();
        //Saved Keys and Buttons

        private  Keys keyDown = Keys.Down;
        private  Keys keyUp = Keys.Up;
        private  Keys keyRight = Keys.Right;
        private  Keys keyLeft = Keys.Left;
        private  Keys keySelect = Keys.Enter;
        private  Keys keyCancel = Keys.RightShift;
        private  Keys keyReturn = Keys.Back;
        private  Keys keyMenu = Keys.Escape;

        private  Buttons buttonDown = Buttons.DPadDown;
        private  Buttons buttonUp = Buttons.DPadUp;
        private  Buttons buttonRight = Buttons.DPadRight;
        private  Buttons buttonLeft = Buttons.DPadLeft;
        private  Buttons buttonSelect = Buttons.A;
        private  Buttons buttonCancel = Buttons.B;
        private  Buttons buttonReturn = Buttons.Y;
        private  Buttons buttonMenu = Buttons.Start;

        private Keys keyW = Keys.W;
        private Keys keyS = Keys.S;

        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public Keys KeyDown { get => keyDown; set => keyDown = value; }

        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public Keys KeyUp { get => keyUp; set => keyUp = value; }

        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public Keys KeyRight { get => keyRight; set => keyRight = value; }

        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public Keys KeyLeft { get => keyLeft; set => keyLeft = value; }

        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public Keys KeySelect { get => keySelect; set => keySelect = value; }

        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public Keys KeyCancel { get => keyCancel; set => keyCancel = value; }

        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public Keys KeyReturn { get => keyReturn; set => keyReturn = value; }

        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public Keys KeyMenu { get => keyMenu; set => keyMenu = value; }

        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public Buttons ButtonDown { get => buttonDown; set => buttonDown = value; }

        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public Buttons ButtonUp { get => buttonUp; set => buttonUp = value; }

        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public Buttons ButtonRight { get => buttonRight; set => buttonRight = value; }

        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public Buttons ButtonLeft { get => buttonLeft; set => buttonLeft = value; }

        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public Buttons ButtonSelect { get => buttonSelect; set => buttonSelect = value; }

        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public Buttons ButtonCancel { get => buttonCancel; set => buttonCancel = value; }

        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public Buttons ButtonReturn { get => buttonReturn; set => buttonReturn = value; }

        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public Buttons ButtonMenu { get => buttonMenu; set => buttonMenu = value; }

        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public Keys KeyS { get => keyS; set => keyS = value; }

        /// <summary>
        /// Get-Set for field of same name
        /// </summary>
        public Keys KeyW { get => keyW; set => keyW = value; }

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
            keybinds.Add(Keys.Enter, new UseCommand());

            buttonbinds.Add(ButtonUp, new MultiCommand(new Vector2(0, -1), -1));
            buttonbinds.Add(ButtonDown, new MultiCommand(new Vector2(0, 1), 1));
            buttonbinds.Add(ButtonLeft, new MoveCommand(new Vector2(-1, 0)));
            buttonbinds.Add(buttonRight, new MoveCommand(new Vector2(1, 0)));

            buttonbinds.Add(ButtonSelect, new UseCommand());
        }

        /// <summary>
        /// Looks through if the predetermined keys and buttons are currently being held down
        /// </summary>
        public void Execute()
        {
            KeyboardState keystate = Keyboard.GetState();
            GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);

            foreach (Keys key in keybinds.Keys)
            {
                if (keystate.IsKeyDown(key))
                {
                    keybinds[key].Execute();
                }
            }

            foreach (Buttons key in buttonbinds.Keys)
            {
                if (gamepadState.IsButtonDown(key))
                {
                    buttonbinds[key].Execute();
                }
            }
        }

        /// <summary>
        /// Checks if a key is pressed
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyPressed(Keys key)
        {
            bool pressed = Keyboard.GetState().IsKeyDown(key);
            return pressed;
        }

        /// <summary>
        /// Checks if a button is pressed
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool ButtonPressed(Buttons button)
        {
            bool pressed = GamePad.GetState(PlayerIndex.One).IsButtonDown(button);
            return pressed;
        }

        //Code for changing keys
        public Keys ChangeKey(Keys oldKey)
        { 
            foreach (Keys key in Keyboard.GetState().GetPressedKeys())
            {

                if (oldKey != key)
                {
                    oldKey = key;
    
                }
                
                break;
            }

            return oldKey;
        }
    }
}