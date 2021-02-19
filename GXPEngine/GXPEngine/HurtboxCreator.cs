using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class HurtboxCreator : GameObject
    {
        Player player;
        int X, Y, W, H, Frame, Duration;
        bool isHurtbox = false;
        bool isHitbox = false;
        bool canShoot = true;
        int damage;

        public HurtboxCreator(int newX, int newY, int newW, int newH, string newColor, int newFrame, int newDuration, Player newPlayer, int newDamage = 1) : base()
        {
            X = newX;
            Y = newY;
            W = newW;
            H = newH;
            Frame = newFrame;
            Duration = newDuration;
            player = newPlayer;
            damage = newDamage;

            player.AddChild(this);

            if(newColor == "00ff00")
            {
                isHurtbox = true;
            }
            if (newColor == "ff0000")
            {
                isHitbox = true;
            }
        }

        void Update()
        {
            if (player.numberOfHurtboxes == 0)
            {
                if (player.currentFrame >= Frame && player.currentFrame < Frame + Duration)
                {
                    if (isHurtbox && !player.isHit)
                    {
                        new Hurtbox(X, Y, W, H, player.currentFrame, player.playerID, player, player.flip);
                    }
                }
            }

            if (player.numberOfHitboxes == 0)
            {
                if (player.currentFrame >= Frame && player.currentFrame < Frame + Duration)
                {
                    if (isHitbox)
                    {
                        new Hitbox(X, Y, W, H, player.currentFrame, player.playerID, player, player.flip, damage);
                    }
                }
            }

            if (player.currentFrame == 21 && canShoot)
            {
                new RangedAttack(player);
                canShoot = false;
            }
            else if (player.currentFrame != 21) canShoot = true;
        }
    }
}