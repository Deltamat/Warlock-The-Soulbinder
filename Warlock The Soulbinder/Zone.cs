using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using System.Collections.Generic;

namespace Warlock_The_Soulbinder
{
    public class Zone
    {
        TiledMapRenderer mapRenderer;
        List<Enemy> enemies = new List<Enemy>();

        public string Name { get; private set; }
        public TiledMap Map { get; private set; }
        public List<Rectangle> CollisionRects { get; private set; } = new List<Rectangle>();
        public List<Trigger> Triggers { get; private set; } = new List<Trigger>();
        public Dictionary<int, NPC> NPCs { get; private set; } = new Dictionary<int, NPC>();

        /// <summary>
        /// Creates a Zone with a name that contains the Tiled map and Tiled objects
        /// </summary>
        /// <param name="zoneName">The name of the Zone</param>
        public Zone(string zoneName)
        {
            Name = zoneName;

            Map = GameWorld.ContentManager.Load<TiledMap>($"{Name}"); 
            //map = GameWorld.ContentManager.Load<TiledMap>("test3");
            mapRenderer = new TiledMapRenderer(GameWorld.Instance.GraphicsDevice);

            // The following extracts the objects from the Tiled TileMap
            foreach (TiledMapObjectLayer layer in Map.ObjectLayers) // go through all object layers
            {
                foreach (TiledMapObject tileObject in layer.Objects) //  go through all objects added in Tiled
                {
                    if (tileObject.Type == "") // if the object has not been given a type in Tiled
                    {
                        // create a Rectangle based on the object made in Tiled
                        CollisionRects.Add(new Rectangle((int)tileObject.Position.X, (int)tileObject.Position.Y, (int)tileObject.Size.Width, (int)tileObject.Size.Height));
                    }

                    else if (tileObject.Type == "EntryTrigger") // if an entryTrigger use the entryTrigger constructor
                    {
                        Rectangle triggerRect = new Rectangle((int)tileObject.Position.X, (int)tileObject.Position.Y, (int)tileObject.Size.Width, (int)tileObject.Size.Height);
                        Triggers.Add(new Trigger(tileObject.Position, triggerRect, null, tileObject.Properties["targetName"]));
                    }
                    else if (tileObject.Type == "ExitTrigger") // if an exitTrigger use the exitTrigger constructor
                    {
                        Rectangle triggerRect = new Rectangle((int)tileObject.Position.X, (int)tileObject.Position.Y, (int)tileObject.Size.Width, (int)tileObject.Size.Height);
                        Triggers.Add(new Trigger(tileObject.Properties["name"], tileObject.Position, triggerRect, this.Name, true));
                    }
                }
            }

            if (zoneName == "t")
            {
                NPCs.Add(1, new NPC(1, true, true));
            }
        }

        public void GenerateZone()
        {
            // kaldes når man går ind i en zone, 
            // skal laves når enemies/npc er færdigt
        }

        public void Update(GameTime gameTime)
        {
            mapRenderer.Update(Map, gameTime);

            foreach (Trigger trigger in Triggers) // checks if the player entered a trigger
            {
                // What happpens when player enters a zone trigger
                if (trigger.IsEntryTrigger == true && trigger.CollisionBox.Intersects(Player.Instance.CollisionBox))
                {
                    GameWorld.Instance.currentZone = trigger.TargetZone;
                    GameWorld.Instance.CurrentZone().GenerateZone(); // Generate the new zone with enemies/npcs
                    Player.Instance.Position = trigger.TargetPos;
                    
                }
            }

            foreach (var item in NPCs)
            {
                item.Value.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch);
                GameWorld.Instance.DrawRectangle(enemy.CollisionBox);
            }

            foreach (Rectangle item in CollisionRects)
            {
                GameWorld.Instance.DrawRectangle(item);
            }

            foreach (Trigger item in Triggers)
            {
                GameWorld.Instance.DrawRectangle(item.CollisionBox);
            }

            foreach (var item in NPCs)
            {
                item.Value.Draw(spriteBatch);
            }

            mapRenderer.Draw(Map, GameWorld.Instance.camera.viewMatrix);

        }

        /// <summary>
        /// The Setup is called after ALL the zones have been created. It then runs through all the triggers and set the targets.
        /// This is done because the triggers are located in the seperate zones.
        /// </summary>
        public void Setup()
        {
            foreach (Trigger trigger in Triggers)
            {
                foreach (Zone zone in GameWorld.Instance.zones)
                {
                    foreach (Trigger otherTrigger in zone.Triggers)
                    {
                        if (trigger.IsEntryTrigger == true && otherTrigger.IsEntryTrigger == false && trigger.TargetName == otherTrigger.Name)
                        {
                            trigger.TargetPos = otherTrigger.Position;
                            trigger.TargetZone = otherTrigger.TargetZone;
                        }
                    }
                }
            }
            
            
        }
    }
}
