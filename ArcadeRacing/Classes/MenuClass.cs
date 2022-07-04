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
        SoundPlayer soundPlayer;
        
        public void LoadContent(ContentManager content)
        {
            backGround_texture = content.Load<Texture2D>("MainMenu");
            btn_texture = content.Load<Texture2D>("PlayButton");
            soundPlayer = new SoundPlayer("StartSound");
            soundPlayer.LoadContent(content);
        }
        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState(); 
            if (InputManager.GetEnter() && menuStates == MenuStates.none)
            {
                soundPlayer.Play();
                   menuStates = MenuStates.animation;
                new Thread(()=> {
                    Thread.Sleep(200);
                    menuStates = MenuStates.moving;
                }).Start();
            }
            if (menuStates == MenuStates.moving)
            {

                ProgramManager.MoveToState(ProgramState.InGame);
                menuStates = MenuStates.none;
            }
        }
        float t = 0;
        public void Render(SpriteBatch spriteBatch)
        {
            t += 0.75f;
            spriteBatch.Begin();
            spriteBatch.Draw(backGround_texture, new Rectangle(0,0, GlobalRenderSettings.windowWidth, GlobalRenderSettings.windowHeight), Color.White);
            if (menuStates == MenuStates.animation)
                spriteBatch.Draw(btn_texture, new Vector2(GlobalRenderSettings.windowWidth / 2 - btn_texture.Width / 2,
                    GlobalRenderSettings.windowHeight / 2 - btn_texture.Height / 2), new Color(
                        1,1, (float)Math.Sin(t)));
            else
                spriteBatch.Draw(btn_texture, new Vector2(GlobalRenderSettings.windowWidth / 2 - btn_texture.Width / 2,
                    GlobalRenderSettings.windowHeight / 2 - btn_texture.Height / 2), Color.White);
            spriteBatch.End();
        }
        
    }
}
