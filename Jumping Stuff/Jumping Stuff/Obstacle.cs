using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumping_Stuff
{
    class Obstacle : Sprite
    {
        Texture2D pixel;
        public Line Top
        {
            get
            {
                return new Line(new Vector2(Position.X,Position.Y), new Vector2(Position.X + Hitbox.Width, Position.Y), pixel);
            }

        }
        public Line Left
        {
            get
            {
                return new Line(new Vector2(Position.X, Position.Y), new Vector2(Position.X, Position.Y + Hitbox.Height), pixel);
            }
        }
        public Line Bottom
        {
            get
            {
                return new Line(new Vector2(Position.X, Position.Y + Hitbox.Height), new Vector2(Position.X + Hitbox.Width, Position.Y + Hitbox.Height), pixel);
            }
        }
        public Line Right
        {
            get
            {
                return new Line(new Vector2(Position.X + Hitbox.Width, Position.Y + Hitbox.Height), new Vector2(Position.X + Hitbox.Width, Position.Y), pixel);
            }
        }

        public Obstacle(Texture2D texture, Color color,Vector2 position, Vector2 scale, Texture2D pixel)
            :base(texture,color,position,scale)
        {
            this.pixel = pixel;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            //Top.DrawLine(spriteBatch);
            //Left.DrawLine(spriteBatch);
            //Right.DrawLine(spriteBatch);
            //Bottom.DrawLine(spriteBatch);
        }
    }
}
