using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Threading;


namespace Warlock_The_Soulbinder
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameWorld : Game
    {
        private GraphicsDeviceManager graphics; 
        private SpriteBatch spriteBatch;
        public static double deltaTimeSecond;
        public static double deltaTimeMilli;
        public SpriteFont font;
        public SpriteFont copperFont;
        private Texture2D collisionTexture;
        public List<Enemy> enemies = new List<Enemy>();
        public Camera camera;
        private float delay;
        private string gameState = "Overworld";
        private string currentSaveFile = "1";
        public string CurrentSaveFile { get => currentSaveFile; set => currentSaveFile = value; }

        Song overworldMusic;
        Song combatMusic;
        private bool currentKeyH = true;
        private bool previousKeyH = true;
        TimeSpan songPosition;

        private float musicVolume;

        //Tiled fields
        private Zone town;
        private Zone beast;
        private Zone grass;
        private Zone water;
        private Zone dragon;
        private Zone metal;
        private Zone undead;
        private Zone fire;
        private Zone wind;
        public string currentZone = "Town";
        public List<Zone> zones = new List<Zone>();


        public int Gold { get; set; }
        public int SoulCount { get; set; }

        private static GameWorld instance;
        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameWorld();
                }
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        private static ContentManager content;


        public static ContentManager ContentManager
        {
            get
            {
                return content;
            }
        }

        /// <summary>
        /// Returns a rectangle whithin the bounds of the window
        /// </summary>
        public Rectangle ScreenSize
        {
            get
            {
                return graphics.GraphicsDevice.Viewport.Bounds;
            }
        }

        /// <summary>
        /// Returns a rectangle with the bounds of the current map
        /// </summary>
        public Rectangle TileMapBounds
        {
            get
            {
                return new Rectangle(0, 0, CurrentZone().Map.WidthInPixels, CurrentZone().Map.HeightInPixels);
            }
        }

        public string GameState
        {
            get
            {
                return gameState;
            }
            set
            {
                if (value == "Overworld" && gameState != "Dialogue" && gameState != "GeneralMenu")
                {
                    MediaPlayer.Play(overworldMusic, songPosition);
                }
                else if (value == "Combat")
                {
                    songPosition = MediaPlayer.PlayPosition; // save the overworld song playback position
                    MediaPlayer.Play(combatMusic, TimeSpan.Zero);
                }

                gameState = value;
            }
        }
        
        public float MusicVolume
        {
            get
            {
                return musicVolume;
            }
            set
            {
                musicVolume = value;
                MediaPlayer.Volume = musicVolume;
            }
        }
        public float SoundEffectVolume { get; set; } = 0.3f;

        public GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            content = Content;
            //Sets the window size
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1020;
#if !DEBUG
            graphics.IsFullScreen = true;
