using ArcadeRacing.Classes.Cars;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcadeRacing.Classes.GameObjects
{
    class BillBoard : GameObject
    {
        public static List<Texture2D> BillBords = new List<Texture2D>();
        public static void LoadTexture(ContentManager content)
        {
            BillBords.Add(content.Load<Texture2D>("banner"));
            BillBords.Add(content.Load<Texture2D>("pts"));
            BillBords.Add(content.Load<Texture2D>("pt"));
        }


        public BillBoard(float z)
        {
            objectWidth = 16;
            objectHeight = 16;
            pos_z = z;
            pos_x= (random.Next(0, 2) - 0.5f) * 2 *( objectWidth +2 );
            texture = BillBords[random.Next(0, BillBords.Count)];
        }
    }
}
