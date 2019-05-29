using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Warlock_The_Soulbinder
{
    class Quest : Menu
    {
        public Dictionary<int, string> Quests { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, string> OngoingQuests { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, string> Completed { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, string> QuestDescription { get; set; } = new Dictionary<int, string>();

        static Quest instance;
        public static Quest Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Quest();
                }
                return instance;
            }
        }
        private Quest()
        {

        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
