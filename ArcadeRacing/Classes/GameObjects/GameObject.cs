using ArcadeRacing.Classes.Cars;
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
    abstract class GameObject
    {
        public static Random random = new Random();
        public virtual float GetX { get => pos_x; set => pos_x = value; }
        public virtual float GetZ { get => pos_z; set => pos_z = value; }
        protected float pos_x, pos_z;
        public static Texture2D debug_texture;
        private static float cameraHeight;
        private static float cameraToSreen;
        protected Texture2D texture;
        protected float objectWidth = 1;
        protected float objectHeight = 1;
        public virtual float GetObjectWidth { get => objectWidth; }
        public virtual float GetObjectWidthCollision { get => objectWidth+2; }
        public virtual float GetObjectHeight { get => objectHeight; }
        protected Rectangle rectangle = new Rectangle(0, 0, 1, 1);

        public virtual bool IsIntersecting(Car car)
        {
            if (this == car)
                return false;
            if (this is Car)
                if ((this as Car).GetGlobalCarState != GlobalCarState.InGame)
                    return false;
            bool res = (GetZ - car.GetZ > -0.1f) && (GetZ - car.GetZ) < 0.1f;


            res &= (Math.Abs(CalculateDist(car)) < CalculateHalfWidth(car));
            if (res)
            {
                if (car is Player)
                {
                    var a = Math.Abs(CalculateDist(car));
                    var b = CalculateHalfWidth(car);
                }
            }

            return res;
        }
        public virtual void OnCollision(GameObject gameObject)
        {

        }
        public virtual Texture2D GetTexture { get => texture; set => texture = value; }
        public virtual void LoadContent(ContentManager content)
        {
            texture = null;
        }
        public static void LoadRendering(GraphicsDevice device)
        {
            cameraHeight = GlobalRenderSettings.cameraHeight;
            cameraToSreen = GlobalRenderSettings.cameraToSreen;
            debug_texture = new Texture2D(device, 1, 1);
            debug_texture.SetData(new Color[1] { Color.Red });
        }
        public virtual (Texture2D, Rectangle, Rectangle) Render(float player_pos_x, float player_pos_z, float prevCurves, float myX = 0)
        {
            if (myX == 0)
                myX = GetX;
            float dz = (pos_z - player_pos_z);
            float y1 = cameraHeight - cameraHeight * cameraToSreen / dz;
            float y2 = cameraHeight - (cameraHeight - objectHeight) * cameraToSreen / dz;

            float wr = myX + (objectWidth / 2) - player_pos_x;
            float wl = myX - (objectWidth / 2) - player_pos_x;

            float x1l = prevCurves + wl * cameraToSreen / dz;
            float x1r = prevCurves + wr * cameraToSreen / dz;

            y1 -= 4.14f;
            y2 -= 4.14f;

            rectangle.X = (int)(x1l).ConvertToMono_x();
            rectangle.Width = (int)(x1r.ConvertToMono_x() - x1l.ConvertToMono_x());
            rectangle.Y = (int)(y2).ConvertToMono_y();
            rectangle.Height = (int)(y1).ConvertToMono_y() - (int)(y2).ConvertToMono_y();

            return ((texture, rectangle, Rectangle.Empty));
        }

        public virtual float CalculateDist(Car car) => CalculateX() - car.GetX * GlobalRenderSettings.playerMLT;
        public virtual float CalculateX()
        {
            if ((this is Car))
                return GetX * GlobalRenderSettings.playerMLT;
            return GetX;
        }
        public virtual float CalculateHalfWidth(Car car)
        {
            return (car.GetObjectWidthCollision + GetObjectWidthCollision) / 2f; /// 2f * 0.1f;}

        }
    }
}
