using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumping_Stuff
{
    class Sprite
    {
        public Vector2 Position { get; set; }
        public Color Color { get; set; }
        public Texture2D Texture { get; set; }
        public float X
        {
            get
            {
                return Position.X;
            }
            set
            {
                Position = new Vector2(value, Position.Y);
            }
        }
        public float Y
        {
            get
            {
                return Position.Y;
            }
            set
            {
                Position = new Vector2(Position.X, value);
            }
        }
        public SpriteEffects Effects { get; set; }
        public Vector2 Scale { get; set; }
        public float Width
        {
            get
            {
                return Hitbox.Width;
            }
        }
        public float Length
        {
            get
            {
                return Hitbox.Height;
            }
        }
        public virtual Rectangle? SourceRectangle { get;}
        public Rectangle Hitbox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)(Texture.Width * Scale.X), (int)(Texture.Height * Scale.Y));
            }
        }
        public Sprite(Texture2D texture) : this(texture, Color.White, Vector2.Zero, Vector2.One)
        {
           
        }
        public Sprite(Texture2D Texture, Color Color, Vector2 Position, Vector2 Scale)
        {
            this.Texture = Texture;
            this.Color = Color;
            this.Position = Position;
            this.Scale = Scale;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, SourceRectangle,Color,0,Vector2.Zero,Scale,Effects,0);
         
        }
    }
}
