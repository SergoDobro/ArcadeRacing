using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcadeRacing.Classes.Cars
{
    partial class Car 
    {
        protected float timer;
        protected Rectangle sourceRectangle;
        private int frameCount = 5; //count кадров
        private float delay = 0.05f;
        private int widthFrame = 56;
        private int heightFrame = 32;
        protected private int currentFrame = 0;
        public void UpdateDraw(float dt)
        {
            
            timer +=  dt;


            if (currentFrame > frameCount) { 
                currentFrame = frameCount-1; 
            }

            if (carStateSides == CarStateSides.None && currentFrame>0)
            {
                if (currentFrame == 0)
                    currentFrame = 1;
                else if (currentFrame == 1)
                    currentFrame = 0;
                else currentFrame -= 1;
            }
            else if (timer > delay)
            {

                currentFrame++;
                timer = 0;
            }

            if (carStateSides == CarStateSides.MoveLeft)
            {
                sourceRectangle = new Rectangle((currentFrame+1) * widthFrame, 0, -widthFrame, heightFrame);
            }
            else if (carStateSides == CarStateSides.MoveRight)
            {
                sourceRectangle = new Rectangle(currentFrame * widthFrame, 0, widthFrame, heightFrame);
            }
            else
            {
                sourceRectangle = new Rectangle(currentFrame * widthFrame, 0, widthFrame, heightFrame);
            }
            //objectWidth = 5;
        }
        public override (Texture2D, Rectangle, Rectangle) Render(float player_pos_x, float player_pos_z, float prevCurves)
        {
            var res = base.Render(player_pos_x, player_pos_z, prevCurves);
            res.Item3 = sourceRectangle;
            return res;
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("CarSprites");
            soundPlayer.LoadContent(content);
            soundPlayer.IsRepeating = true;
            soundPlayer.Play();
            soundPlayer2.LoadContent(content);
            soundPlayer2.IsRepeating = true;
            System.Threading.Thread.Sleep(100);
            soundPlayer2.Play();
        }
    }
}
