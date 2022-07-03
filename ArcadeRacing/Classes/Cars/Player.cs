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
        public static float CameraZ;
        public override float GetZ { get => base.GetZ; set => base.GetZ = value; }
        public override void ControlsLogic(float dt, float seg0curv)
        {
            CameraZ = pos_z - 0.7f;
            System.Diagnostics.Debug.WriteLine(pos_x + " " + pos_z);
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

        public override void FinishedTrack()
        {
            base.FinishedTrack();
            new System.Threading.Thread(()=> {
                System.Threading.Thread.Sleep(1000 * secsAfterOff+100);
                ProgramManager.MoveToState(ProgramState.Finish);
            }).Start();
        }
    }
}
