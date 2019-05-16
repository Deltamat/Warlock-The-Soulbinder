﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    class Menu
    {
        protected int selectedInt;
        protected Rectangle selectionRectangle;

        public int SelectedInt { get => selectedInt; set => selectedInt = value; }
        public Rectangle SelectionRectangle { get => selectionRectangle; set => selectionRectangle = value; }


        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
