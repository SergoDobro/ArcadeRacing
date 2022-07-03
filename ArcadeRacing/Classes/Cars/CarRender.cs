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

            timer += dt;
            if (globalCarState == GlobalCarState.InGame )
            {
                if (currentFrame > frameCount)
                {
                    currentFrame = frameCount - 1;
                }
                if (carStateSides == CarStateSides.None && currentFrame > 0)
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
                    sourceRectangle = new Rectangle((currentFrame + 1) * widthFrame, 0, -widthFrame, heightFrame);
                }
                else if (carStateSides == CarStateSides.MoveRight)
                {
                    sourceRectangle = new Rectangle(currentFrame * widthFrame, 0, widthFrame, heightFrame);
                }
                else
                {
                    sourceRectangle = new Rectangle(currentFrame * widthFrame, 0, widthFrame, heightFrame);
                }
            }
            else if (globalCarState == GlobalCarState.Death)
            {
                if (timer > 0.17f)
                {
                    currentFrameDeath++;
                    timer = 0;
                }
                if (currentFrameDeath >= 4)
                {
                    currentFrameDeath = 4;
                    globalCarState = GlobalCarState.Dead;
                    FinishedTrack();
                }
                sourceRectangle = new Rectangle(1 * 62, 0, 62, 32);
            }
            else
            {
                sourceRectangle = new Rectangle(0, 0, widthFrame, heightFrame);
            }
            
        }
        int currentFrameDeath = 0;
        Texture2D explosionTexture;
        public override (Texture2D, Rectangle, Rectangle) Render(float player_pos_x, float player_pos_z, float prevCurves)
        {
            var res = base.Render(player_pos_x, player_pos_z, prevCurves);
            res.Item3 = sourceRectangle;
            if (globalCarState == GlobalCarState.Death)
            {
                res.Item1 = explosionTexture;
                int dX = (int)(res.Item2.Width * 0.5);
                int dY = (int)(res.Item2.Height * 0.5);
                res.Item2.X -= dX;
                res.Item2.Width += dX * 2;
                res.Item2.Y -= dY * 2;
                res.Item2.Height += dY * 2;
            }
            else if (globalCarState == GlobalCarState.Waiting || globalCarState == GlobalCarState.Finished
                || globalCarState == GlobalCarState.InGame)
            {
            }
            return res;
        }

        public override void LoadContent(ContentManager content)
        {
            objectWidth = 2;
            objectHeight = 2;
            if (texture == null)
                texture = content.Load<Texture2D>("CarSprites");
            if (explosionTexture == null)
                explosionTexture = content.Load<Texture2D>("explosion");

            if (soundPlayer == null)
                soundPlayer.LoadContent(content);
            if (soundPlayer2 == null)
                soundPlayer2.LoadContent(content);
            soundPlayer.IsRepeating = true;
            soundPlayer2.IsRepeating = true;
            soundPlayer.Voulme = 0;
            soundPlayer2.Voulme = 0;
            currentFrameDeath = 0;

            new System.Threading.Thread(() => {
                soundPlayer.Play();
                System.Threading.Thread.Sleep(100);
                soundPlayer2.Play();
            }); 
        }

    }
}
