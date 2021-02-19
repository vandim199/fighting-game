using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Backdrop : AnimationSprite
    {
        public Backdrop(string backgroundImage) : base(backgroundImage, 5, 8, -1 , false, false)
        {
            SetCycle(0, 21, 5);
        }

        void Update()
        {
            Animate();
        }
    }
}