using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadeRacing.Classes.Cars
{
    enum CarStateForward { Accelerate, Decelerate, None }
    enum CarStateSides { MoveRight, MoveLeft, None }
    partial class Car : GameObject
    {
        protected const float maxSpeed = 7;
        protected float accel = maxSpeed / 5;
        protected float breaking = -maxSpeed;
        protected float decel = -maxSpeed / 5;
        protected float offRoadDecel = -maxSpeed / 2;
        protected float offRoadLimit = maxSpeed / 4;


        protected float cenrtryFugal = 46f;
        protected float movecoef = 4f;
        public float speed = 0f;
        public SoundPlayer soundPlayer = new SoundPlayer("carRoar");
        public SoundPlayer soundPlayer2 = new SoundPlayer("carRoar");

        protected CarStateSides carStateSides_prev = CarStateSides.None;
        protected CarStateForward carStateForward = CarStateForward.None;
        protected CarStateSides carStateSides = CarStateSides.None;
        public void Killed()
        {

        }
        const float interpoleSound = 0.12f;
        public virtual void Update(float dt, float seg0curv)
        {
            soundPlayer.Voulme = (speed / maxSpeed) / 2;
            soundPlayer2.Voulme = (speed / maxSpeed) / 2;// (speed / maxSpeed);
            soundPlayer.Pitch = (speed / maxSpeed) * 1f - 1;
            soundPlayer2.Pitch = ((speed / maxSpeed) * 1.5f - 1)* interpoleSound+(1- interpoleSound)*soundPlayer2.Pitch;// (speed / maxSpeed);

            UpdateDraw(dt);
            GetX += -dt * seg0curv * cenrtryFugal * (float)Math.Pow((speed / maxSpeed), 1.5);
            if (carStateForward == CarStateForward.Accelerate)
            {
                speed += dt * accel;
                if (speed > maxSpeed)
                    speed = maxSpeed;
            }
            else if (carStateForward == CarStateForward.Decelerate)
            {
                speed += dt * breaking;
            }
            else if (carStateForward == CarStateForward.None)
            {
                speed += dt * decel;
            }
            if (carStateSides == CarStateSides.MoveRight) GetX += dt * movecoef * speed / maxSpeed;
            if (carStateSides == CarStateSides.MoveLeft) GetX -= dt * movecoef * speed / maxSpeed;
            if (Math.Abs(GetX) > 1 && speed > offRoadLimit)
                speed += dt * offRoadDecel;
            speed = Math.Clamp(speed, 0, maxSpeed);
            GetZ += dt * speed;
            GetX = Math.Clamp(GetX, -2, 2);
            carStateSides_prev = carStateSides;

            if (carStateSides!=CarStateSides.None)
            {
                soundPlayer2.Pitch = ((speed / maxSpeed) * 2 - 1)* interpoleSound + soundPlayer2.Pitch*(1- interpoleSound);
            }
        }
        public void FinishedTrack()
        {

        }
    }
}
