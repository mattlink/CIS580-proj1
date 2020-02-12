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

    enum PlayerState
    {
        Up, Down, Left, Right
    }

    class Player
    {
        Game1 game;
        Texture2D texture;
        BoundingRectangle bounds;

        float speed;

        PlayerState pState = PlayerState.Up;

        //KeyboardState oldKeyState;

        public Player(float x, float y, float r, float speed, Game1 game)
        {
            this.game = game;
            this.bounds = new BoundingRectangle(x, y, r, r);
            this.speed = speed;
        }

        private float StateToFloat (PlayerState ps)
        {
            if (ps == PlayerState.Up) return -90.0f;
            if (ps == PlayerState.Down) return 90.0f;
            if (ps == PlayerState.Right) return 180.0f;
            return 0.0f;
        }

        public void LoadContent(ContentManager cm)
        {
            this.texture = cm.Load<Texture2D>("pixel");
        }

        public void Update(GameTime gameTime, List<Droplet> droplets)
        {
            // update player position (based on arrow key input)
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Up))
            {
                this.bounds.Y -= speed;
                pState = PlayerState.Up;
            } 

            if (keyState.IsKeyDown(Keys.Down))
            {
                this.bounds.Y += speed;
                pState = PlayerState.Down;
            }

            if (keyState.IsKeyDown(Keys.Left))
            {
                this.bounds.X -= speed;
                pState = PlayerState.Left;
            }

            if (keyState.IsKeyDown(Keys.Right))
            {
                this.bounds.X += speed;
                pState = PlayerState.Right;
            }


            // update texture rotation state


            // check for if a droplet is consumed after this position update (collision check)
            List<Droplet> toRemove = new List<Droplet>();

            foreach(Droplet d in droplets)
            {
                if (this.CollidesWith(d))
                {
                    this.game.dropletsSwallowed++;
                    //this.game.RemoveDroplet(d);
                    toRemove.Add(d);
                }
            }

            foreach (Droplet d in toRemove)
            {
                this.game.RemoveDroplet(d);
            }
        }

        public void Draw(SpriteBatch sb)
        {
            var origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
            sb.Begin();
            sb.Draw(this.texture, this.bounds, null, Color.DarkOrange, StateToFloat(this.pState), origin, SpriteEffects.None, 0f);
            sb.End();
        }

        public bool CollidesWith(Droplet b)
        {
            if (
                ((b.bounds.X > this.bounds.X && b.bounds.X < this.bounds.X + this.bounds.Width) ||
                (b.bounds.X + b.bounds.Width > this.bounds.X && b.bounds.X + b.bounds.Width < this.bounds.X + this.bounds.Width))
                &&
                ((this.bounds.Y + this.bounds.Height > b.bounds.Y && this.bounds.Y + this.bounds.Height < b.bounds.Y + b.bounds.Height) ||
                (this.bounds.Y < b.bounds.Y + b.bounds.Height && this.bounds.Y > b.bounds.Y)))
            {

                return true;
            }
            return false;
        }
    }
}
