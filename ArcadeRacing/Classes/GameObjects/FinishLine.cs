using ArcadeRacing.Classes.Cars;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcadeRacing.Classes.GameObjects
{
    class FinishLine : GameObject
    {
        public override void LoadContent(ContentManager content)
        {
            texture = (content.Load<Texture2D>("Finish"));
        }

        public FinishLine(float z, int pos = 0)
        {
            pos_z = z;
            pos_x = 0;
            objectWidth = 30; //10 = road
            objectHeight = 10;
        }
        //public override bool IsIntersecting(Car car)
        //{
        //    var res = base.IsIntersecting(car);
        //    if (res)
        //        car.FinishedTrack();
        //    return res;
        //}
    }
}
