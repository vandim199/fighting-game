using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Player : AnimationSprite
    {
        float _speed = 50;
        float _speedX, _speedY = 20;
        float _timeJumped;
        bool canJump;
        bool _playingAnimation;
        bool crouching = false;
        public bool isLeft, flip = false;
        Player enemy;
        int posX;
        public int playerID;
        public int numberOfHurtboxes = 0;
        public int numberOfHitboxes = 0;
        public int hp = 100;
        float _scale;
        public bool startInvulnerable = false;
        public bool invulnerable = false;
        float timeInvulnerable = 100000000;
        public bool isHit = false;
        bool isBlocking = false;
        public int damageTaken = 0;
        float blockingStun = 10000000;
        bool holdFlip;
        bool isAttacking = false;

        int[] controller1 = {Key.W, Key.A, Key.S, Key.D, Key.E};
        int[] controller2 = {Key.UP, Key.LEFT, Key.DOWN, Key.RIGHT, Key.RIGHT_SHIFT};
        int[] controller;

        //   _ = start   ^ = end      idle  walk   attack crouch hit   block  kick
        //                            i_ i^ w_ w^  a_ a^  c_ c^ h_ h^  b_ b^  k_ k^
        int[] animationsBoobBitch = { 0, 7, 13, 5, 7, 5, 12, 1, 11, 1, 18, 1, 19, 4};
        int[] animationsFillia = { 0, 2, 0, 12, 12, 8, 24, 5 };
        int[] animations;

        GameObject character;

        public Player(int _character, int playerNumber, Player newEnemy, string playerSprite, int columns = 1, int rows = 1, double _newScale = 1) : base(playerSprite, columns, rows, -1, false, true)
        {
            _scale = (float)_newScale;
            scale = _scale;
            SetOrigin(width / 2, height / 2);

            if(playerNumber == 1)
            {
                posX = 100;
                controller = controller1;
            }
            if(playerNumber == 2)
            {
                posX = 1600;
                controller = controller2;
            }
            SetXY(posX, 0);

            if (_character == 1)
            {
                animations = animationsFillia;
                character = new CharacterMoveset(this, "Test Box 3.svg");
            }
            if (_character == 2)
            {
                animations = animationsBoobBitch;
                character = new CharacterMoveset(this, "BoobBitch.svg");
            }

            enemy = newEnemy;

            playerID = playerNumber;
        }

        void Update()
        {
            movement();
            combat();
            animation();

            if (enemy == null)
            {
                enemy = GameLoader.player2;
            }

            if (enemy != null)
            {
                if (x < enemy.x)
                {
                    isLeft = true;
                }
                else isLeft = false;

                if (!isLeft)
                {
                    Mirror(true, false);
                    flip = true;
                }
                else
                {
                    Mirror(false, false);
                    flip = false;
                }
            }

            if (damageTaken != 0)
            {
                holdFlip = flip;
                if (isBlocking && !invulnerable) 
                {
                    blockingStun = Time.time;
                    isHit = true;
                }
                else
                {
                    hp -= damageTaken;
                    startInvulnerable = true;
                }
                damageTaken = 0;
            }

            if (Time.time > blockingStun + 300)
            {
                isHit = false;
                blockingStun = 100000000;
                crouching = false;
                _playingAnimation = false;
            }

            if (isBlocking && isHit && !invulnerable)
            {
                SetCycle(animations[10], animations[11], 5);
                flip = holdFlip;
                isBlocking = true;
            }
        }

        private void movement()
        {
            if (Input.GetKey(controller[3])) _speedX = _speed;
            else if (Input.GetKey(controller[1])) _speedX = -_speed;
            else _speedX = 0;

            if (Input.GetKey(controller[0]) && canJump)
            {
                _timeJumped = Time.now;
                _speedY = -20;
            }

            if (Time.now >= _timeJumped + 200)
            {
                _speedY = 20;
            }

            if (!canJump)
            {
                _speed = 14;
            }

            if (this.collider.GetCollisionInfo(GameLoader.floor.collider) != null)
            {
                y -= 5;
                canJump = true;
                _speed = 50;
            }
            else canJump = false;

            if (isHit)
            {
                _speedX = 0;
            }

            if (!_playingAnimation)
            {
                MoveUntilCollision(_speedX, _speedY, GameLoader.enviroment);
            }
        }

        private void combat()
        {
            if (Input.GetKeyDown(controller[4]) && !crouching)
            {
                SetCycle(animations[4], animations[5], 5);
                _playingAnimation = true;
            }
            if (currentFrame == animations[4] + animations[5] - 1) _playingAnimation = false;

            if (startInvulnerable)
            {
                timeInvulnerable = Time.time;
                invulnerable = true;
                isHit = true;
                startInvulnerable = false;
            }

            if (Time.time > timeInvulnerable + 600)
            {
                isHit = false;
                invulnerable = false;
                timeInvulnerable = 1000000000;
            }

            if (invulnerable)
            {
                SetCycle(animations[8], animations[9], 5);
            }
            if (canJump || crouching)
            {
                if (Input.GetKey(controller[1]) && !flip)
                {
                    isBlocking = true;
                }
                else if (Input.GetKey(controller[3]) && flip)
                {
                    isBlocking = true;
                }
                else isBlocking = false;
            }
            else isBlocking = false;

            if (crouching && Input.GetKeyDown(controller[4]))
            {
                SetCycle(animations[12], animations[13], 7);
                isAttacking = true;
            }
            if (currentFrame == animations[12] + animations[13] - 1)
            {
                isAttacking = false;
                SetFrame(animations[6] + animations[7] - 1);
            }
        }

        private void animation()
        {
            Animate();

            if (!_playingAnimation)
            {
                if (_speedX != 0) SetCycle(animations[2], animations[3], 5);
                else if (!invulnerable) SetCycle(animations[0], animations[1], 7);
            }

            if (canJump != false)
            {
                if (Input.GetKey(controller[2]) && currentFrame != 28 && !isHit)
                {
                    _playingAnimation = true;
                    crouching = true;
                    SetCycle(animations[6], animations[7], 5);
                }
            }
            if (currentFrame == animations[6] + animations[7] - 1) SetCycle(animations[6] + animations[7] - 1, 1, 5);
            if (!Input.GetKey(controller[2]) && crouching && !isAttacking)
            {
                SetCycle(0, 1, 5);
                crouching = false;
                _playingAnimation = false;
            }
        }
    }
}