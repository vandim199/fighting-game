using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Player1 : AnimationSprite
    {
        float _speedX, _speedY;
        float _timeJumped;

        public Player1() : base("spritesheet.png", 12, 1)
        {
            scale = 0.8f;
            SetXY(0, 200);
            SetCycle(0, 12, 5);
        }

        void Update()
        {
            if (_speedX != 0) Animate();

            if (Input.GetKey(Key.D)) _speedX = 6;
            else if (Input.GetKey(Key.A)) _speedX = -6;
            else _speedX = 0;

            if (Input.GetKeyDown(Key.W))
            {
                _timeJumped = Time.now;
                _speedY = 20;
            }

            if (Time.now >= _timeJumped + 200) _speedY = -20;

            MoveUntilCollision(_speedX, _speedY);
        }
    }
}
