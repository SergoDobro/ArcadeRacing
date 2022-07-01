using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadeRacing.Classes
{
    enum CarStateForward { Accelerate, Decelerate, None }
    enum CarStateSides { MoveRight, MoveLeft, None }
    class Car
    {
        protected const float maxSpeed = 9;
        protected float accel = maxSpeed / 5;
        protected float breaking = -maxSpeed;
        protected float decel = -maxSpeed / 5;
        protected float offRoadDecel = -maxSpeed / 2;
        protected float offRoadLimit = maxSpeed / 4;


        protected float cenrtryFugal = 46f;
        protected float movecoef = 4f;
        protected float speed = 0f;
        protected CarStateForward carStateForward = CarStateForward.None;
        protected CarStateSides carStateSides = CarStateSides.None;



        protected float pos_x, pos_z;
        public virtual float GetX { get => pos_x; set => pos_x = value; }
        public virtual float GetZ { get => pos_z; set => pos_z = value; }

        public bool IsIntersecting(Car car)
        {
            return Math.Abs(car.GetX - pos_x) < 2 && Math.Abs(car.GetZ - pos_z) < 2;
        }
        public void Killed()
        {

        }
        public void Render(float player_pos_x, float player_pos_z)
        {

        }
        public virtual void Update(float dt, float seg0curv)
        {
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
        }


    }
}
