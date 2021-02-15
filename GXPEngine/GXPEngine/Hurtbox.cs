using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GXPEngine
{
    class Hurtbox : Canvas
    {
        int _frameCreated;
        int _transparency = 0;
        public int playerID;
        Player player;
        public bool isHit = false;
        public int damageTaken = 0;

        public Hurtbox(int posX, int posY, int sizeX, int sizeY, int playerFrame, int newPlayerID, Player newPlayer, bool mirrored = false) : base(sizeX, sizeY)
        {
            _frameCreated = playerFrame;
            graphics.FillRectangle(new SolidBrush(Color.FromArgb(_transparency, 0, 255, 0)), new Rectangle(0, 0, width, height));
            
            player = newPlayer as Player;
            playerID = newPlayerID;
            player.numberOfHurtboxes++;

            player.AddChild(this);

            //DO NOT TOUCH:
            SetOrigin(player.width/2, player.height / 2);
            SetXY(posX, posY);

            if (mirrored)
            {
                //x = (player.width - (x + width)) + (1000 - player.width);
                x = -x -width + 1000;
            }
        }

        void Update()
        {
            if (player.currentFrame != _frameCreated)
            {
                player.numberOfHurtboxes--;
                this.LateDestroy();
            }

            if (isHit)
            {
                player.damageTaken = damageTaken;
                isHit = false;
            }
        }
    }
}