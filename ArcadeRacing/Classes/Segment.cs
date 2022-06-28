using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadeRacing.Classes
{
    class Segment
    {
        private BasicEffect basicEffect;

        short[] ind = { 0, 1, 2, 0, 2, 3 };
        public float z;
        public static float segmentLength = 1;
        static float cameraHeight = 5;
        static float cameraToSreen = 2;
        static float roadWidth = 5;
        public float curveture = -0.5f;
        public static float dy = 1f;

        VertexPositionColor[] vert = new VertexPositionColor[4];

        public Segment(float z, GraphicsDevice device, float curve)
        {
            curveture = curve;
            this.z = z;
            vert[0].Color = Color.Gray;
            vert[1].Color = Color.Black;
            vert[2].Color = Color.Black;
            vert[3].Color = Color.Gray;
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
        public void RenderSegment(float player_pos_x, float player_pos_z, GraphicsDevice device, float prevCurves, out float returnprevCurves)
        {
            float dz = (z - player_pos_z);
            float y1 = cameraHeight - cameraHeight * cameraToSreen / dz;
            float y2 = cameraHeight - cameraHeight * cameraToSreen / (dz + segmentLength);

            float wr = (roadWidth / 2) - player_pos_x;
            float wl = - (roadWidth / 2) - player_pos_x;

            float x1l = wl * cameraToSreen / dz;
            float x1r = wr * cameraToSreen / dz;

            float x2l = wl * cameraToSreen / (dz + segmentLength);
            float x2r = wr * cameraToSreen / (dz + segmentLength);

            float crv = curveture * y2;
            x1l += prevCurves;
            x1r += prevCurves;

            x2l += prevCurves + crv;
            x2r += prevCurves + crv;
            returnprevCurves = prevCurves + crv;

            //x1l = x1l.ConvertToMono_x();
            //x2l = x2l.ConvertToMono_x();
            //x1r = x1r.ConvertToMono_x();
            //x2r = x2r.ConvertToMono_x();
            //y1 = y1.ConvertToMono_y();
            //y2 = y2.ConvertToMono_y();
            y1 /= 1;
            y2 /= 1;

            y1 -= 4.2f;
            y2 -= 4.2f;

            vert[0].Position = new Vector3(x1l, y1, 0);
            vert[1].Position = new Vector3(x2l, y2, 0);
            vert[2].Position = new Vector3(x2r, y2, 0);
            vert[3].Position = new Vector3(x1r, y1, 0);

            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.DrawUserIndexedPrimitives<VertexPositionColor>(
                    PrimitiveType.TriangleList, vert, 0, 4, ind, 0, 2);
            }
        }
    }
    static class Extention
    {
        static int width = 800;
        static int height = 480;
        public static float ConvertToMono_x(this float x)
        {
            return (width / 2) + x * width/2;
        }
        public static float ConvertToMono_y(this float y)
        {
            return height - y*50;
        }
    }
}
