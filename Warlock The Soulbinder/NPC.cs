using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    public class NPC : Character
    {
        string dialogue;
        bool hasQuest;
        bool hasShop;
        int index;

        public NPC(int index, bool hasQuest, bool hasShop)
        {
            this.index = index;
            this.hasQuest = hasQuest;
            this.hasShop = hasShop;
            Sprite = GameWorld.ContentManager.Load<Texture2D>("tempPlayer");
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, Color.White);
        }

    }
}
