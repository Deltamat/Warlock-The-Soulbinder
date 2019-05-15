using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    public class Button : GameObject
    {
        private MouseState previousMouse;
        private MouseState currentMouse;
        private Vector2 positionButton;
        private bool isHovering;
        private Texture2D texture;

        public EventHandler Click()
        {
            throw new NotImplementedException();
        }
    }
}