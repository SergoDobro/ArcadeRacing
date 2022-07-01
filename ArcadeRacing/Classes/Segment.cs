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

        short[] ind = { 0, 1, 2, 0, 2, 3,
        4,5,6,4,6,7,
        8,9,10,8,10,11};
        public float z;
        public static float segmentLength = 0.1f;

        static float roadWidth = 14;
        static float boardroadWidth = 0.75f;
        public float curveture = 0f;
        public static float dy = 1f;


        private static BasicEffect basicEffect;
        private static float cameraHeight;
        private static float cameraToSreen;

        VertexPositionColor[] vert = new VertexPositionColor[12];

        public static void LoadSegmentation(GraphicsDevice device)
        {
            basicEffect = GlobalRenderSettings.basicEffect;
            cameraHeight = GlobalRenderSettings.cameraHeight;
            cameraToSreen = GlobalRenderSettings.cameraToSreen;
        }

        public Segment(float z, float curve)
        {
            curveture = curve;
            this.z = z;
            for (int i = 0; i < 4; i++)
            {
                vert[i].Color = Color.Gray;
            }

            if ((int)(z*10)%2==0)
            {
                for (int i = 4; i < 12; i++)
                {
                    vert[i].Color = Color.White;
                }
            }
            else
            {
                for (int i = 4; i < 12; i++)
                {
                    vert[i].Color = Color.Red;
                }
            }


        }
        public void RenderSegment(float player_pos_x, float player_pos_z, GraphicsDevice device, float prevCurves, out float returnprevCurves)
        {
            float dz = (z - player_pos_z);
            float y1 = cameraHeight - cameraHeight * cameraToSreen / dz;
            float y2 = cameraHeight - cameraHeight * cameraToSreen / (dz + segmentLength);

            float wr =   (roadWidth / 2)                - player_pos_x;
            float wl = - (roadWidth / 2)                - player_pos_x;

            float crv = curveture * y2;
            returnprevCurves = prevCurves + crv;

            float x1l = prevCurves + wl * cameraToSreen / dz;
            float x1r = prevCurves + wr * cameraToSreen / dz;

            float x2l = prevCurves + crv + wl * cameraToSreen / (dz + segmentLength);
            float x2r = prevCurves + crv + wr * cameraToSreen / (dz + segmentLength);

            float x1l2 = prevCurves +       (-boardroadWidth - (roadWidth / 2) - player_pos_x) * cameraToSreen / dz;
            float x2l2 = prevCurves + crv + (-boardroadWidth - (roadWidth / 2) - player_pos_x) * cameraToSreen / (dz + segmentLength);


            float x1r2 = prevCurves +       (boardroadWidth + (roadWidth / 2) - player_pos_x) * cameraToSreen / dz;
            float x2r2 = prevCurves + crv + (boardroadWidth + (roadWidth / 2) - player_pos_x) * cameraToSreen / (dz + segmentLength);

            //x1l += player_pos_x;
            //x1r += player_pos_x;

            //x2l += player_pos_x;
            //x2r += player_pos_x;

            //x1l = x1l.ConvertToMono_x();
            //x2l = x2l.ConvertToMono_x();
            //x1r = x1r.ConvertToMono_x();
            //x2r = x2r.ConvertToMono_x();
            //y1 = y1.ConvertToMono_y();
            //y2 = y2.ConvertToMono_y();

            y1 -= 4.14f;
            y2 -= 4.14f;

            //center road
            vert[0].Position = new Vector3(x1l, y1, 0);
            vert[1].Position = new Vector3(x2l, y2, 0);
            vert[2].Position = new Vector3(x2r, y2, 0);
            vert[3].Position = new Vector3(x1r, y1, 0);

            //left bound
            vert[4].Position = new Vector3(x1l2, y1, 0);
            vert[5].Position = new Vector3(x2l2, y2, 0);
            vert[6].Position = new Vector3(x2l, y2, 0);
            vert[7].Position = new Vector3(x1l, y1, 0);

            //right bound
            vert[8].Position = new Vector3(x1r, y1, 0);
            vert[9].Position = new Vector3(x2r, y2, 0);
            vert[10].Position = new Vector3(x2r2, y2, 0);
            vert[11].Position = new Vector3(x1r2, y1, 0);
            //float topX = 6.9f;
            //float topY = topX / (800/480f);
            //vert[8].Position = new Vector3(topX-1, topY-1, 0);
            //vert[9].Position = new Vector3(topX-1, topY, 0);
            //vert[10].Position = new Vector3(topX, topY, 0);
            //vert[11].Position = new Vector3(topX, topY-1, 0);

            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                device.DrawUserIndexedPrimitives<VertexPositionColor>(
                    PrimitiveType.TriangleList, vert, 0, vert.Length, ind, 0, 6);
            }
        }
    }
}
