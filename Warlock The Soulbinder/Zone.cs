using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;
using System;
using System.Collections.Generic;

namespace Warlock_The_Soulbinder
{
    public class Zone
    {
        List<Rectangle> spawnPoints = new List<Rectangle>();
        int enemiesInZone;
        List<NPC> pillars = new List<NPC>();

        /// <summary>
        /// A MonoGame Extended Class that is used to draw and update the TileMap
        /// </summary>
        public TiledMapRenderer MapRenderer { get; private set; }
        /// <summary>
        /// A list of Enemies in the Zone
        /// </summary>
        public List<Enemy> Enemies { get; set; } = new List<Enemy>();
        /// <summary>
        /// The name of the Zone
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// A MonoGame Extended Class that contains the TileMap
        /// </summary>
        public TiledMap Map { get; private set; }
        /// <summary>
        /// A list of Rectangle's that is collidable
        /// </summary>
        public List<Rectangle> CollisionRects { get; private set; } = new List<Rectangle>();
        /// <summary>
        /// A list of the Trigger's in the Zone
        /// </summary>
        public List<Trigger> Triggers { get; private set; } = new List<Trigger>();
        /// <summary>
        /// A list of the NPC's in the Zone
        /// </summary>
        public List<NPC> NPCs { get; private set; } = new List<NPC>();
        
        /// <summary>
        /// Creates a Zone with a name and number of enemies that contains the Tiled map and Tiled objects
        /// </summary>
        /// <param name="zoneName">The name of the Zone</param>
        /// <param name="enemiesInZone">How many enemies to spawn in the zone. Cant exceed number of spawn points </param>
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

            // if the specifed enemies exceeds the toal amount of spawnpoints, set to spawnPoints.Count
            if (this.enemiesInZone > spawnPoints.Count) 
            {
                this.enemiesInZone = spawnPoints.Count;
            }

            // based on the name of the zone create the belonging npc's
            if (Name == "Town")
            {                
                NPC temp = new NPC("npc/npc_knight", new Vector2(2800, 3000), false, false, false, "What a nice little lake.");
                temp.AddDialogue("I would like to take a swim, but.");
                temp.AddDialogue("This armour would make me sink like a rock.");
                NPCs.Add(temp);

                NPCs.Add(new NPC("npc/npc_healer", new Vector2(1800, 2520), false, true, false, "")); // healer
                NPCs.Add(new NPC("npc/npc_monk", new Vector2(185, 4160), false, false, false, "Can't a man get some privacy!"));

                temp = new NPC("npc/npc_megaold", new Vector2(1510, 550), false, false, false, "At the top of this mountain stands");
                temp.AddDialogue("the Dragon Shrines. The legends say that");
                temp.AddDialogue("only a Warlock who has mastered the elements");
                temp.AddDialogue("can defeat the mighty Dragons.");
                NPCs.Add(temp);

                NPCs.Add(new NPC("npc/npc_king", new Vector2(1260, 2750), false, false, false, "Pay your taxes peasant."));

                temp = new NPC("npc/npc_old", new Vector2(1950, 3630), false, false, false, "How could this have happened!?");
                temp.AddDialogue("My own creations have turned against me!");
                temp.AddDialogue("My workshop lies beyond the forest.");
                temp.AddDialogue("If you travel there, be careful.");
                temp.AddDialogue("Oh! And one more thing Warlock.");
                temp.AddDialogue("My Sentry's have the ability to scan any");
                temp.AddDialogue("creature and learn their weakness.");
                temp.AddDialogue("If you could capture one, it might prove");
                temp.AddDialogue("useful for a Warlock such as yourself.");
                NPCs.Add(temp);
            }
            if (Name == "Dragon")
            {

                NPC fireDragonShrine = new NPC("npc/fireDragonShrine", new Vector2(1870, 1175), false, false, true, "");
                fireDragonShrine.DragonElement = "Fire";
                NPCs.Add(fireDragonShrine);

                NPC waterDragonShrine = new NPC("npc/waterDragonShrine", new Vector2(2170, 1175), false, false, true, "");
                waterDragonShrine.DragonElement = "Water";
                NPCs.Add(waterDragonShrine);

                NPC metalDragonShrine = new NPC("npc/metalDragonShrine", new Vector2(2470, 1175), false, false, true, "");
                metalDragonShrine.DragonElement = "Metal";
                NPCs.Add(metalDragonShrine);

                NPC earthDragonShrine = new NPC("npc/earthDragonShrine", new Vector2(2675, 900), false, false, true, "");
                earthDragonShrine.DragonElement = "Earth";
                NPCs.Add(earthDragonShrine);

                NPC darkDragonShrine = new NPC("npc/darkDragonShrine", new Vector2(2470, 675), false, false, true, "");
                darkDragonShrine.DragonElement = "Dark";
                NPCs.Add(darkDragonShrine);

                NPC airDragonShrine = new NPC("npc/airDragonShrine", new Vector2(2170, 675), false, false, true, "");
                airDragonShrine.DragonElement = "Air";
                NPCs.Add(airDragonShrine);

                NPC neutralDragonShrine = new NPC("npc/neutralDragonShrine", new Vector2(1870, 675), false, false, true, "");
                neutralDragonShrine.DragonElement = "Neutral";
                NPCs.Add(neutralDragonShrine);

                NPC firePillar = new NPC("pillars/BlankPillar", new Vector2(1770, 1175), true, false, false, "");
                NPC waterPillar = new NPC("pillars/BlankPillar", new Vector2(2070, 1175), true, false, false, "");
                NPC metalPillar = new NPC("pillars/BlankPillar", new Vector2(2370, 1175), true, false, false, "");
                NPC earthPillar = new NPC("pillars/BlankPillar", new Vector2(2575, 900), true, false, false, "");
                NPC darkPillar = new NPC("pillars/BlankPillar", new Vector2(2370, 675), true, false, false, "");
                NPC airPillar = new NPC("pillars/BlankPillar", new Vector2(2070, 675), true, false, false, "");
                NPC neutralPillar = new NPC("pillars/BlankPillar", new Vector2(1770, 675), true, false, false, "");
                pillars.Add(firePillar);
                pillars.Add(waterPillar);
                pillars.Add(metalPillar);
                pillars.Add(earthPillar);
                pillars.Add(darkPillar);
                pillars.Add(airPillar);
                pillars.Add(neutralPillar);

                foreach (NPC pillar in pillars)
                {
                    CollisionRects.Add(pillar.CollisionBox);
                }
                foreach (NPC npc in NPCs)
                {
                    npc.UpdateDialogue();
                }
            }

