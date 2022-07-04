using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcadeRacing.Classes
{
    class Background
    {
        Texture2D texture_top, texture_bottom;
        int horizonHeight =(int) (GlobalRenderSettings.windowHeight / 2 - 50 * GlobalRenderSettings.windowHeight/480f);
        Rectangle rectangle_top_01, rectangle_top_02;
        Rectangle rectangle_bottom;
        static int width =(int)( 1600* GlobalRenderSettings.windowWidth/800);
        public void LoadContent(ContentManager content, GraphicsDevice device)
        {
            texture_top = content.Load<Texture2D>("background_top");

            texture_bottom = new Texture2D(device, 1, 1);
            texture_bottom.SetData(new Color[1] { new Color(0, 120, 0) }); //content.Load<Texture2D>("background_bottom");

            rectangle_top_01 = new Rectangle(0, 0, width, horizonHeight);
            rectangle_top_02 = new Rectangle(0, 0, width, horizonHeight);

            rectangle_bottom = new Rectangle(0, horizonHeight, GlobalRenderSettings.windowWidth, GlobalRenderSettings.windowHeight- horizonHeight+10);
        }
        public void Render(SpriteBatch spriteBatch, float playertotalrotation)
        {
            RenderTop(spriteBatch, -playertotalrotation);
            RenderBottom(spriteBatch);
        }
        private void RenderTop(SpriteBatch spriteBatch, float playertotalrotation)
        {
            rectangle_top_01.X = (int)(playertotalrotation * (50* GlobalRenderSettings.windowWidth / 800f));
            if (rectangle_top_01.X > -1) rectangle_top_01.X = (rectangle_top_01.X % width) - width;
            if (rectangle_top_01.X < -(width-1)) rectangle_top_01.X = (rectangle_top_01.X % width);
            rectangle_top_02.X = rectangle_top_01.X + rectangle_top_01.Width;
            spriteBatch.Draw(texture_top, rectangle_top_01, Color.White);
            spriteBatch.Draw(texture_top, rectangle_top_02, Color.White);
        }
        private void RenderBottom(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture_bottom, rectangle_bottom, Color.White);
        }

    }
}
