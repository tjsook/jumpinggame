using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jumping_Stuff
{
    class Line
    {
        public Vector2 StartPoint;
        public Vector2 EndPoint;
        Texture2D Texture;
        float Rotation;
        float Distance;

        public Line(Vector2 startPoint, Vector2 endPoint, Texture2D texture)
        {
            Texture = texture;
            StartPoint = startPoint;
            EndPoint = endPoint;
            Rotation = (float)Math.Atan2(EndPoint.Y - StartPoint.Y, EndPoint.X - StartPoint.X);
            Distance = Vector2.Distance(EndPoint,StartPoint);

        }
        public void DrawLine(SpriteBatch sprite)
        {
            sprite.Draw(Texture, StartPoint, null, Color.Red, Rotation, Vector2.Zero, new Vector2(Distance, 5), SpriteEffects.None, 0f);
         
        }



        //calculate Theta/Angle/Rotation of line
        //calculate distance between points
        
        //draw function for debugging
    }
}
