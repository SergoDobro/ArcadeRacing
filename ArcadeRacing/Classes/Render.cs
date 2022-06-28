using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadeRacing.Classes
{
    partial class MainGameClass
    {
        public GraphicsDevice graphicsDevice;
        public void Render(GraphicsDevice graphicsDevice)
        {
            RenderSegments(graphicsDevice);
            RenderObjects();
        }

        private void RenderSegments(GraphicsDevice graphicsDevice)
        {
            float curviture = 0;//segments[0].curveture * (1-(player.GetZ - prev));
            //segments[1].RenderSegment(player.GetX, player.GetZ, graphicsDevice, segments[0].curveture, out c);
            for (int i = 2; i < renderDistance + currentSegment && i < segments.Count; i++)
            {
                segments[i].RenderSegment(player.GetX, player.GetZ, graphicsDevice, curviture, out curviture);
            }
        }
        private void RenderPlayer()
        {
            player.RenderPlayer();
        }
        private void RenderEnemyCars()
        {
            var carsOrdered = cars.OrderBy(x=>x.GetZ);
            foreach (var car in carsOrdered)
            {
                if (car.GetZ - player.GetZ > 1)
                {
                    car.Render(player.GetX, player.GetZ);
                }
            }
        }
        private void RenderObjects()
        {
            //var gameObjectsByDistance = gameObjects.OrderBy(x=>x.GetZPos());
            foreach (var gameObject in gameObjects)
            {
                if (gameObject.GetZPos() - player.GetZ > 1)
                {
                    gameObject.Render(player.GetX, player.GetZ);
                }
            }
        }
    }
}
