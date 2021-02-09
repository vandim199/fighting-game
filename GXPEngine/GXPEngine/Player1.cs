using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GXPEngine
{
    class Player1 : AnimationSprite
    {
        float _speed = 7;
        float _speedX, _speedY;
        float _timeJumped;
        public static int onFrame;

        public Player1() : base("Fillia Kick.png", 8, 1, -1, false, false)
        {
            scale = 0.8f;
            SetXY(0, 200);
            SetCycle(0, 8, 5);
        }

        void Update()
        {
            onFrame = currentFrame;
            //if (_speedX != 0) Animate();

            if (Input.GetKey(Key.D)) _speedX = _speed;
            else if (Input.GetKey(Key.A)) _speedX = -_speed;
            else _speedX = 0;

            if (Input.GetKeyDown(Key.W))
            {
                _timeJumped = Time.now;
                //_speedY = 20;
            }

            //if (Time.now >= _timeJumped + 200) _speedY = -20;

            MoveUntilCollision(_speedX, _speedY);

            if (Input.GetKey(Key.RIGHT)) Animate();

            if (currentFrame == 2)
            {
                Hurtbox hurtbox = new Hurtbox(100, 0, 500, 700, currentFrame);
                AddChild(hurtbox);

                Hitbox hitbox = new Hitbox(600, 10, 400, 300, currentFrame);
                AddChild(hitbox);
            }
            else if (currentFrame == 3)
            {
                Hurtbox hurtbox = new Hurtbox(100, 0, 500, 700, currentFrame);
                AddChild(hurtbox);

                Hitbox hitbox = new Hitbox(550, 80, 350, 250, currentFrame);
                AddChild(hitbox);
            }
        }
    }
}