#endif
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            IsMouseVisible = true;

            Quest.Instance.OngoingQuests.Add(1, "Kill");
            Quest.Instance.QuestDescription.Add(1, "yippi kai yay"); //motherfucker

            town = new Zone("Town");
            beast = new Zone("Beast");
            grass = new Zone("Grass");
            dragon = new Zone("Dragon");
            wind = new Zone("Wind");
            fire = new Zone("Fire");
            water = new Zone("Water");
            undead = new Zone("Undead");
            metal = new Zone("Metal");
            zones.Add(town);
            zones.Add(beast);
            zones.Add(grass);
            zones.Add(dragon);
            zones.Add(wind);
            zones.Add(fire);
            zones.Add(water);
            zones.Add(undead);
            zones.Add(metal);

            foreach (var zone in zones)
            {
                zone.Setup();
            }

            camera = new Camera();

            IsMouseVisible = true;
            
            enemies.Add(new Enemy(0, new Vector2(1100, 150)));
            enemies.Add(new Enemy(4, new Vector2(1100, 300)));
            enemies.Add(new Enemy(7, new Vector2(1100, 450)));
            enemies.Add(new Enemy(12, new Vector2(1100, 600)));
            enemies.Add(new Enemy(16, new Vector2(1100, 750)));
            enemies.Add(new Enemy(20, new Vector2(1100, 900)));

            FilledStone.StoneList.Add(new FilledStone("sheep", RandomInt(1, 10)));
            FilledStone.StoneList.Add(new FilledStone("wolf", RandomInt(1, 10)));
            FilledStone.StoneList.Add(new FilledStone("bear", RandomInt(1, 10)));
            FilledStone.StoneList.Add(new FilledStone("plantEater", RandomInt(1, 10)));
            FilledStone.StoneList.Add(new FilledStone("insectSoldier", RandomInt(1, 10)));
            FilledStone.StoneList.Add(new FilledStone("slimeSnake", RandomInt(1, 10)));
            FilledStone.StoneList.Add(new FilledStone("tentacle", RandomInt(1, 10)));
            FilledStone.StoneList.Add(new FilledStone("frog", RandomInt(1, 10)));
            FilledStone.StoneList.Add(new FilledStone("fish", RandomInt(1, 10)));
            FilledStone.StoneList.Add(new FilledStone("mummy", RandomInt(1, 10)));
            FilledStone.StoneList.Add(new FilledStone("vampire", RandomInt(1, 10)));
            FilledStone.StoneList.Add(new FilledStone("banshee", RandomInt(1, 10)));
            FilledStone.StoneList.Add(new FilledStone("bucketMan", RandomInt(1, 10)));
            FilledStone.StoneList.Add(new FilledStone("defender", RandomInt(1, 10)));
            FilledStone.StoneList.Add(new FilledStone("sentry", RandomInt(1, 10)));
            FilledStone.StoneList.Add(new FilledStone("fireGolem", RandomInt(1, 10)));
            FilledStone.StoneList.Add(new FilledStone("infernalDemon", RandomInt(1, 10)));
            FilledStone.StoneList.Add(new FilledStone("ashZombie", RandomInt(1, 10)));
            FilledStone.StoneList.Add(new FilledStone("falcon", RandomInt(1, 10)));
            FilledStone.StoneList.Add(new FilledStone("bat", RandomInt(1, 10)));
            FilledStone.StoneList.Add(new FilledStone("raven", RandomInt(1, 10)));
            //Code to make pages for the filled stones
            FilledStone.StoneListPages = 0;
            int tempStoneList = FilledStone.StoneList.Count;
            for (int i = 0; i < 99; i++)
            {
                if (tempStoneList - 9 > 0)
                {
                    FilledStone.StoneListPages++;
                    tempStoneList -= 9;
                }
            }

            // Music
            MusicVolume = 0.5f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = MusicVolume;
            combatMusic = Content.Load<Song>("sound/combatMusicV2");
            overworldMusic = Content.Load<Song>("sound/overworldMusic");
            MediaPlayer.Play(overworldMusic);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            #if DEBUG
            collisionTexture = Content.Load<Texture2D>("CollisionTexture");
