using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcadeRacing.Classes
{
    static class InputManager
    {
        static public float GetInputX(int player = 0)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState gamePadState = GamePad.GetState(player);
            if (keyboardState.IsKeyDown(Keys.D))
                return 1;
            if (keyboardState.IsKeyDown(Keys.A))
                return -1;
            if (gamePadState.ThumbSticks.Left.X!=0)
                return gamePadState.ThumbSticks.Left.X;
            return 0;
        }
        static public float GetInputY(int player = 0)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState gamePadState = GamePad.GetState(player);
            if (keyboardState.IsKeyDown(Keys.W))
                return 1;
            if (keyboardState.IsKeyDown(Keys.S))
                return -1;
            if (gamePadState.Buttons.LeftShoulder == ButtonState.Pressed ||
                gamePadState.Buttons.RightShoulder == ButtonState.Pressed)
                return 1;
            if (gamePadState.Triggers.Left > 0 ||
                gamePadState.Triggers.Right > 0)
                return -1;
            return 0;

        }
        static public bool GetEnter(int player = 0)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState gamePadState = GamePad.GetState(player);
            return (keyboardState.IsKeyDown(Keys.Enter) || gamePadState.Buttons.A == ButtonState.Pressed);
        }
    }
}
