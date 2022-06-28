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

        short[] ind = { 0, 2, 1, 1, 2, 3 };
        public void RenderSegment(float player_pos_x, float player_pos_z, GraphicsDevice device)
        {
            basicEffect = new BasicEffect(device);

            VertexPositionTexture[] vert = new VertexPositionTexture[4];
            vert[0].Position = new Vector3(0, 0, 0);
            vert[1].Position = new Vector3(100, 0, 0);
            vert[2].Position = new Vector3(0, 100, 0);
            vert[3].Position = new Vector3(100, 100, 0);


            foreach (EffectPass effectPass in basicEffect.CurrentTechnique.Passes)
            {
                effectPass.Apply();
                device.DrawUserIndexedPrimitives<VertexPositionTexture>(
                    PrimitiveType.TriangleList, vert, 0, vert.Length, ind, 0, ind.Length / 3);
            }
        }
    }
}
