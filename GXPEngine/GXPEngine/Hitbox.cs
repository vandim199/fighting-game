using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GXPEngine
{
    class Hitbox : Canvas
    {
        int _frameCreated;
        int _transparency = 50;
        public int playerID;
        Player player;

        public Hitbox(int posX, int posY, int sizeX, int sizeY, int playerFrame, int newPlayerID, Player newPlayer) : base(sizeX, sizeY)
        {
            _frameCreated = playerFrame;
            graphics.FillRectangle(new SolidBrush(Color.FromArgb(_transparency, 255, 0, 0)), new Rectangle(0, 0, width, height));
            SetXY(posX - GameLoader.player1.width / 2, posY - GameLoader.player1.height / 2);

            player = newPlayer as Player;
            playerID = newPlayerID;
        }

        void Update()
        {
            if (player.onFrame != _frameCreated)
            {
                this.LateDestroy();
            }
        }

        void OnCollision(GameObject other)
        {
            if(other is Hurtbox)
            {
                Hurtbox otherHurtbox = other as Hurtbox;

                if (otherHurtbox.playerID != this.playerID)
                {
                    Console.WriteLine("HIT");
                }
            }
        }
    }
}
