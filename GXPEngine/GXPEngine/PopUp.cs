using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class PopUp : Sprite
    {
        float showTime;
        bool destroyed = false;

        public PopUp(string image, int posX = 0, int posY = 0) : base(image)
        {
            showTime = Time.time;
            SetXY(game.width / 2 + width + posX, game.height / 2 + height + posY);
            SetOrigin(width / 2, height / 2);
        }

        void Update()
        {
            if (Time.time > showTime + 1500)
            {
                destroyed = true;
                LateDestroy();
            }
        }
    }
}
