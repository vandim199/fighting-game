using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class RangedAttack : Sprite
    {
        Player player;
        int speed = 50;

        public RangedAttack(Player newPlayer) : base("assets\\Laser.png")
        {
            player = newPlayer;
            
            player.AddChild(this);
            SetXY(0, player.y - 640);

            if (player.flip)
            {
                speed = -speed;
                scaleX = -scaleX;
                x = 200;
            }
            else x += player.width/4;
        }

        void Update()
        {
            scaleX += speed;

            if (player.currentFrame >= 24 || player.currentFrame < 21)
            {
                LateDestroy();
            }
        }

        void OnCollision(GameObject other)
        {
            if (other is Hurtbox)
            {
                Hurtbox otherHurtbox = other as Hurtbox;

                if (otherHurtbox.playerID != player.playerID)
                {
                    otherHurtbox.damageTaken = 2;
                    otherHurtbox.isHit = true;
                }
            }
        }
    }
}
