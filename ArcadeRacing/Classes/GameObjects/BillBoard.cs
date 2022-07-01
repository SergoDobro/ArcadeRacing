using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcadeRacing.Classes.GameObjects
{
    class BillBoard : GameObject
    {
        public static Random random = new Random();
        public static List<Texture2D> BillBords = new List<Texture2D>();
        public static void LoadTexture(Texture2D texture2D)
        {
            BillBords.Add(texture2D);
        }
        public BillBoard()
        {
            objectWidth = 8;
            objectHeight = 16;
            GetX = (random.Next(0, 2) - 0.5f) * 2 * 8;
            texture = BillBords[1];//random.Next(0, BillBords.Count)];
        }
    }
}
