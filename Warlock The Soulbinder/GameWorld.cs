using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Graphics;

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
        private Texture2D collisionTexture;
        public List<Enemy> enemies = new List<Enemy>();
        public Camera camera;
        private float delay;
        private string gameState = "Overworld";

        //Tiled fields
        private Zone t;
        private Zone t2;
        public string currentZone = "t";
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
        /// Returns a rectangle whitin the bounds of the map
        /// </summary>
        public Rectangle TileMapBounds
        {
            get
            {
                return new Rectangle(0, 0, CurrentZone().Map.WidthInPixels, CurrentZone().Map.HeightInPixels);
            }
        }

        public string GameState { get => gameState; set => gameState = value; }

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
            t = new Zone("t");
            t2 = new Zone("t2");

            zones.Add(t);
            zones.Add(t2);

            foreach (var zone in zones)
            {
                zone.Setup();
            }

            camera = new Camera();

            IsMouseVisible = true;
            
            enemies.Add(new Enemy(0, new Vector2(1100, 100)));
            enemies.Add(new Enemy(4, new Vector2(1100, 250)));
            enemies.Add(new Enemy(7, new Vector2(1100, 400)));
            enemies.Add(new Enemy(12, new Vector2(1100, 550)));
            enemies.Add(new Enemy(16, new Vector2(1100, 700)));
            enemies.Add(new Enemy(20, new Vector2(1100, 850)));

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
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("font");
            
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
                FilledStone.StoneList.Add(new FilledStone("wolf", "wolf", RandomInt(1,10)));

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
                gameState = "Overworld";
                delay = 0;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D2) && delay > 100)
            {
                gameState = "Combat";
                delay = 0;
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

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
                        
            if (GameState == "Overworld") //Overworld draw
            {
                spriteBatch.Begin(SpriteSortMode.FrontToBack, null, SamplerState.PointClamp, null, null, null, camera.viewMatrix);

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
