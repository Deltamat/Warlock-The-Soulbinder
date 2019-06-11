using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        public Camera camera;
        private Texture2D fullScreen;
        private float delay;
        private string gameState = "MainMenu";
        private SpriteFont smallFont;
        private string currentSaveFile = "1";
        public string CurrentSaveFile { get => currentSaveFile; set => currentSaveFile = value; }
        private Random rng = new Random();
        public NumberFormatInfo replaceComma = new NumberFormatInfo();
        Song overworldMusic;
        Song combatMusic;
        Song dragonMusic;
        TimeSpan songPosition;
        private float musicVolume;
        private Texture2D background;
        private bool saved;
        private bool saving;
        private double savedTextTime;
        private Thread saveThread;

        //Tiled fields
        private Zone town, neutral, earth, water, dragon, metal, dark, fire, air, dragonRealm;
        public string currentZone = "Town";
        public List<Zone> zones = new List<Zone>();


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
                return Graphics.GraphicsDevice.Viewport.Bounds;
            }
        }

        /// <summary>
        /// Returns a rectangle with the bounds of the current tile map
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
                
                gameState = value;
                ChangeMusic();
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
        public SpriteFont SmallFont { get => smallFont; set => smallFont = value; }
        public SpriteBatch SpriteBatch { get => spriteBatch; set => spriteBatch = value; }
        public GraphicsDeviceManager Graphics { get => graphics; set => graphics = value; }
        public TimeSpan SongPosition { get => songPosition; set => songPosition = value; }
        public Song DragonMusic { get => dragonMusic; set => dragonMusic = value; }
        public Texture2D Background { get => background; set => background = value; }
        public bool Saved { get => saved; set => saved = value; }
        public double SavedTextTime { get => savedTextTime; set => savedTextTime = value; }
        public bool Saving { get => saving; set => saving = value; }

        public GameWorld()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            content = Content;
            //Sets the window size
            Graphics.PreferredBackBufferWidth = 1920;
            Graphics.PreferredBackBufferHeight = 1020;
#if !DEBUG
            Graphics.IsFullScreen = false;
#endif
            Graphics.ApplyChanges();
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
            SmallFont = Content.Load<SpriteFont>("smallFont");
            fullScreen = Content.Load<Texture2D>("fullScreen");
            
            replaceComma.NumberDecimalSeparator = ".";

            // zones are created with names and number of enemies
            town = new Zone("Town", 0);
            neutral = new Zone("Neutral", 9);
            earth = new Zone("Earth", 15);
            dragon = new Zone("Dragon", 0);
            air = new Zone("Air", 8);
            fire = new Zone("Fire", 10);
            water = new Zone("Water", 10);
            dark = new Zone("Dark", 7);
            metal = new Zone("Metal", 9);
            dragonRealm = new Zone("DragonRealm", 10);
            
            zones.Add(town);
            zones.Add(neutral);
            zones.Add(earth);
            zones.Add(dragon);
            zones.Add(air);
            zones.Add(fire);
            zones.Add(water);
            zones.Add(dark);
            zones.Add(metal);
            zones.Add(dragonRealm);

            foreach (Zone zone in zones)
            {
                zone.Setup();
            }

            camera = new Camera();

            IsMouseVisible = true;

