using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcadeRacing.Classes
{

    public class GlobalRenderSettings
    {
        public static float cameraHeight = 5;
        public static float cameraToSreen = 0.5f;
        public static BasicEffect basicEffect;
        public static int windowWidth = 800;
        public static int windowHeight = 480;
        public static float playerMLT = 6f;
        public static void LoadGlobalRenderSettings(GraphicsDevice device)
        {
            float asprat = 800f / 480f;
            Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), asprat, 0.1f, 1000.0f);
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
}
