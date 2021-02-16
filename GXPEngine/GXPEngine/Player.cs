using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class Player : AnimationSprite
    {
        private float _speed = 50;
        private float _speedX, _speedY = 20;
        private float _timeJumped;
        private bool _canJump;
        private bool _playingAnimation;
        private bool _crouching = false;
        public bool isLeft, flip = false;
        private Player _enemy;
        private int _posX;
        public int playerID;
        public int numberOfHurtboxes = 0;
        public int numberOfHitboxes = 0;
        public int hp = 100;
        private float _scale;
        public bool startInvulnerable = false;
        public bool invulnerable = false;
        private float _timeInvulnerable = 100000000;
        public bool isHit = false;
        private bool _isBlocking = false;
        public int damageTaken = 0;
        private float _blockingStun = 10000000;
        private bool _holdFlip;
        private bool _isAttacking = false;

        private int[] _controller1 = {Key.W, Key.A, Key.S, Key.D, Key.E, Key.Q};
        private int[] _controller2 = {Key.UP, Key.LEFT, Key.DOWN, Key.RIGHT, Key.RIGHT_SHIFT, Key.DELETE};
        private int[] _controller;

        //            _ = start   ^ = end      idle  walk   attack crouch hit   block  kick   laser
        //                                     i_ i^ w_ w^  a_ a^  c_ c^ h_ h^  b_ b^  k_ k^  l_ l^
        private int[] _animationsBoobBitch = { 0, 7, 13, 5, 7, 5, 12, 1, 11, 1, 18, 1, 19, 4, 23, 7};
        private int[] _animationsFillia = { 0, 2, 0, 12, 12, 8, 24, 5 };
        private int[] _animations;

        private GameObject _character;

        public Player(int newCharacter, int newPlayerNumber, Player newEnemy, string newPlayerSprite, int newColumns = 1, int newRows = 1, double newScale = 1) : base(newPlayerSprite, newColumns, newRows, -1, false, true)
        {
            _scale = (float)newScale;
            scale = _scale;
            SetOrigin(width / 2, height / 2);

            if(newPlayerNumber == 1)
            {
                _posX = 100;
                _controller = _controller1;
            }
            if(newPlayerNumber == 2)
            {
                _posX = 1600;
                _controller = _controller2;
            }
            SetXY(_posX, 0);
            
            if (newCharacter == 1 || newCharacter == 2 || newCharacter == 3)
            {
                _animations = _animationsBoobBitch;
                _character = new CharacterMoveset(this, "BoobBitch.svg");
            }
            if (newCharacter == 4)
            {
                _animations = _animationsFillia;
                _character = new CharacterMoveset(this, "Test Box 3.svg");
            }

            _enemy = newEnemy;

            playerID = newPlayerNumber;
        }

        void Update()
        {
            movement();
            combat();
            animation();
            flipCharacters();
            hitInteraction();
        }

        private void movement()
        {
            if (Input.GetKey(_controller[3])) _speedX = _speed;
            else if (Input.GetKey(_controller[1])) _speedX = -_speed;
            else _speedX = 0;

            if (Input.GetKey(_controller[0]) && _canJump)
            {
                _timeJumped = Time.now;
                _speedY = -20;
            }

            if (Time.now >= _timeJumped + 200)
            {
                _speedY = 20;
            }

            if (!_canJump)
            {
                _speed = 14;
            }

            if (this.collider.GetCollisionInfo(GameLoader.floor.collider) != null)
            {
                y -= 5;
                _canJump = true;
                _speed = 50;
            }
            else _canJump = false;

            for (int i = 0; i < GameLoader.enviroment.Length; i++)
            {
                if (GameLoader.enviroment[i].collider.GetCollisionInfo(this.collider) != null)
                {
                    this.x -= GameLoader.enviroment[i].collider.GetCollisionInfo(this.collider).normal.x * 3;
                }
            }

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
            if (Input.GetKeyDown(_controller[4]) && !_crouching)
            {
                SetCycle(_animations[4], _animations[5], 5);
                _playingAnimation = true;
            }
            if (currentFrame == _animations[4] + _animations[5] - 1) _playingAnimation = false;
            
            if (_crouching && Input.GetKeyDown(_controller[4]))
            {
                SetCycle(_animations[12], _animations[13], 7);
                _isAttacking = true;
            }

            if (currentFrame == _animations[12] + _animations[13] - 1)
            {
                _isAttacking = false;
                SetFrame(_animations[6] + _animations[7] - 1);
            }

            if (Input.GetKeyDown(_controller[5]))
            {
                SetCycle(_animations[14], _animations[15], 5);
                _playingAnimation = true;
            }

            if (currentFrame == _animations[14] + _animations[15] - 1)
            {
                _playingAnimation = false;
            }
        }

        private void animation()
        {
            Animate();

            if (!_playingAnimation)
            {
                if (_speedX != 0) SetCycle(_animations[2], _animations[3], 7);
                else if (!invulnerable) SetCycle(_animations[0], _animations[1], 7);
            }

            if (_canJump != false)
            {
                if (Input.GetKey(_controller[2]) && currentFrame != 28 && !isHit)
                {
                    _playingAnimation = true;
                    _crouching = true;
                    SetCycle(_animations[6], _animations[7], 5);
                }
            }
            if (currentFrame == _animations[6] + _animations[7] - 1) SetCycle(_animations[6] + _animations[7] - 1, 1, 5);
            if (!Input.GetKey(_controller[2]) && _crouching && !_isAttacking)
            {
                SetCycle(0, 1, 5);
                _crouching = false;
                _playingAnimation = false;
            }
        }

        void flipCharacters()
        {
            if (_enemy == null)
            {
                _enemy = GameLoader.player2;
            }

            if (_enemy != null)
            {
                if (x < _enemy.x)
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
        }

        void hitInteraction()
        {
            if (startInvulnerable)
            {
                _timeInvulnerable = Time.time;
                invulnerable = true;
                isHit = true;
                startInvulnerable = false;
            }

            if (Time.time > _timeInvulnerable + 600)
            {
                isHit = false;
                invulnerable = false;
                _timeInvulnerable = 1000000000;
            }

            if (invulnerable)
            {
                SetCycle(_animations[8], _animations[9], 5);
                _crouching = false;
                _isAttacking = false;
            }

            if (_canJump || _crouching)
            {
                if (Input.GetKey(_controller[1]) && !flip)
                {
                    _isBlocking = true;
                }
                else if (Input.GetKey(_controller[3]) && flip)
                {
                    _isBlocking = true;
                }
                else _isBlocking = false;
            }
            else _isBlocking = false;

            if (damageTaken != 0)
            {
                _holdFlip = flip;
                if (_isBlocking && !invulnerable)
                {
                    _blockingStun = Time.time;
                    isHit = true;
                }
                else
                {
                    hp -= damageTaken;
                    startInvulnerable = true;
                }
                damageTaken = 0;
            }

            if (Time.time > _blockingStun + 300)
            {
                isHit = false;
                _blockingStun = 100000000;
                _crouching = false;
                _playingAnimation = false;
            }

            if (_isBlocking && isHit && !invulnerable)
            {
                SetCycle(_animations[10], _animations[11], 5);
                flip = _holdFlip;
                _isBlocking = true;
            }
        }
    }
}