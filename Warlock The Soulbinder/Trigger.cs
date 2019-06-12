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
        /// <summary>
        /// The name of the Trigger
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// The name of the exitTrigger this Trigger points to. Only used by entryTriggers
        /// </summary>
        public string TargetName { get; private set; }
        /// <summary>
        /// The position of the Trigger
        /// </summary>
        public Vector2 Position { get; private set; }
        /// <summary>
        /// A Collisionbox of the Trigger
        /// </summary>
        public Rectangle CollisionBox { get; private set; }
        /// <summary>
        /// Is this Trigger a entryTrigger
        /// </summary>
        public bool IsEntryTrigger { get; private set; }
        /// <summary>
        /// The position of the exitTrigger this Trigger connects to
        /// </summary>
        public Vector2 TargetPos { get; set; }
        /// <summary>
        /// The zone the exitTrigger is located in
        /// </summary>
        public string TargetZone { get; set; }

        /// <summary>
        /// EntryTrigger constructor
        /// </summary>
        /// <param name="position">The position of the trigger</param>
        /// <param name="collisionBox">The Rectangle collisionBox</param>
        /// <param name="targetName">The name of the targetTrigger</param>
        public Trigger(Vector2 position, Rectangle collisionBox, string targetName)
        {
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
