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
        bool isHurtbox;
        bool isHitbox;

        public HurtboxCreator(int newX, int newY, int newW, int newH, string newColor, int newFrame, int newDuration, Player newPlayer) : base()
        {
            X = newX;
            Y = newY;
            W = newW;
            H = newH;
            Frame = newFrame;
            Duration = newDuration;
            player = newPlayer;
            player.AddChild(this);

            if(newColor == "00ff00")
            {
                isHurtbox = true;
                Console.WriteLine("it's a hurtbox");
            }
            if (newColor == "ff0000")
            {
                isHitbox = true;
                Console.WriteLine("it's a hitbox");
            }
        }

        void Update()
        {
            if (player.numberOfHurtboxes == 0)
            {
                if (player.currentFrame >= Frame && player.currentFrame < Duration)
                {
                    if (isHurtbox)
                    {
                        new Hurtbox(X, Y, W, H, player.currentFrame, player.playerID, player);
                    }
                }
            }

            if (player.numberOfHitboxes == 0)
            {
                if (player.currentFrame >= Frame && player.currentFrame < Duration)
                {
                    if (isHitbox)
                    {
                        new Hitbox(X, Y, W, H, player.currentFrame, player.playerID, player);
                    }
                }
            }
        }
    }
}
