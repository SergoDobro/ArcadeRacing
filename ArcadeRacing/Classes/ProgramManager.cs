using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcadeRacing.Classes
{
    enum ProgramState { Menu, InGame, Finish }
    class ProgramManager
    {
        ProgramState programState = ProgramState.InGame; 
        MainGameClass _mainGame;
        public ProgramManager()
        {
            _mainGame = new MainGameClass();
        }
        public void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
        {
            _mainGame.LoadContent(graphicsDevice, content);
        }
        public void Update(GameTime gameTime)
        {
            switch (programState)
            {
                case ProgramState.Menu:
                    break;
                case ProgramState.InGame:
                    _mainGame.Update(gameTime);
                    break;
                case ProgramState.Finish:
                    break;
                default:
                    break;
            }
        }
        public void Render(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            switch (programState)
            {
                case ProgramState.Menu:
                    break;
                case ProgramState.InGame:
                    _mainGame.Render(graphicsDevice, spriteBatch);
                    break;
                case ProgramState.Finish:
                    break;
                default:
                    break;
            }
        }
    }
}
