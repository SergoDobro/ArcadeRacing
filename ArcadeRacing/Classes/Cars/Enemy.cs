using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadeRacing.Classes.Cars
{
    class Enemy : Car
    { 
        float whereToGo = 0;

        float riskiness = (float)(random.NextDouble() - 1)/2f;
        public override void ControlsLogic(float dt, float seg0curv)
        {
            
            if (GetX < whereToGo - 0.2f)
                carStateSides = CarStateSides.MoveRight;
            else if (GetX > whereToGo + 0.2f)
                carStateSides = CarStateSides.MoveLeft;
            else
                carStateSides = CarStateSides.None;
            if (speed / maxSpeed > 0.5f + riskiness && Math.Abs(seg0curv) > 0.2f)
                carStateForward = CarStateForward.Decelerate;
            else if (speed / maxSpeed > 0.95f + riskiness)
                carStateForward = CarStateForward.None;
            else 
                carStateForward = CarStateForward.Accelerate;
            if (Math.Abs(seg0curv) > 0.1f)
                whereToGo = (float)(random.NextDouble() - 1);
        }
    }
}
