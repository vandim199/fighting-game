using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GXPEngine
{
    class Hurtbox : GameObject
    {
        int _frameCreated;
        int _transparency = 50;

        public Hurtbox(int posX, int posY, int sizeX, int sizeY, int playerFrame) : base()
        {
            _frameCreated = playerFrame;

            Canvas hitbox = new Canvas(sizeX, sizeY);
            hitbox.graphics.FillRectangle(new SolidBrush(Color.FromArgb(_transparency, 0, 255, 0)), new Rectangle(0, 0, hitbox.width, hitbox.height));
            hitbox.SetXY(posX, posY);
            AddChild(hitbox);
        }

        void Update()
        {
            if (Player1.onFrame != _frameCreated)
            {
                this.LateDestroy();
            }
        }
    }
}
