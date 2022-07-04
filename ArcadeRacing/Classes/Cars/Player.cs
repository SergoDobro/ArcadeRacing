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
            System.Diagnostics.Debug.WriteLine(pos_x + " " + pos_z);
            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState gamePadState = GamePad.GetState(0);
            if (InputManager.GetInputY() > 0)
            {
                carStateForward = CarStateForward.Accelerate;
            }
            else if (InputManager.GetInputY() < 0)
                carStateForward = CarStateForward.Decelerate;
            else
                carStateForward = CarStateForward.None;


            if (InputManager.GetInputX() < 0)
            {
                inputParametr = - InputManager.GetInputX();
                carStateSides = CarStateSides.MoveLeft;
            }
            else if (InputManager.GetInputX() > 0)
            {
                inputParametr = InputManager.GetInputX();
                carStateSides = CarStateSides.MoveRight;
            }
            else
                carStateSides = CarStateSides.None;





            GamePad.SetVibration(0, (speed / maxSpeed) / 2-0.1f, (speed / maxSpeed) / 2 - 0.1f);
        }

        public override void FinishedTrack()
        {
            base.FinishedTrack();
            new System.Threading.Thread(()=> {
                System.Threading.Thread.Sleep(1000 * secsAfterOff+100);
                ProgramManager.MoveToState(ProgramState.Finish);
            }).Start();
        }

        public override void UpdateSounds(float mainPlayerPosCastile)
        {
            base.UpdateSounds(mainPlayerPosCastile);
            //soundPlayer.Voulme /= 2f;
            //soundPlayer2.Voulme /= 2f;
        }
    }
}
