using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Backdrop : Sprite
    {
        public Backdrop(string backgroundImage) : base(backgroundImage, false, false)
        {
            game.AddChild(this);
        }
    }
}
