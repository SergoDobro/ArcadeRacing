using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadeRacing.Classes
{
    interface IGameObject
    {
        void Render(float player_pos_x, float player_pos_z);
        bool IsIntersecting(Car car);
        float GetZPos();
        float GetXPos();
    }
}
