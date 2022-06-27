using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcadeRacing.Classes
{
    class Car
    {
        protected float pos_x, pos_z;
        public virtual float GetX { get => pos_x; set => pos_x = value; }
        public virtual float GetZ { get => pos_z; set => pos_z = value; }

        public bool IsIntersecting(Car car)
        {
            return false;
        }
        public void Killed()
        {

        }

        public void Render(float player_pos_x, float player_pos_z)
        {

        }
    }
}