#if DEBUG
            //adds five of all enemy types as stones to the player's inventory - DEBUG ONLY
            #region tempStonesAdd
            if (FilledStone.StoneList.Count == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    FilledStone.StoneList.Add(new FilledStone(new Enemy(0, Vector2.Zero)));
                    FilledStone.StoneList.Add(new FilledStone(new Enemy(1, Vector2.Zero)));
                    FilledStone.StoneList.Add(new FilledStone(new Enemy(2, Vector2.Zero)));
                    FilledStone.StoneList.Add(new FilledStone(new Enemy(3, Vector2.Zero)));
                    FilledStone.StoneList.Add(new FilledStone(new Enemy(4, Vector2.Zero)));
                    FilledStone.StoneList.Add(new FilledStone(new Enemy(5, Vector2.Zero)));
                    FilledStone.StoneList.Add(new FilledStone(new Enemy(6, Vector2.Zero)));
                    FilledStone.StoneList.Add(new FilledStone(new Enemy(7, Vector2.Zero)));
                    FilledStone.StoneList.Add(new FilledStone(new Enemy(8, Vector2.Zero)));
                    FilledStone.StoneList.Add(new FilledStone(new Enemy(9, Vector2.Zero)));
                    FilledStone.StoneList.Add(new FilledStone(new Enemy(10, Vector2.Zero)));
                    FilledStone.StoneList.Add(new FilledStone(new Enemy(11, Vector2.Zero)));
                    FilledStone.StoneList.Add(new FilledStone(new Enemy(12, Vector2.Zero)));
                    FilledStone.StoneList.Add(new FilledStone(new Enemy(13, Vector2.Zero)));
                    FilledStone.StoneList.Add(new FilledStone(new Enemy(14, Vector2.Zero)));
                    FilledStone.StoneList.Add(new FilledStone(new Enemy(15, Vector2.Zero)));
                    FilledStone.StoneList.Add(new FilledStone(new Enemy(16, Vector2.Zero)));
                    FilledStone.StoneList.Add(new FilledStone(new Enemy(17, Vector2.Zero)));
                    FilledStone.StoneList.Add(new FilledStone(new Enemy(18, Vector2.Zero)));
                    FilledStone.StoneList.Add(new FilledStone(new Enemy(19, Vector2.Zero)));
                    FilledStone.StoneList.Add(new FilledStone(new Enemy(20, Vector2.Zero)));
                }
            }
            #endregion
#endif

            //LogLoad
            Log.Instance.GenerateLogList();
            Log.Instance.CalculateBonus();
            // Music
            MusicVolume = 0.04f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = MusicVolume;
            combatMusic = Content.Load<Song>("sound/combatMusic");
            DragonMusic = Content.Load<Song>("sound/dragonMusic");
            overworldMusic = Content.Load<Song>("sound/overworldMusic");
            MediaPlayer.Play(overworldMusic);

            Equipment.Instance.UpdateExperienceRequired();
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

            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            Background = Content.Load<Texture2D>("forest_background");
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
            deltaTimeSecond = gameTime.ElapsedGameTime.TotalSeconds;
            deltaTimeMilli = gameTime.ElapsedGameTime.Milliseconds;
            delay += gameTime.ElapsedGameTime.Milliseconds;

            InputHandler.Instance.Execute(); //gets keys pressed

            if (GameState == "MainMenu")
            {
                MainMenu.Instance.Update(gameTime);
            }

            Player.Instance.Update(gameTime);

#if DEBUG
            //TEMPORARY
            #region TEMP
            if (Keyboard.GetState().IsKeyDown(Keys.T) && delay > 100)
            {

            }

            if (Keyboard.GetState().IsKeyDown(Keys.U) && delay > 100)
            {
                foreach (FilledStone stone in Equipment.Instance.EquippedEquipment)
                {
                    if (stone != null)
                    {
                        stone.Level = 20;
                    }
                }

                Player.Instance.CurrentHealth = Player.Instance.MaxHealth;
            }

            #endregion
#endif

            
            if (Saved) //timer for how long the "Saved" text should be in the top-left of the screen
            {
                SavedTextTime += deltaTimeMilli;
            }

            if ((InputHandler.Instance.KeyPressed(InputHandler.Instance.KeyMenu) || InputHandler.Instance.ButtonPressed(InputHandler.Instance.ButtonMenu)) && delay > 200)
            {
                if (GameState == "Overworld")
                {
                    Sound.PlaySound("sound/menuSounds/openMenu");
                    GeneralMenu.Instance.SelectedInt = 0;
                    GeneralMenu.Instance.InventoryState = "GeneralMenu";
                    GameState = "GeneralMenu";
                }

                else if (GameState == "GeneralMenu")
                {
                    Sound.PlaySound("sound/menuSounds/closeMenu");
                    GameState = "Overworld";
                }

                delay = 0;
            }

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
            GraphicsDevice.Clear(Color.Black);

            if (GameState == "MainMenu")
            {
                SpriteBatch.Begin();
                MainMenu.Instance.Draw(SpriteBatch);
                SpriteBatch.End();
            }

            if (GameState == "Overworld" || GameState == "Dialogue") //Overworld draw
            {
                SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, camera.ViewMatrix);
                
                CurrentZone().Draw(SpriteBatch);

                foreach (var layer in CurrentZone().Map.TileLayers)
                {
                    if (layer.Name != "Top" || layer.Name != "OverTop")
                    {
                        CurrentZone().MapRenderer.Draw(layer, camera.ViewMatrix, null, null, 0.99f);
                    }
                }

