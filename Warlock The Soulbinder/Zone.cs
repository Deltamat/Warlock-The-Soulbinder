using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using System.Collections.Generic;

namespace Warlock_The_Soulbinder
{
    public class Zone
    {
        List<Rectangle> spawnPoints = new List<Rectangle>();
        List<Rectangle> usedSpawnPoints = new List<Rectangle>();
        int enemiesInZone = 3;

        public TiledMapRenderer MapRenderer { get; set; }
        public List<Enemy> Enemies { get; set; } = new List<Enemy>();
        public string Name { get; private set; }
        public TiledMap Map { get; private set; }
        public List<Rectangle> CollisionRects { get; private set; } = new List<Rectangle>();
        public List<Trigger> Triggers { get; private set; } = new List<Trigger>();
        public List<NPC> NPCs { get; private set; } = new List<NPC>();


        /// <summary>
        /// Creates a Zone with a name that contains the Tiled map and Tiled objects
        /// </summary>
        /// <param name="zoneName">The name of the Zone</param>
        public Zone(string zoneName, int enemiesInZone)
        {
            Name = zoneName;
            this.enemiesInZone = enemiesInZone;

            Map = GameWorld.ContentManager.Load<TiledMap>($"zones/{Name}");
            MapRenderer = new TiledMapRenderer(GameWorld.Instance.GraphicsDevice);

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
                        Triggers.Add(new Trigger(tileObject.Position, triggerRect, tileObject.Properties["targetName"]));
                    }
                    else if (tileObject.Type == "ExitTrigger") // if an exitTrigger use the exitTrigger constructor
                    {
                        Rectangle triggerRect = new Rectangle((int)tileObject.Position.X, (int)tileObject.Position.Y, (int)tileObject.Size.Width, (int)tileObject.Size.Height);
                        Triggers.Add(new Trigger(tileObject.Properties["name"], tileObject.Position, triggerRect, this.Name));
                    }
                    else if (tileObject.Type == "Spawn") // if an spawnpoint add to the spawnpoint list
                    {
                        spawnPoints.Add(new Rectangle((int)tileObject.Position.X, (int)tileObject.Position.Y, (int)tileObject.Size.Width, (int)tileObject.Size.Height));
                    }
                }
            }

            if (Name == "Town") // giv navnet på zonen og opret de rigtige npc'er
            {
                NPCs.Add(new NPC("npc_knight", new Vector2(400), true, false, 1, "normies get out reeeee"));
                NPCs.Add(new NPC("npc_old", new Vector2(1000), false, true, 1, "normies get out reeeee"));
            }
            if (Name == "Dragon")
            {
                NPC fireDragon = new NPC("npc_old", new Vector2(100), false, false, 1, "rawr");
                fireDragon.dragonType = "Grass";
                NPCs.Add(fireDragon);
            }
            foreach (var npc in NPCs)
            {
                CollisionRects.Add(npc.CollisionBox);
            }
        }

        /// <summary>
        /// Method that populates a zone with enemies
        /// </summary>
        public void GenerateZone()
        {
            if (Name == "Beast")
            {
                for (int i = 0; i < enemiesInZone; i++)
                {
                    Rectangle temp = spawnPoints[GameWorld.Instance.RandomInt(0, spawnPoints.Count)];
                    Enemies.Add(new Enemy(GameWorld.Instance.RandomInt(0, 3), new Vector2(temp.X, temp.Y)));
                    usedSpawnPoints.Add(temp);
                    spawnPoints.Remove(temp);
                }
                spawnPoints.AddRange(usedSpawnPoints);
                usedSpawnPoints = new List<Rectangle>();
            }
            else if (Name == "Grass")
            {
                for (int i = 0; i < enemiesInZone; i++)
                {
                    Rectangle temp = spawnPoints[GameWorld.Instance.RandomInt(0, spawnPoints.Count)];
                    Enemies.Add(new Enemy(GameWorld.Instance.RandomInt(3, 6), new Vector2(temp.X, temp.Y)));
                    usedSpawnPoints.Add(temp);
                    spawnPoints.Remove(temp);
                }
                spawnPoints.AddRange(usedSpawnPoints);
                usedSpawnPoints = new List<Rectangle>();
            }
            else if (Name == "Water")
            {
                for (int i = 0; i < enemiesInZone; i++)
                {
                    Rectangle temp = spawnPoints[GameWorld.Instance.RandomInt(0, spawnPoints.Count)];
                    Enemies.Add(new Enemy(GameWorld.Instance.RandomInt(6, 9), new Vector2(temp.X, temp.Y)));
                    usedSpawnPoints.Add(temp);
                    spawnPoints.Remove(temp);
                }
                spawnPoints.AddRange(usedSpawnPoints);
                usedSpawnPoints = new List<Rectangle>();
            }
            else if (Name == "Undead")
            {
                for (int i = 0; i < enemiesInZone; i++)
                {
                    Rectangle temp = spawnPoints[GameWorld.Instance.RandomInt(0, spawnPoints.Count)];
                    Enemies.Add(new Enemy(GameWorld.Instance.RandomInt(9, 12), new Vector2(temp.X, temp.Y)));
                    usedSpawnPoints.Add(temp);
                    spawnPoints.Remove(temp);
                }
                spawnPoints.AddRange(usedSpawnPoints);
                usedSpawnPoints = new List<Rectangle>();
            }
            else if (Name == "Metal")
            {
                for (int i = 0; i < enemiesInZone; i++)
                {
                    Rectangle temp = spawnPoints[GameWorld.Instance.RandomInt(0, spawnPoints.Count)];
                    Enemies.Add(new Enemy(GameWorld.Instance.RandomInt(12, 15), new Vector2(temp.X, temp.Y)));
                    usedSpawnPoints.Add(temp);
                    spawnPoints.Remove(temp);
                }
                spawnPoints.AddRange(usedSpawnPoints);
                usedSpawnPoints = new List<Rectangle>();
            }
            else if (Name == "Fire")
            {
                for (int i = 0; i < enemiesInZone; i++)
                {
                    Rectangle temp = spawnPoints[GameWorld.Instance.RandomInt(0, spawnPoints.Count)];
                    Enemies.Add(new Enemy(GameWorld.Instance.RandomInt(15, 18), new Vector2(temp.X, temp.Y)));
                    usedSpawnPoints.Add(temp);
                    spawnPoints.Remove(temp);
                }
                spawnPoints.AddRange(usedSpawnPoints);
                usedSpawnPoints = new List<Rectangle>();
            }
            else if (Name == "Wind")
            {
                for (int i = 0; i < enemiesInZone; i++)
                {
                    Rectangle temp = spawnPoints[GameWorld.Instance.RandomInt(0, spawnPoints.Count)];
                    Enemies.Add(new Enemy(GameWorld.Instance.RandomInt(18, 21), new Vector2(temp.X, temp.Y)));
                    usedSpawnPoints.Add(temp);
                    spawnPoints.Remove(temp);
                }
                spawnPoints.AddRange(usedSpawnPoints);
                usedSpawnPoints = new List<Rectangle>();
            }
        }

        public void Update(GameTime gameTime)
        {
            MapRenderer.Update(Map, gameTime);

            foreach (Trigger trigger in Triggers) // checks if the player entered a trigger
            {
                // What happpens when player enters a zone trigger
                if (trigger.IsEntryTrigger == true && trigger.CollisionBox.Intersects(Player.Instance.CollisionBox))
                {
                    foreach (var enemy in Enemies)
                    {
                        enemy.Alive = false;
                    }
                    Enemies = new List<Enemy>();
                    GameWorld.Instance.currentZone = trigger.TargetZone;
                    GameWorld.Instance.CurrentZone().GenerateZone(); // Generate the new zone with enemies/npcs
                    Player.Instance.Position = new Vector2(trigger.TargetPos.X - 19, trigger.TargetPos.Y - 6);
                }
            }

            foreach (var npc in NPCs)
            {
                npc.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in Enemies)
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

            foreach (var npc in NPCs)
            {
                npc.Draw(spriteBatch);
                GameWorld.Instance.DrawRectangle(npc.CollisionBox);
            }

            //MapRenderer.Draw(Map, GameWorld.Instance.camera.viewMatrix);
            //foreach (var layer in Map.TileLayers)
            //{
            //    if (layer.Name == "Top" || layer.Name == "OverTop")
            //    {
            //        mapRenderer.Draw(layer, GameWorld.Instance.camera.viewMatrix, null, null, 0.99f);
            //    }
            //    else
            //    {
            //        mapRenderer.Draw(layer, GameWorld.Instance.camera.viewMatrix, null, null, 0.98f);
            //    }
            //}

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