            foreach (NPC npc in NPCs)
            {
                CollisionRects.Add(npc.CollisionBox);
            }
        }

        /// <summary>
        /// Method that populates a zone with enemies
        /// </summary>
        public void GenerateZone()
        {
            if (Name == "DragonRealm")
            {
                GenerateEnemies(Dialogue.Instance.TalkingNPC.DragonElement);
            }
            else
            {
                GenerateEnemies(Name);
            }
        }

        /// <summary>
        /// Updates the zone
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            MapRenderer.Update(Map, gameTime);

            foreach (Trigger trigger in Triggers) // checks if the player entered a trigger
            {
                // What happpens when player enters a zone trigger
                if (trigger.IsEntryTrigger == true && trigger.CollisionBox.Intersects(Player.Instance.CollisionBox) && !GameWorld.Instance.Saving)
                {
                    
                    KillEnemiesInZone();
                    GameWorld.Instance.currentZone = trigger.TargetZone;
                    GameWorld.Instance.CurrentZone().GenerateZone(); // Generate the new zone with enemies
                    Player.Instance.Position = new Vector2(trigger.TargetPos.X - 19, trigger.TargetPos.Y - 6);
                    Player.Instance.GracePeriod = 1.5;

                    if (trigger.TargetZone == "Dragon")
                    {
                        GameWorld.Instance.ChangeMusic();
                    }
                }
            }

