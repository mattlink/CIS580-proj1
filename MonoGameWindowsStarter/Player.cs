using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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

        const int FRAME_WIDTH = 32;
        const int FRAME_HEIGHT = 32;
        const int ANIMATION_FRAME_RATE = 124;
        int frame = 0;

        PlayerState pState = PlayerState.Up;
        SoundEffect eatSFX;


        TimeSpan timer;

        //KeyboardState oldKeyState;

        public Player(float x, float y, float r, float speed, Game1 game)
        {
            this.game = game;
            this.bounds = new BoundingRectangle(x, y, r, r);
            this.speed = speed;
            this.timer = new TimeSpan(0);
        }

        private float StateToFloat (PlayerState ps)
        {
            if (ps == PlayerState.Up) return (float)(Math.PI * 3) / 2.0f;
            if (ps == PlayerState.Down) return (float)(Math.PI / 2f);
            if (ps == PlayerState.Left) return (float)Math.PI;
            return 0.0f;
        }

        public void LoadContent(ContentManager cm)
        {
            this.texture = cm.Load<Texture2D>("pac2_sheet");
            //this.texture = cm.Load<Texture2D>("pac");
            this.eatSFX = cm.Load<SoundEffect>("coin_pickup");
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
                this.eatSFX.Play();
                this.game.RemoveDroplet(d);
            }


            timer += gameTime.ElapsedGameTime;

            while (timer.TotalMilliseconds > ANIMATION_FRAME_RATE)
            {
                frame++;
                timer -= new TimeSpan(0, 0, 0, 0, ANIMATION_FRAME_RATE);
            }

            frame %= 6;

            Console.WriteLine(frame);
        }

        public void Draw(SpriteBatch sb)
        {
            var source = new Rectangle(
                frame * FRAME_WIDTH,
                0,
                FRAME_WIDTH,
                FRAME_HEIGHT
                );
            var origin = new Vector2(32 / 2f, 32 / 2f);
            sb.Begin();
            sb.Draw(this.texture, this.bounds, source, Color.White, StateToFloat(this.pState), origin, SpriteEffects.None, 0f);

            //sb.Draw(texture, this.bounds, source, Color.White);
            //sb.Draw(this.texture, null, this.bounds, source, origin, StateToFloat(this.pState), new Vector2(2f, 2f), Color.White, SpriteEffects.None, 0f);
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
