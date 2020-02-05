﻿using Microsoft.Xna.Framework;
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

        List<Droplet> droplets;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            droplets = new List<Droplet>();
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
            var rand = new Random();
            int numDroplets = 12;
            Color[] colors = { Color.Yellow, Color.Green, Color.Red, Color.Blue, Color.Orange, Color.DarkOrange, Color.DarkOliveGreen, Color.OliveDrab };

            for (int i = 0; i < numDroplets; i++)
            {
                float randX = (float)rand.Next(this.graphics.PreferredBackBufferWidth);
                float randY = (float)rand.Next(60);
                float randSize = (float)rand.Next(15, 69);
                //float randSpeed = rand.Next(1, 2);
                //float randSpeed = 0.5f;
                float randSpeed = (float)(rand.NextDouble()) * (1.2f - 0.3f) + 0.3f;
                int randColor = rand.Next(colors.Length);

                droplets.Add(
                    new Droplet(randX, randY, randSize, randSpeed, colors[randColor], this)
                );
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

            // TODO: Add your update logic here
            foreach(Droplet d in this.droplets)
            {
                d.Update(gameTime);
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

            // TODO: Add your drawing code here
            foreach (Droplet d in this.droplets)
            {
                d.Draw(this.spriteBatch);
            }

            base.Draw(gameTime);
        }
    }
}
