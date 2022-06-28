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
        public void Render(GraphicsDevice graphicsDevice)
        {
            RenderSegments(graphicsDevice);
            RenderObjects();
        }

        private void RenderSegments(GraphicsDevice graphicsDevice)
        {
            for (int i = currentSegment; i < renderDistance + currentSegment && i < segments.Count; i++)
            {
                segments[i].RenderSegment(player.GetX, player.GetZ, graphicsDevice);
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
                if (car.GetZ - player.GetZ > 0)
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
                if (gameObject.GetZPos() - player.GetZ > 0)
                {
                    gameObject.Render(player.GetX, player.GetZ);
                }
            }
        }
    }
}
