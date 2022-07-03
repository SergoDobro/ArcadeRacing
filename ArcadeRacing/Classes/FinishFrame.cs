using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ArcadeRacing.Classes
{
    class FinishFrame
    {
        Texture2D texture;
        MainGameClass mainGame;
        public FinishFrame(MainGameClass mainGameClass)
        {
            mainGame = mainGameClass;
        }
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("EndFrame");
        }
        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                ProgramManager.MoveToState(ProgramState.Menu);
            }
        }
        float t = 0;
        
        public void Render(SpriteBatch spriteBatch)
        {
            t += 0.75f;
            spriteBatch.Begin();
            spriteBatch.Draw(texture, new Vector2(GlobalRenderSettings.windowWidth / 2 - texture.Width / 2,
                GlobalRenderSettings.windowHeight / 2 - texture.Height / 2), new Color(
                    1, 1, (float)Math.Sin(t), 0.9f));
            spriteBatch.End();
        }
    }
}
