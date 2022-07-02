﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcadeRacing.Classes
{
    enum ProgramState { Menu, InGame, Finish }
    static class ProgramManager
    {
        static ProgramState programState = ProgramState.Menu;
        static MainGameClass _mainGame;
        static MenuClass _menuClass;
        static public void Init()
        {
            _mainGame = new MainGameClass();
            _menuClass = new MenuClass();
        }
        static public void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
        {
            _mainGame.LoadContent(graphicsDevice, content);
            _menuClass.LoadContent(content);
        }
        static public void Update(GameTime gameTime)
        {
            switch (programState)
            {
                case ProgramState.Menu:
                    _menuClass.Update();
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
        static public void Render(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            switch (programState)
            {
                case ProgramState.Menu:
                    _menuClass.Render(spriteBatch);
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

        static public void MoveToState(ProgramState newProgramState)
        {
            switch (programState)
            {
                case ProgramState.Menu:
                    switch (newProgramState)
                    {
                        case ProgramState.InGame:
                            _mainGame.Start();
                            programState = ProgramState.InGame;
                            break;
                        default:
                            break;
                    }
                    break;

                case ProgramState.InGame:
                    switch (newProgramState)
                    {
                        case ProgramState.Finish:
                            programState = ProgramState.Finish;
                            break;
                        default:
                            break;
                    }
                    break;

                case ProgramState.Finish:
                    switch (newProgramState)
                    {
                        case ProgramState.Menu:
                            programState = ProgramState.Menu;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

        }
    }
}
