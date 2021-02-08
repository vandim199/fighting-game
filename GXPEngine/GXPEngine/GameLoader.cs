using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class GameLoader : GameObject
    {
        public GameLoader() : base(false)
        {
            new Backdrop("bg1.jpg");
        }
    }
}
