using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadeRacing.Classes.Cars
{
    class Player : Car
    {
        public Player()
        {
            objectWidth = 5;
            objectHeight = 2;
        }
        public override void Update(float dt, float seg0curv)
        {
            base.Update(dt, seg0curv);
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.W))
                carStateForward = CarStateForward.Accelerate;
            else if (keyboardState.IsKeyDown(Keys.S))
                carStateForward = CarStateForward.Decelerate;
            else
                carStateForward = CarStateForward.None;

            if (keyboardState.IsKeyDown(Keys.A))
                carStateSides = CarStateSides.MoveLeft;
            else if (keyboardState.IsKeyDown(Keys.D))
                carStateSides = CarStateSides.MoveRight;
            else
                carStateSides = CarStateSides.None;

        }
    }
}
