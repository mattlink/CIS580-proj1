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

        public void Update(GameTime gameTime, List<Droplet> allDroplets)
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

            // check collisions with the other droplets
            foreach(Droplet d in allDroplets)
            {
                //if (this == d) continue;
                if (this.CollidesWith(d))
                {
                    // change this guys direction
                    this.speed *= -1;
                    // change d's direction
                    d.speed *= -1;

                    /*var delta = (this.bounds.X + this.bounds.Width) - (d.bounds.X - d.bounds.Width);

                    if (this.speed > 0) this.speed += 2 * delta;
                    if (this.speed < 0) this.speed -= 2 * delta;

                    if (d.speed > 0) d.speed += 2 * delta;
                    if (d.speed < 0) d.speed -= 2 * delta;*/

                    //d.speed += 2 * delta;
                }
            }

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Begin();

            // pull in droplet sphere content/image
            sb.Draw(this.texture, this.bounds, this.color);

            sb.End();
        }

        public bool CollidesWith(Droplet b)
        {
           if (
                ((b.bounds.X > this.bounds.X && b.bounds.X < this.bounds.X + this.bounds.Width) ||
                (b.bounds.X + b.bounds.Width > this.bounds.X && b.bounds.X + b.bounds.Width < this.bounds.X + this.bounds.Width))  
                &&
                ((this.bounds.Y + this.bounds.Height > b.bounds.Y && this.bounds.Y + this.bounds.Height < b.bounds.Y + b.bounds.Height) ||
                (this.bounds.Y < b.bounds.Y + b.bounds.Height && this.bounds.Y > b.bounds.Y))) {

                return true;
            }
            return false;
               
        }

    }
}
