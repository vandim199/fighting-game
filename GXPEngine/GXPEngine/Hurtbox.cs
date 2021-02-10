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
        int _transparency = 50;
        public int playerID;
        Player player;

        public Hurtbox(int posX, int posY, int sizeX, int sizeY, int playerFrame, int newPlayerID, Player newPlayer) : base(sizeX, sizeY)
        {
            _frameCreated = playerFrame;
            SetOrigin(width / 2, height / 2);
            graphics.FillRectangle(new SolidBrush(Color.FromArgb(_transparency, 0, 255, 0)), new Rectangle(0, 0, width, height));
            SetXY(posX, posY);
            //SetXY(posX - GameLoader.player1.width / 2, posY - GameLoader.player1.width / 2);
            //owner = ownerID;

            player = newPlayer as Player;
            playerID = newPlayerID;
            player.numberOfHurtboxes++;

            player.AddChild(this);
        }

        void Update()
        {
            if (player.onFrame != _frameCreated)
            {
                player.numberOfHurtboxes--;
                this.LateDestroy();
            }
        }
    }
}
