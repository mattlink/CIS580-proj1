using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace MonoGameWindowsStarter
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        List<Droplet> droplets;
        Player player;

        KeyboardState oldKeyState;

        public int dropletsSwallowed = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            droplets = new List<Droplet>();
            player = new Player(12, 12, 50, 5, this);
        }

        private Droplet CreateRandomDroplet(Random rand)
        {
            //var rand = new Random();
            Color[] colors = { Color.DarkSlateBlue, Color.MidnightBlue, Color.DarkBlue, Color.DarkRed, Color.DarkSeaGreen, Color.ForestGreen, Color.DarkBlue, Color.DarkOliveGreen, Color.OliveDrab };

            //Color[] colors = { Color.Sienna };

            float randX = (float)rand.Next(this.GraphicsDevice.Viewport.Width);
            Console.WriteLine(randX);
            Console.WriteLine(this.GraphicsDevice.Viewport.Width);
            Console.WriteLine(this.graphics.PreferredBackBufferWidth);
            float randY = (float)rand.Next(this.GraphicsDevice.Viewport.Height / 2);
            float randSize = (float)rand.Next(15, 30);
            //float randSpeed = rand.Next(1, 2);
            //float randSpeed = 0.5f;
            float randSpeed = (float)(rand.NextDouble()) * (1.2f - 0.3f) + 0.3f;
            int randColor = rand.Next(colors.Length);

            Droplet drop = new Droplet(randX, randY, randSize, randSpeed * 0.5f, colors[randColor], this);

            drop.LoadContent(this.Content);

            return drop;
        }

        public void RemoveDroplet(Droplet d)
        {
            this.droplets.Remove(d);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // set the screen size
            graphics.PreferredBackBufferWidth = 1042;
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();

           
            // load in some random droplets
            int numDroplets = 4;
            Random rand = new Random();
            
            for (int i = 0; i < numDroplets; i++)
            {
                droplets.Add(CreateRandomDroplet(rand));
            }


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = Content.Load<SpriteFont>("defaultFont");

            player.LoadContent(Content);

            // TODO: use this.Content to load your game content here
            foreach (Droplet d in this.droplets)
            {
                d.LoadContent(this.Content);
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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

            // TODO: If user presses spacebar: add another random droplet
            // TODO: If user presses backspace: delete a random droplet

            KeyboardState keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Space) && !oldKeyState.IsKeyDown(Keys.Space))
            {
                // add a new random droplet
                //droplets.Add(CreateRandomDroplet((new Random()).Next()));
                droplets.Add(CreateRandomDroplet(new Random()));
            }

            if (keyState.IsKeyDown(Keys.Delete) && !oldKeyState.IsKeyDown(Keys.Delete))
            {
                // remove a random droplet
                if (droplets.Count > 0)
                {
                    droplets.Remove(droplets[(new Random()).Next(droplets.Count)]);
                }
                
            }

           

            foreach(Droplet d in this.droplets)
            {
                d.Update(gameTime, this.droplets);
            }

            player.Update(gameTime, this.droplets);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            this.GraphicsDevice.Clear(Color.FloralWhite);

            spriteBatch.Begin();
            spriteBatch.DrawString(
                spriteFont,
                "Droplets Consumed: " + this.dropletsSwallowed,
                new Vector2(50, 50),
                Color.Black
                );
            spriteBatch.End();

            player.Draw(this.spriteBatch);

            // TODO: Add your drawing code here
            foreach (Droplet d in this.droplets)
            {
                d.Draw(this.spriteBatch);
            }

            base.Draw(gameTime);
        }
    }
}
