using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

        public float X;
        public float Y;

        public float Radius;

        // TODO: BoundingSphere???

        public Droplet(float x, float y, float r)
        {
            this.X = x;
            this.Y = y;
            this.Radius = r;
        }

        public void LoadContent()
        {
            // pass in content manager
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch sb)
        {
            sb.Begin();

            // pull in droplet sphere content/image

            sb.End();
        }

    }
}
