using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace MonoGameWindowsStarter
{
    /*
     * We shall spawn in many droplets of varying radii and colors to fall from the sky
     * Avoid red ones, catch green ones?
     * */
    class Droplet
    {

        Game1 game;
        Texture2D texture;
        BoundingRectangle bounds;

        float speed;
        Color color;

        public Droplet(float x, float y, float r, float speed, Color color, Game1 game)
        {
            this.game = game;
          
            this.bounds = new BoundingRectangle(x, y, r, r);
            this.speed = speed;
            this.color = color;

        }

        public void LoadContent(ContentManager cm)
        {
            // pass in content manager
            texture = cm.Load<Texture2D>("pixel");
        }

        public void Update(GameTime gameTime)
        {
            //KeyboardState keyState = Keyboard.GetState();

            // move the droplet down the game window
            bounds.Y += (float)gameTime.ElapsedGameTime.TotalMilliseconds * speed;

            if (bounds.Y + bounds.Height > this.game.GraphicsDevice.Viewport.Height)
            {
                this.speed *= -1;
            }

            if (bounds.Y < 0)
            {
                this.speed *= -1;
            }

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Begin();

            // pull in droplet sphere content/image
            sb.Draw(this.texture, this.bounds, this.color);

            sb.End();
        }

    }
}