            foreach (NPC npc in NPCs)
            {
                npc.Update(gameTime);
            }
        }

        /// <summary>
        /// Draws the enemies and npcs in the zone
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy enemy in Enemies)
            {
                enemy.Draw(spriteBatch);
#if DEBUG
                GameWorld.Instance.DrawRectangle(enemy.CollisionBox);
#endif
            }

            foreach (Rectangle item in CollisionRects)
            {
#if DEBUG
                GameWorld.Instance.DrawRectangle(item);
#endif
            }

            foreach (Trigger item in Triggers)
            {
#if DEBUG
                GameWorld.Instance.DrawRectangle(item.CollisionBox);
#endif
            }

            foreach (NPC npc in NPCs)
            {
                npc.Draw(spriteBatch);
#if DEBUG
                GameWorld.Instance.DrawRectangle(npc.CollisionBox);
#endif
            }
            if (Name == "Dragon")
            {
                foreach (NPC pillar in pillars)
                {
                    pillar.Draw(spriteBatch);
#if DEBUG
                    GameWorld.Instance.DrawRectangle(pillar.CollisionBox);
#endif
                }
            }
        }

        /// <summary>
        /// The Setup is called after ALL the zones have been created. It then runs through all the triggers to change zones and set the targets.
        /// This is done because the triggers are located in the seperate zones.
        /// </summary>
        public void Setup()
        {
            foreach (Trigger trigger in Triggers) // goes through all triggers in the list
            {
                foreach (Zone zone in GameWorld.Instance.zones) // then for each zone
                {
                    foreach (Trigger otherTrigger in zone.Triggers) // go through all triggers in that zone
                    {
                        // if the entrytriggers targetName = the other exittriggers name
                        if (trigger.IsEntryTrigger == true && otherTrigger.IsEntryTrigger == false && trigger.TargetName == otherTrigger.Name)
                        {
                            trigger.TargetPos = otherTrigger.Position;
                            trigger.TargetZone = otherTrigger.TargetZone;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Method that generates enemies in the zone
        /// </summary>
        /// <param name="enemyType">the type of enemy to generate</param>
        private void GenerateEnemies(string enemyType)
        {
            int enemyindex = 0;
            List<Rectangle> usedSpawnPoints = new List<Rectangle>();

            for (int i = 0; i < enemiesInZone; i++) // spawn a random enemy of the right type
            {
                switch (enemyType)
                {
                    case "Neutral":
                        enemyindex = GameWorld.Instance.RandomInt(0, 3);
                        break;
                    case "Earth":
                        enemyindex = GameWorld.Instance.RandomInt(3, 6);
                        break;
                    case "Water":
                        enemyindex = GameWorld.Instance.RandomInt(6, 9);
                        break;
                    case "Dark":
                        enemyindex = GameWorld.Instance.RandomInt(9, 12);
                        break;
                    case "Metal":
                        enemyindex = GameWorld.Instance.RandomInt(12, 15);
                        break;
                    case "Fire":
                        enemyindex = GameWorld.Instance.RandomInt(15, 18);
                        break;
                    case "Air":
                        enemyindex = GameWorld.Instance.RandomInt(18, 21);
                        break;
                }

                Rectangle temp = spawnPoints[GameWorld.Instance.RandomInt(0, spawnPoints.Count)]; // chose a random spawnpoint to spawn the enemy
                Enemies.Add(new Enemy(enemyindex, new Vector2(temp.X, temp.Y))); // add a enemy on the chosen spawnpoint
                usedSpawnPoints.Add(temp); // remove the spawnpoints so it isnt used twice
                spawnPoints.Remove(temp);
            }
            spawnPoints.AddRange(usedSpawnPoints);
            usedSpawnPoints = new List<Rectangle>();

            if (Name == "DragonRealm")
            {

                MediaPlayer.Play(GameWorld.Instance.DragonMusic, TimeSpan.Zero);
                GameWorld.Instance.ChangeMusic();
    
                int dragon = -1;
                if (enemyType == "Neutral" && Combat.Instance.NeutralDragonDead == false)
                {
                    dragon = 21;
                }
                if (enemyType == "Earth" && Combat.Instance.EarthDragonDead == false)
                {
                    dragon = 22;
                }
                if (enemyType == "Water" && Combat.Instance.WaterDragonDead == false)
                {
                    dragon = 23;
                }
                if (enemyType == "Dark" && Combat.Instance.DarkDragonDead == false)
                {
                    dragon = 24;
                }
                if (enemyType == "Metal" && Combat.Instance.MetalDragonDead == false)
                {
                    dragon = 25;
                }
                if (enemyType == "Fire" && Combat.Instance.FireDragonDead == false)
                {
                    dragon = 26;
                }
                if (enemyType == "Air" && Combat.Instance.AirDragonDead == false)
                {
                    dragon = 27;
                }

                if (dragon != -1)
                {
                    Enemies.Add(new Enemy(dragon, new Vector2(3100, 500)));
                }
                
            }
        }

        /// <summary>
        /// Method that kills the enemy threads in the zone and removes them from the list
        /// </summary>
        public void KillEnemiesInZone()
        {
            foreach (Enemy enemy in Enemies)
            {
                enemy.Alive = false;
            }
            Enemies = new List<Enemy>();
        }

        /// <summary>
        /// Method the checks which dragons are dead and makes the pillars colored.
        /// If all are dead create the win game npc
        /// </summary>
        public void ChangeDragonPillarSprite()
        {
            int dragonsDead = 0;
            if (Combat.Instance.FireDragonDead)
            {
                pillars[0].Sprite = GameWorld.ContentManager.Load<Texture2D>("pillars/FirePillar");
                dragonsDead++;
            }
            if (Combat.Instance.WaterDragonDead)
            {
                pillars[1].Sprite = GameWorld.ContentManager.Load<Texture2D>("pillars/WaterPillar");
                dragonsDead++;
            }
            if (Combat.Instance.MetalDragonDead)
            {
                pillars[2].Sprite = GameWorld.ContentManager.Load<Texture2D>("pillars/MetalPillar");
                dragonsDead++;
            }
            if (Combat.Instance.EarthDragonDead)
            {
                pillars[3].Sprite = GameWorld.ContentManager.Load<Texture2D>("pillars/EarthPillar");
                dragonsDead++;
            }
            if (Combat.Instance.DarkDragonDead)
            {
                pillars[4].Sprite = GameWorld.ContentManager.Load<Texture2D>("pillars/DarkPillar");
                dragonsDead++;
            }
            if (Combat.Instance.AirDragonDead)
            {
                pillars[5].Sprite = GameWorld.ContentManager.Load<Texture2D>("pillars/AirPillar");
                dragonsDead++;
            }
            if (Combat.Instance.NeutralDragonDead)
            {
                pillars[6].Sprite = GameWorld.ContentManager.Load<Texture2D>("pillars/NeutralPillar");
                dragonsDead++;
            }

            // if all dragons are dead, spawn the npc that says the game is won.
            if (dragonsDead == 7)
            {
                
                NPC wizard = new NPC("npc/npc_Wizard", new Vector2(2375, 900), false, false, false, "Congratulations. You have beaten the Game!");
                NPCs.Add(wizard);
                CollisionRects.Add(wizard.CollisionBox);
            }
        }
    }
}
