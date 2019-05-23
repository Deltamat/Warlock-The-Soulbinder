using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warlock_The_Soulbinder
{
    public class Trigger
    {
        public string Name { get; private set; }
        public string TargetName { get; private set; }
        public Vector2 Position { get; private set; }
        public Rectangle CollisionBox { get; private set; }
        public bool IsEntryTrigger { get; private set; }
        public Vector2 TargetPos { get; set; }
        public string TargetZone { get; set; }

        private Trigger()
        {

        }

        /// <summary>
        /// EntryTrigger constructor
        /// </summary>
        /// <param name="position">The position of the trigger</param>
        /// <param name="collisionBox">The Rectangle collisionBox</param>
        /// <param name="targetZone">The name of the zone in which the targetTrigger is located</param>
        /// <param name="targetName">The name of the targetTrigger</param>
        public Trigger(Vector2 position, Rectangle collisionBox, string targetName)
        {
            Trigger trigger = new Trigger();
            TargetName = targetName;
            Position = position;
            CollisionBox = collisionBox;
            IsEntryTrigger = true;
            
        }

        /// <summary>
        /// ExitTrigger constructor
        /// </summary>
        /// <param name="name">The name of the exitTrigger</param>
        /// <param name="position">The position of the trigger</param>
        /// <param name="collisionBox">The Rectangle collisionBox</param>
        /// <param name="zoneName">The name of the zone the trigger is located in</param>
        public Trigger(string name, Vector2 position, Rectangle collisionBox, string zoneName)
        {
            Name = name;
            Position = position;
            CollisionBox = collisionBox;
            IsEntryTrigger = false;
            TargetZone = zoneName;
        }
    }
}
