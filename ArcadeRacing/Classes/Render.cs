using ArcadeRacing.Classes.GameObjects;
using Microsoft.Xna.Framework;
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
        public const int SegentDistructorMult = 4;
        public GraphicsDevice graphicsDevice;

        public void LoadContent(GraphicsDevice graphicsDevice, Microsoft.Xna.Framework.Content.ContentManager content)
        {
            BillBoard.LoadTexture(content.Load<Texture2D>("banner"));
            BillBoard.LoadTexture(content.Load<Texture2D>("pts"));

            GlobalRenderSettings.LoadGlobalRenderSettings(graphicsDevice);
            Segment.LoadSegmentation(graphicsDevice);
            GameObject.LoadRendering(graphicsDevice);


            int z = 0;
            for (int i = 0; i < 100; i++)
            {
                z += random.Next(10,100);
                AddGameOblects(z);
            }
        }
        public void Render(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            RenderSegments(graphicsDevice);
            RenderObjects(spriteBatch);
        }

        private void RenderSegments(GraphicsDevice graphicsDevice)
        {
            float curviture = 0;//segments[0].curveture * (1-(player.GetZ - prev));
            //segments[1].RenderSegment(player.GetX, player.GetZ, graphicsDevice, segments[0].curveture, out c);
            for (int i = 0; i < renderDistance + currentSegment && i < segments.Count; i++)
            {
                if (segments[i].z - player.GetZ > Segment.segmentLength * SegentDistructorMult)
                {
                    segments[i].RenderSegment(player.GetX * 5, player.GetZ, graphicsDevice, curviture, out curviture);
                }
                else
                {
                    //curviture = segments[i].curveture * (player.GetZ - (player.GetZ * 10) % 10);
                }
                //System.Text.Json.
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
        private void RenderObjects(SpriteBatch spriteBatch)
        {

            

            float cz = 0;
            int ind = 0;
            float curviture = 0;

            for (int i = 0; i < renderDistance + currentSegment && i < segments.Count; i++)
            {
                if (segments[i].z - player.GetZ > Segment.segmentLength * SegentDistructorMult)
                {
                    ind = i;
                    float inter = (player.GetZ - ((player.GetZ * 10) % 10));
                    //curviture = segments[i].curveture * inter + (1- inter)* segments[i+1].curveture;
                    break;
                }
            }

            foreach (var gameObject in gameObjects)
            {
                if (gameObject.GetZ - player.GetZ > 0 && gameObject.GetZ - player.GetZ<segments.Count * Segment.segmentLength-5)
                {
                    while (gameObject.GetZ> cz)
                    {
                        cz = segments[ind].z;
                        curviture += segments[ind].curveture;
                        ind++;
                    }
                    gameObject.Render(player.GetX, player.GetZ, curviture, spriteBatch);
                }
            }



            //foreach (var gameObject in gameObjects)
            //{
            //    if (gameObject.GetZ - player.GetZ > 0)
            //    {
            //        gameObject.Render(player.GetX, player.GetZ, GetAllPrevCurves(), spriteBatch);
            //    }
            //}
        }
    }
    public class GlobalRenderSettings
    {
        public static float cameraHeight = 5;
        public static float cameraToSreen = 0.5f;
        public static BasicEffect basicEffect;
        public static void LoadGlobalRenderSettings(GraphicsDevice device)
        {
            float asprat = 800f / 480f;
            Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), asprat, 0.1f, 1000.0f);//(800, 480, -100, 100);
            Matrix view = Matrix.CreateLookAt(new Vector3(0, 0, 10), Vector3.Zero, Vector3.Up);

            basicEffect = new BasicEffect(device);
            basicEffect.LightingEnabled = false;
            basicEffect.TextureEnabled = false;
            basicEffect.VertexColorEnabled = true;
            basicEffect.Projection = projection;
            basicEffect.View = view;
            basicEffect.World = Matrix.Identity;
        }
    }
    static class Extention
    {
        static int width = 800;
        static int height = 480;
        public static float ConvertToMono_x(this float x)
        {
            return ((x+ sizeX) /(2* sizeX)) * width;
        }
        const float sizeX = 2;
        const float sizeY = 4.1f;
        public static float ConvertToMono_y(this float y)
        {
            return ((sizeY - y) / (sizeY*2)) * height;
        }
    }
}
