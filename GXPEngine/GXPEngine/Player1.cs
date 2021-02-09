using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GXPEngine
{
    class Player1 : AnimationSprite
    {
        float _speed = 50;
        float _speedX, _speedY = 20;
        float _timeJumped;
        bool canJump;
        bool _playingAnimation;
        public static int onFrame;

        public Player1() : base("FilliaTest.png", 12, 2, -1, false, true)
        {
            scale = 0.8f;
            SetXY(0, 0);
        }

        void Update()
        {
            Animate();
            onFrame = currentFrame;

            if (Input.GetKeyDown(Key.J))
            {
                SetCycle(12, 8, 5);
                _playingAnimation = true;
            }
            if (currentFrame == 19) _playingAnimation = false;

            if (!_playingAnimation)
            {
                if (_speedX != 0) SetCycle(0, 12, 5);
                else SetCycle(0, 1, 5);
            }

            if (Input.GetKey(Key.D)) _speedX = _speed;
            else if (Input.GetKey(Key.A)) _speedX = -_speed;
            else _speedX = 0;

            if (Input.GetKey(Key.W) && canJump)
            {
                _timeJumped = Time.now;
                _speedY = -20;
            }

            if(Time.now >= _timeJumped + 200)
            {
                _speedY = 20;
            }

            if (!canJump)
            {
                _speed = 20;
            }

            if (this.collider.GetCollisionInfo(GameLoader.floor.collider) != null)
            {
                y -= 5;
                canJump = true;
                _speed = 50;
            }
            else canJump = false;

            MoveUntilCollision(_speedX, _speedY);

            if (Input.GetKey(Key.RIGHT)) Animate();

            if (currentFrame == 14)
            {
                Hurtbox hurtbox = new Hurtbox(100, 0, 500, 700, currentFrame);
                AddChild(hurtbox);

                Hitbox hitbox = new Hitbox(600, 10, 400, 300, currentFrame);
                AddChild(hitbox);
            }
            else if (currentFrame == 15)
            {
                Hurtbox hurtbox = new Hurtbox(100, 0, 500, 700, currentFrame);
                AddChild(hurtbox);

                Hitbox hitbox = new Hitbox(550, 80, 350, 250, currentFrame);
                AddChild(hitbox);
            }
        }
    }
}
