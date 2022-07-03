using ArcadeRacing.Classes.GameObjects;
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
    enum GlobalCarState { Waiting, InGame, Death, Dead, Finished }
    partial class Car : GameObject
    {
        protected const float maxSpeed = 7;
        protected float accel = maxSpeed / 8;
        protected float decel = -maxSpeed / 5;
        protected float breaking = -maxSpeed;
        protected float offRoadDecel = -maxSpeed / 2;
        protected float offRoadLimit = maxSpeed / 4;
        protected float cenrtryFugal = 60f;
        protected float movecoef = 4f;
        public float speed = 0f;

        public SoundPlayer soundPlayer = new SoundPlayer("carRoar");
        public SoundPlayer soundPlayer2 = new SoundPlayer("carRoar");

        protected CarStateSides carStateSides_prev = CarStateSides.None;
        protected CarStateForward carStateForward = CarStateForward.None;
        protected CarStateSides carStateSides = CarStateSides.None;
        protected GlobalCarState globalCarState = GlobalCarState.InGame;
        public GlobalCarState GetGlobalCarState { get => globalCarState; }
        public override float GetObjectWidthCollision { get => objectWidth * 0.9f; }
        public Car() => Reset();
        public void Reset()
        {
            globalCarState = GlobalCarState.InGame;
            speed = 0;

        }
        public void Killed()
        {
            globalCarState = GlobalCarState.Death;
            speed = 0;
            carStateSides = CarStateSides.None;
            carStateForward = CarStateForward.None;
        }
        const float interpoleSound = 0.12f;
        public virtual void Update(float dt, float seg0curv)
        {
            soundPlayer.Voulme = (speed / maxSpeed) / 2;
            soundPlayer2.Voulme = (speed / maxSpeed) / 2;// (speed / maxSpeed);
            soundPlayer.Pitch = (speed / maxSpeed) * 1f - 1;
            soundPlayer2.Pitch = ((speed / maxSpeed) * 1.5f - 1) * interpoleSound + (1 - interpoleSound) * soundPlayer2.Pitch;// (speed / maxSpeed);

            UpdateDraw(dt);
            GetX += -dt * seg0curv * cenrtryFugal * (float)Math.Pow((speed / maxSpeed), 1.5);

            if (globalCarState == GlobalCarState.InGame)
            {
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
            }
            speed = Math.Clamp(speed, 0, maxSpeed);
            pos_z += dt * speed;
            GetX = Math.Clamp(GetX, -2, 2);
            carStateSides_prev = carStateSides;

            if (carStateSides != CarStateSides.None)
            {
                soundPlayer2.Pitch = ((speed / maxSpeed) * 2 - 1) * interpoleSound + soundPlayer2.Pitch * (1 - interpoleSound);
            }
            if (globalCarState == GlobalCarState.InGame)
                ControlsLogic(dt, seg0curv);
            if (globalCarState == GlobalCarState.Finished)
            {
                carStateForward = CarStateForward.None;
                carStateSides = CarStateSides.None;
            }
        }
        public virtual void ControlsLogic(float dt, float seg0curv)
        {
        }

        protected int secsAfterOff = 4;
        public override void OnCollision(GameObject gameObject)
        {
            if (gameObject.GetType() == typeof(FinishLine))
            {
                FinishedTrack();
            }
            else if (gameObject.GetType() == typeof(Obsticle))
            {
                float dst = gameObject.CalculateDist(this);
                float hd = gameObject.CalculateHalfWidth(this);
                int sg = Math.Sign(dst);
                float bnd = Math.Abs(
                    sg * hd + gameObject.CalculateX()
                    );
                GetX = Math.Clamp(GetX * GlobalRenderSettings.playerMLT, -bnd, bnd) / GlobalRenderSettings.playerMLT;
            }
            else if (gameObject.GetType() == typeof(Enemy))
            {
                if (((Enemy)gameObject).globalCarState == GlobalCarState.InGame)
                {
                    Killed();
                }
            }
            else
            {
                Killed();
            }
            if (gameObject.GetType() == typeof(Player))
            {
                Killed();
            }
        }
        public virtual void FinishedTrack()
        {
            if (globalCarState != GlobalCarState.Dead)
                globalCarState = GlobalCarState.Finished;
            carStateSides = CarStateSides.None;

        }
    }
}
