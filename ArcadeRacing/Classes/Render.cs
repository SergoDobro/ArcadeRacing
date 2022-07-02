using ArcadeRacing.Classes.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        private Background background;
        public void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
        {
            this.graphicsDevice = graphicsDevice;
            BillBoard.LoadTexture(content);
            Obsticle.LoadTexture(content);

            GlobalRenderSettings.LoadGlobalRenderSettings(graphicsDevice);
            Segment.LoadSegmentation(graphicsDevice);
            GameObject.LoadRendering(graphicsDevice);
            background = new Background();
            background.LoadContent(content, graphicsDevice);
            player.LoadContent(content);
            for (int i = 0; i < renderDistance; i++)
            {
                AddSegment();
            }
            int _z = 0;
            for (int i = 0; i < 100; i++)
            {
                _z += random.Next(10,14);
                AddGameOblect(_z);
            }

            foreach (var go in gameObjects)
            {
                if (go.GetTexture==null)
                {
                    go.LoadContent(content);
                }
            }
        }
        public void Render(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            RenderBackground(spriteBatch);
            spriteBatch.End();
            RenderSegments(graphicsDevice);
            spriteBatch.Begin();
            RenderObjects(spriteBatch);
            RenderPlayer(spriteBatch);
            spriteBatch.End();
        }
        private void RenderSegments(GraphicsDevice graphicsDevice)
        {
            float curviture = 0;
            for (int i = 0; i < renderDistance && i < segments.Count; i++)
            {
                if (segments[i].z - player.GetZ > Segment.segmentLength * SegentDistructorMult)
                {
                    segments[i].RenderSegment(player.GetX * 5, player.GetZ, graphicsDevice, curviture, out curviture);
                }
                else
                {
                    //curviture = segments[i].curveture * (player.GetZ - (player.GetZ * 10) % 10);
                }
            }
        }
        private void RenderPlayer(SpriteBatch spriteBatch)
        {
            var data = player.Render(0, player.GetZ-0.55f, 0);
            spriteBatch.Draw(data.Item1, data.Item2, data.Item3, Color.White);
        }
        Stack<(Texture2D, Rectangle, Rectangle)> objectRenderStack = new Stack<(Texture2D, Rectangle, Rectangle)>();
        private void RenderObjects(SpriteBatch spriteBatch)
        {
            float cz = 0;
            int ind = 0;
            float curviture = 0;

            for (int i = 0; i < renderDistance && i < segments.Count; i++)
            {
                //curviture += segments[ind].curveture *
                //            (GlobalRenderSettings.cameraHeight -
                //            GlobalRenderSettings.cameraHeight * GlobalRenderSettings.cameraToSreen / (segments[ind].z - player.GetZ + Segment.segmentLength)); 
                if (segments[i].z - player.GetZ > Segment.segmentLength * (SegentDistructorMult+1))
                {
                    ind = i;
                    float inter = (((player.GetZ * 10) % 10)/10f);
                    System.Diagnostics.Debug.WriteLine(inter);
                    //curviture = segments[i+1].curveture * inter + (1- inter)* segments[i].curveture;
                    break;
                }
            }
            objectRenderStack.Clear();
            gameObjects.Sort(new Comparison<GameObject>((a, b) =>
            {

                if (a.GetZ - b.GetZ > 0)
                    return 1;
                else
                    return -1;
            }));
            foreach (var gameObject in gameObjects)
            {
                if (gameObject.GetZ - player.GetZ > 0 && gameObject.GetZ - player.GetZ<segments.Count * Segment.segmentLength && ind < segments.Count)
                {
                    while (gameObject.GetZ > cz && ind < segments.Count)
                    {
                        cz = segments[ind].z;
                        curviture += segments[ind].curveture
                            *
                            (GlobalRenderSettings.cameraHeight - 
                            GlobalRenderSettings.cameraHeight * GlobalRenderSettings.cameraToSreen / (segments[ind].z - player.GetZ + Segment.segmentLength));
                        ind++;
                    }
                    objectRenderStack.Push(gameObject.Render(player.GetX * 5, player.GetZ, curviture));
                }
            }
            foreach (var obj in objectRenderStack)
            {
                if (obj.Item3==Rectangle.Empty)
                    spriteBatch.Draw(obj.Item1, obj.Item2, Color.White);
                else
                    spriteBatch.Draw(obj.Item1, obj.Item2, obj.Item3, Color.White);
            }
        }
        private void RenderBackground(SpriteBatch spriteBatch)
        {
            background.Render(spriteBatch, curvetureTotal);
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

        const float sizeX = 6.9f;
        static float sizeY = sizeX / ((float)width / height);
        public static float ConvertToMono_x(this float x)
        {
            return (width / 2) + (x / sizeX) * (width / 2);//((x+sizeX) / (2 * sizeX)) * width; // + 0.5f * width;
        }
        public static float ConvertToMono_y(this float y)
        {
            return ((sizeY - y) / (sizeY*2)) * height;
        }
    }
}