#if DEBUG
                SpriteBatch.DrawString(font, $"{Player.Instance.Position}", Player.Instance.Position, Color.Red); // for npc placement
#endif

                Player.Instance.Draw(SpriteBatch);

                //collisionboxes
#if DEBUG
                DrawCollisionBox(Player.Instance);
#endif

                if (GameState == "Dialogue")
                {
                    Dialogue.Instance.Draw(SpriteBatch);
                }

                SpriteBatch.End();
                base.Draw(gameTime);
            }
            else if (GameState == "Combat") //Combat draw
            {
                SpriteBatch.Begin();

                Combat.Instance.Draw(SpriteBatch);

                SpriteBatch.End();
            }
            else if (GameState == "GeneralMenu") //Menu draw
            {
                SpriteBatch.Begin();

                GeneralMenu.Instance.Draw(SpriteBatch);
                
                SpriteBatch.End();
                
            }

            if (GameState == "Overworld")
            {
                SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, camera.ViewMatrix);
                foreach (var layer in CurrentZone().Map.TileLayers)
                {
                    if (layer.Name == "Top" || layer.Name == "OverTop")
                    {
                        CurrentZone().MapRenderer.Draw(layer, camera.ViewMatrix, null, null, 0.99f);
                    }
                }
                SpriteBatch.End();
            }

            //draws "Saved" in the top-left of the screen
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);
            if (SavedTextTime > 0 && !saving)
            {
                spriteBatch.DrawString(copperFont, "Saved!", Vector2.Zero, Color.HotPink, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            }

            if (SavedTextTime > 2500)
            {
                Saved = false;
                SavedTextTime = 0;
            }
            spriteBatch.End();

            //draws "Saving..." in the top-left of the screen
            if (saving)
            {
                SpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, null);
                SpriteBatch.DrawString(copperFont, "Saving...", Vector2.Zero, Color.DeepPink);
                SpriteBatch.End();
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

            SpriteBatch.Draw(collisionTexture, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 0);
            SpriteBatch.Draw(collisionTexture, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 0);
            SpriteBatch.Draw(collisionTexture, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 0);
            SpriteBatch.Draw(collisionTexture, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 0);
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

            SpriteBatch.Draw(collisionTexture, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 0);
            SpriteBatch.Draw(collisionTexture, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 0);
            SpriteBatch.Draw(collisionTexture, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 0);
            SpriteBatch.Draw(collisionTexture, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Returns a random int within x and y, y excluded
        /// </summary>
        /// <param name="x">Lower bounds</param>
        /// <param name="y">Upper bounds</param>
        /// <returns></returns>
        public int RandomInt(int x, int y)
        {
            return rng.Next(x, y);
        }

        /// <summary>
        /// Method that returns the currently loaded Zone
        /// </summary>
        /// <returns>The current Zone</returns>
        public Zone CurrentZone()
        {
            foreach (Zone zone in zones)
            {
                if (zone.Name == currentZone)
                {

                    return zone;
                }
            }
            return zones[0];
        }

        public void ChangeMusic()
        {
            SongPosition = MediaPlayer.PlayPosition; // save the overworld song playback position

            if ((currentZone == "DragonRealm") && gameState != "Dialogue" && gameState != "GeneralMenu")
            {
                MediaPlayer.Play(DragonMusic, SongPosition);
            }
            else if (GameState == "Overworld" && gameState != "Dialogue" && gameState != "GeneralMenu")
            {
                MediaPlayer.Play(overworldMusic, SongPosition);
            }
            else if (GameState == "Combat" && currentZone != "DragonRealm")
            {
                SongPosition = MediaPlayer.PlayPosition; // save the overworld song playback position
                MediaPlayer.Play(combatMusic, TimeSpan.Zero);
            }
        }

        /// <summary>
        /// Loads everything from the database depending on which savefile was chosen in MainMenu.
        /// </summary>
        public void LoadDB()
        {
            Controller.Instance.OpenTheGates();

            FilledStone.StoneList = Controller.Instance.LoadFromSoulStoneDB();
            Controller.Instance.LoadFromPlayerDB();
            Controller.Instance.LoadFromLogDB();
            Player.Instance.GraceStart = false;
            Player.Instance.GracePeriod = 0;
            
            CurrentZone().Enemies = Controller.Instance.LoadFromEnemyDB();
            Controller.Instance.LoadFromStatisticDB();
            dragon.ChangeDragonPillarSprite(); // updates the pillars so they match the dead dragons

            Equipment.Instance.LoadEquipment();
            Equipment.Instance.UpdateExperienceRequired();

            Controller.Instance.CloseTheGates();
        }

        /// <summary>
        /// Starts a thread to save the game
        /// </summary>
        public void SaveToDBThreadMaker()
        {
            if (!saving)
            {
                saving = true;
                saveThread = new Thread(() => SaveToDB());
                saveThread.Start();
            }
        }

        /// <summary>
        /// Saves all the necesary variables to the database
        /// </summary>
        public void SaveToDB()
        {
            Controller.Instance.OpenTheGates();

            Controller.Instance.DeleteEnemyDB();
            Controller.Instance.DeletePlayerDB();
            Controller.Instance.DeleteLogDB();
            Controller.Instance.DeleteSoulStoneDB();
            Controller.Instance.DeleteStatisticDB();

            //Enemies
            for (int i = 0; i < CurrentZone().Enemies.Count; i++)
            {
                Controller.Instance.SaveToEnemyDB(CurrentZone().Enemies[i].Level, CurrentZone().Enemies[i].Position.X, CurrentZone().Enemies[i].Position.Y, CurrentZone().Enemies[i].Defense, CurrentZone().Enemies[i].Damage, CurrentZone().Enemies[i].MaxHealth, CurrentZone().Enemies[i].AttackSpeed, CurrentZone().Enemies[i].MetalResistance, CurrentZone().Enemies[i].EarthResistance, CurrentZone().Enemies[i].AirResistance, CurrentZone().Enemies[i].FireResistance, CurrentZone().Enemies[i].DarkResistance, CurrentZone().Enemies[i].WaterResistance, CurrentZone().Enemies[i].Monster);
            }
            //Filled soul stones
            for (int i = 0; i < FilledStone.StoneList.Count; i++)
            {
                Controller.Instance.SaveToSoulStoneDB(FilledStone.StoneList[i].Monster, FilledStone.StoneList[i].Experience, FilledStone.StoneList[i].EquipmentSlot, FilledStone.StoneList[i].Level, FilledStone.StoneList[i].Damage, FilledStone.StoneList[i].MaxHealth, FilledStone.StoneList[i].AttackSpeed);
            }
            //Player
            Controller.Instance.SaveToPlayerDB(Player.Instance.Position.X, Player.Instance.Position.Y, currentZone, Player.Instance.CurrentHealth);

            //Which dragons are dead
            Controller.Instance.SaveToStatisticDB(Combat.Instance.EarthDragonDead, Combat.Instance.FireDragonDead, Combat.Instance.DarkDragonDead, Combat.Instance.MetalDragonDead, Combat.Instance.WaterDragonDead, Combat.Instance.AirDragonDead, Combat.Instance.NeutralDragonDead);
            //Log for scanned enemies
            Controller.Instance.SaveToLogDB(Log.Instance.SheepLog, Log.Instance.WolfLog, Log.Instance.BearLog, Log.Instance.PlantEaterLog, Log.Instance.InsectSoldierLog, Log.Instance.SlimeSnakeLog, Log.Instance.TentacleLog, Log.Instance.FrogLog, Log.Instance.FishLog, Log.Instance.MummyLog, Log.Instance.VampireLog, Log.Instance.BansheeLog, Log.Instance.BucketManLog, Log.Instance.DefenderLog, Log.Instance.SentryLog, Log.Instance.FireGolemLog, Log.Instance.InfernalDemonLog, Log.Instance.AshZombieLog, Log.Instance.FalconLog, Log.Instance.BatLog, Log.Instance.RavenLog);
            
            Controller.Instance.CloseTheGates();

            Saved = true;
            Saving = false;
        }
    }
}