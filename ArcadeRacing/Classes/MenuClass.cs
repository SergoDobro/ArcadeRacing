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
    enum MenuStates { none, animation, moving }
    class MenuClass
    {
        Texture2D backGround_texture;
        Texture2D btn_texture;
        MenuStates menuStates = MenuStates.none;
        public void LoadContent(ContentManager content)
        {
            backGround_texture = content.Load<Texture2D>("MainMenu");
            btn_texture = content.Load<Texture2D>("PlayButton");
        }
        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState(); 
            if (keyboardState.IsKeyDown(Keys.Enter) && menuStates == MenuStates.none)
            {
                menuStates = MenuStates.animation;
                new Thread(()=> {
                    Thread.Sleep(2000);
                    menuStates = MenuStates.moving;
                }).Start();
            }
            if (menuStates == MenuStates.moving)
            {
                ProgramManager.MoveToState(ProgramState.InGame);
            }
        }
        float t = 0;
        public void Render(SpriteBatch spriteBatch)
        {
            t += 1;
            spriteBatch.Begin();
            spriteBatch.Draw(backGround_texture, new Vector2(0,0), Color.White);
            if (menuStates == MenuStates.animation)
                spriteBatch.Draw(btn_texture, new Vector2(backGround_texture.Width / 2 - btn_texture.Width / 2,
                    backGround_texture.Height / 2 - btn_texture.Height / 2), new Color(
                        1,1, (float)Math.Sin(t)));
            else
                spriteBatch.Draw(btn_texture, new Vector2(backGround_texture.Width / 2 - btn_texture.Width / 2,
                    backGround_texture.Height / 2 - btn_texture.Height / 2), Color.White);
            spriteBatch.End();
        }
    }
}
