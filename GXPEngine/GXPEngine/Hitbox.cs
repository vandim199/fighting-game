using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GXPEngine
{
    class Hitbox : GameObject
    {
        int _frameCreated;
        int _transparency = 50;

        public Hitbox(int posX, int posY, int sizeX, int sizeY, int playerFrame) : base()
        {
            _frameCreated = playerFrame;

            Canvas hitbox = new Canvas(sizeX, sizeY);
            hitbox.graphics.FillRectangle(new SolidBrush(Color.FromArgb(_transparency, 255, 0, 0)), new Rectangle(0, 0, hitbox.width, hitbox.height));
            hitbox.SetXY(posX - GameLoader.player1.width / 2, posY - GameLoader.player1.height / 2);
            AddChild(hitbox);
        }

        void Update()
        {
            if (Player.onFrame != _frameCreated)
            {
                this.LateDestroy();
            }
        }

        void OnCollision(GameObject other)
        {
            //other is Enemy* but I still don't have enemies
            if(other is Hurtbox)
            {
                Console.WriteLine("HIT");
            }
        }
    }
}