#endif

            

            #region load

            #endregion
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("font");
            copperFont = Content.Load<SpriteFont>("fontCopperplate");
            
            Combat.Instance.LoadContent(content);
            GeneralMenu.Instance.LoadContent(content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {

        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            deltaTimeSecond = gameTime.ElapsedGameTime.TotalSeconds;
            deltaTimeMilli = gameTime.ElapsedGameTime.Milliseconds;
            delay += gameTime.ElapsedGameTime.Milliseconds;

            InputHandler.Instance.Execute(); //gets keys pressed

            Player.Instance.Update(gameTime);
            Combat.Instance.Update(gameTime);

            //TEMPORARY
            #region
            if (Keyboard.GetState().IsKeyDown(Keys.E) && delay > 100)
            {
                FilledStone.StoneList.Add(new FilledStone("wolf", RandomInt(1, 10)));
                FilledStone.StoneList.Add(new FilledStone("fish", RandomInt(1, 10)));
                FilledStone.StoneList.Add(new FilledStone("infernalDemon", RandomInt(1, 10)));
                FilledStone.StoneList.Add(new FilledStone("defender", RandomInt(1, 10)));

                //Code to make pages for the filled stones
                FilledStone.StoneListPages = 0;
                int tempStoneList = FilledStone.StoneList.Count;
                for (int i = 0; i < 99; i++)
                {
                    if (tempStoneList - 9 > 0)
                    {
                        FilledStone.StoneListPages++;
                        tempStoneList -= 9;
                    }
                }
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D1) && delay > 100)
            {
                GameState = "Overworld";
                delay = 0;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D2) && delay > 100)
            {
                GameState = "Combat";
                delay = 0;
            }
            if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeyMenu) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonMenu)) && delay > 200)
            {
                if (GameState == "Overworld")
                {
                    GameState = "GeneralMenu";
                }
                
                else if (GameState == "GeneralMenu")
                {
                    GameState = "Overworld";
                }

                delay = 0;
            }

            #endregion

            //temporary save
            #region save
            previousKeyH = currentKeyH;
            currentKeyH = Keyboard.GetState().IsKeyUp(Keys.H);

            if (previousKeyH == false && currentKeyH == true)
            {
                Controller.Instance.OpenTheGates();

                Controller.Instance.DeleteConsumableDB();
                Controller.Instance.DeleteEnemyDB();
                Controller.Instance.DeletePlayerDB();
                Controller.Instance.DeleteQuestDB();
                Controller.Instance.DeleteSoulStoneDB();
                Controller.Instance.DeleteStatisticDB();

                for (int i = 0; i < Consumable.ConsumableList.Count; i++)
                {
                    Controller.Instance.SaveToConsumableDB(Consumable.ConsumableList[i].Name, Consumable.ConsumableList[i].Amount);
                }
                for (int i = 0; i < enemies.Count; i++)
                {
                    Controller.Instance.SaveToEnemyDB(enemies[i].Level, enemies[i].Position.X, enemies[i].Position.Y, enemies[i].Defense, enemies[i].Damage, enemies[i].MaxHealth, enemies[i].AttackSpeed, enemies[i].MetalResistance, enemies[i].EarthResistance, enemies[i].AirResistance, enemies[i].FireResistance, enemies[i].DarkResistance, enemies[i].WaterResistance, enemies[i].Monster);
                }
                for (int i = 0; i < FilledStone.StoneList.Count; i++)
                {
                    Controller.Instance.SaveToSoulStoneDB(FilledStone.StoneList[i].Monster, FilledStone.StoneList[i].Level);
                }
                //for (int i = 0; i < Quest.Instance.Quests.Count; i++)
                //{
                //    Controller.Instance.SaveToQuestDB(Quest.Instance.Quests[i],); // mangler en bedre måde at gemme quests
                //}
                int weapon, armour, skill1, skill2, skill3;
                try
                {
                    weapon = Equipment.Instance.Weapon.Id;
                }
                catch (Exception)
                {
                    weapon = -1;
                }
                try
                {
                    armour = Equipment.Instance.Armor.Id;
                }
                catch (Exception)
                {
                    armour = -1;
                }
                try
                {
                    skill1 = Equipment.Instance.Skill1.Id;
                }
                catch (Exception)
                {
                    skill1 = -1;
                }
                try
                {
                    skill2 = Equipment.Instance.Skill2.Id;
                }
                catch (Exception)
                {
                    skill2 = -1;
                }
                try
                {
                    skill3 = Equipment.Instance.Skill3.Id;
                }
                catch (Exception)
                {
                    skill3 = -1;
                }
               
                Controller.Instance.SaveToPlayerDB(Player.Instance.Position.X, Player.Instance.Position.Y, currentZone, weapon, armour, skill1, skill2, skill3);
                
                Controller.Instance.SaveToStatisticDB(Gold, SoulCount);

                Controller.Instance.CloseTheGates();
            }

            #endregion

            CurrentZone().Update(gameTime);

            camera.Position = Player.Instance.Position; //Makes the camera follow the player

            if (gameState == "Combat")
            {
                Combat.Instance.Update(gameTime);
            }
            else if (gameState == "GeneralMenu")
            {
                GeneralMenu.Instance.Update(gameTime);
            }

            Dialogue.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            if (GameState == "Overworld" || GameState == "Dialogue") //Overworld draw
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, camera.viewMatrix);

                CurrentZone().Draw(spriteBatch);
                
                foreach (Enemy enemy in enemies)
                {
                    enemy.Draw(spriteBatch);
                    DrawCollisionBox(enemy);
                }
                Player.Instance.Draw(spriteBatch);

                //collisionboxes
                #if DEBUG
                DrawCollisionBox(Player.Instance);
                #endif
                if (GameState == "Dialogue")
                {
                    Dialogue.Instance.Draw(spriteBatch);
                }

                spriteBatch.End();
                base.Draw(gameTime);
            }
            else if (GameState == "Combat") //Combat draw
            {
                spriteBatch.Begin();

                Combat.Instance.Draw(spriteBatch);

                spriteBatch.End();
            }
            else if (GameState == "GeneralMenu") //Menu draw
            {
                spriteBatch.Begin();

                GeneralMenu.Instance.Draw(spriteBatch);

                spriteBatch.End();
            }
        }

        /// <summary>
        /// Draw collision boxes around the GameObject 'go'
        /// </summary>
        /// <param name="go">A GameObject</param>
        private void DrawCollisionBox(GameObject go)
        {
            Rectangle collisionBox = go.CollisionBox;
            Rectangle topLine = new Rectangle(collisionBox.X, collisionBox.Y, collisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(collisionBox.X, collisionBox.Y + collisionBox.Height, collisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(collisionBox.X + collisionBox.Width, collisionBox.Y, 1, collisionBox.Height);
            Rectangle leftLine = new Rectangle(collisionBox.X, collisionBox.Y, 1, collisionBox.Height);

            spriteBatch.Draw(collisionTexture, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        /// <summary>
        /// Draw collision boxes for the Rectangle 'collisionBox'
        /// </summary>
        /// <param name="collisionBox">A rectangle</param>
        public void DrawRectangle(Rectangle collisionBox)
        {
            Rectangle topLine = new Rectangle(collisionBox.X, collisionBox.Y, collisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(collisionBox.X, collisionBox.Y + collisionBox.Height, collisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(collisionBox.X + collisionBox.Width, collisionBox.Y, 1, collisionBox.Height);
            Rectangle leftLine = new Rectangle(collisionBox.X, collisionBox.Y, 1, collisionBox.Height);

            spriteBatch.Draw(collisionTexture, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        /// <summary>
        /// Returns a random int within x and y
        /// </summary>
        /// <param name="x">Lower bounds</param>
        /// <param name="y">Upper bounds</param>
        /// <returns></returns>
        public int RandomInt(int x, int y)
        {
            Random rng = new Random();
            Thread.Sleep(10);
            return rng.Next(x, y);
        }

        /// <summary>
        /// Method that returns the currently loaded Zone
        /// </summary>
        /// <returns>The current Zone</returns>
        public Zone CurrentZone()
        {
            foreach (var zone in zones)
            {
                if (zone.Name == currentZone)
                {
                    return zone;
                }
            }
            return null;
        }

        /// <summary>
        /// Loads all the variables from the database
        /// </summary>
        private void LoadDB()
        {

        }

        /// <summary>
        /// Saves all the variables to the database
        /// </summary>
        private void SaveToDB()
        {

        }
    }
}
