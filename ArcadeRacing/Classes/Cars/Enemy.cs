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
        float whereToGoLine = -0.5f;

        float riskiness = (float)(random.NextDouble() - 1)/2f;
        int aiTimer= 0;
        public override void ControlsLogic(float dt, float seg0curv)
        {
            aiTimer += 1;
            if (aiTimer%30 == 0)
            {
                whereToGo = (float)(random.NextDouble() - 1);
                whereToGoLine = (random.Next(0, 2) - 0.5f)*2*0.4f;
            }

            if (GetX < whereToGo - 0.2f + whereToGoLine)
                carStateSides = CarStateSides.MoveRight;
            else if (GetX > whereToGo + 0.2f + whereToGoLine)
                carStateSides = CarStateSides.MoveLeft;
            else
                carStateSides = CarStateSides.None;
            if (speed / maxSpeed > 0.5f + riskiness && Math.Abs(seg0curv) > 0.2f)
                carStateForward = CarStateForward.Decelerate;
            else if (speed / maxSpeed > 0.95f + riskiness)
                carStateForward = CarStateForward.None;
            else 
                carStateForward = CarStateForward.Accelerate;
        }
    }
}
