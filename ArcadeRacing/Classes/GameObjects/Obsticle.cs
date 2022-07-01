using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcadeRacing.Classes.GameObjects
{
    class Obsticle : GameObject
    {
        public static List<Texture2D> obsticles = new List<Texture2D>();
        public static void LoadTexture(ContentManager content)
        {
            obsticles.Add(content.Load<Texture2D>("obsticle_texture"));
        }
        public Obsticle(float z, int pos = 0)
        {
            GetZ = z;
            GetX =  (pos - 0.5f) * 2 * (objectWidth+8);
            objectWidth = 1;
            objectHeight = 2;
            texture = obsticles[random.Next(0, obsticles.Count)];
        }
    }
}