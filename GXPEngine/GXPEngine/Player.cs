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
        public bool _playingAnimation;
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
        private bool _walkingForward = false;
        private bool _jumping = false;
        private float stepsDelay = 10000000;
        private bool stepMade = false;
        public float remainingTime;

        private int[] _controller1 = {Key.W, Key.A, Key.S, Key.D, Key.E, Key.Q};
        private int[] _controller2 = {Key.UP, Key.LEFT, Key.DOWN, Key.RIGHT, Key.RIGHT_SHIFT, Key.ENTER};
        private int[] _controller;

        //                                     0  1  2   3  4  5   6  7  8  9   10  11 12  13 14  15 16  17 18  19 20  21 22  23
        //            _ = start   ^ = end      idle  walk   attack crouch hit   block  kick   laser highKick jump  inAir  land
        //                                     i_ i^ w_ w^  a_ a^  c_ c^ h_ h^  b_ b^  k_ k^  l_ l^  hk_hk^ j_ j^  a_ a^  l_ l^
        private int[] _animationsBoobBitch = { 0, 7, 12, 5, 7, 5, 36, 3, 11, 1, 17, 1, 39, 5, 19, 6, 25, 6, 30, 2, 32, 2, 34, 2};
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
                _character = new CharacterMoveset(this, "assets\\BoobBitch.svg");
            }

            _enemy = newEnemy;

            playerID = newPlayerNumber;

            stepsDelay = Time.time;
        }

        void Update()
        {
            movement();
            combat();
            animation();
            flipCharacters();
            hitInteraction();

            if (_playingAnimation)
            {
                _jumping = false;
            }

            if (remainingTime == 98 && playerID == 1)
            {
                _enemy = null;
            }
        }

        private void movement()
        {
            if (Input.GetKey(_controller[3])) _speedX = _speed;
            else if (Input.GetKey(_controller[1])) _speedX = -_speed;
            else _speedX = 0;

            if (_speedX != 0 && !stepMade)
            {
                MyGame.Walking.Play();
                stepsDelay = Time.time;
                stepMade = true;
            }

            if (Time.time > stepsDelay + 300)
            {
                stepMade = false;
            }

            if (Input.GetKey(_controller[0]) && _canJump && !_jumping)
            {
                _jumping = true;
                SetCycle(_animations[18], _animations[19] + 1, 10);
            }

            if (currentFrame == _animations[18] + 1)
            {
                _timeJumped = Time.now;
                _speedY = -30;
                SetCycle(_animations[20], _animations[21], 5);
            }

            if (_jumping && _canJump && currentFrame > _animations[20])
            {
                SetCycle(_animations[22], _animations[23] + 1, 10);
            }
            if (currentFrame == _animations[22] + _animations[23] - 1)
            {
                _jumping = false;
            }

            if (Time.now >= _timeJumped + 200)
            {
                _speedY = 30;
            }

            if (_jumping)
            {
                _speed = 9;
                if (isHit)
                {
                    _jumping = false;
                }
            }

            if (this.collider.GetCollisionInfo(GameLoader.floor.collider) != null)
            {
                y -= 8;
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
                MoveUntilCollision(_speedX * Time.deltaTime * 0.1f, _speedY * Time.deltaTime * 0.08f, GameLoader.enviroment);
            }
        }

        private void combat()
        {
            //Normal Punch
            if (Input.GetKeyDown(_controller[4]) && !_crouching && !_playingAnimation)
            {
                MyGame.Whoosh.Play();
                SetCycle(_animations[4], _animations[5], 5);
                _playingAnimation = true;
            }
            if (currentFrame == _animations[4] + _animations[5] - 1) _playingAnimation = false;
            
            //Crouch Kick
            if (_crouching && Input.GetKeyDown(_controller[4]))
            {
                MyGame.Whoosh.Play();
                SetCycle(_animations[12], _animations[13], 7);
                _isAttacking = true;
            }

            if (currentFrame == _animations[12] + _animations[13] - 1)
            {
                _isAttacking = false;
                SetFrame(_animations[6] + _animations[7] - 1);
            }

            //Laser Beam
            if (Input.GetKeyDown(_controller[5]) && !_walkingForward && !_playingAnimation)
            {
                MyGame.Laser.Play(false, 0, 0.3f);
                SetCycle(_animations[14], _animations[15], 7);
                _playingAnimation = true;
            }
            if (currentFrame == _animations[14] + _animations[15] - 1)
            {
                _playingAnimation = false;
            }

            //High Kick
            if (_speedX > 0 && !flip)
            {
                _walkingForward = true;
            }
            else if (_speedX < 0 && flip)
            {
                _walkingForward = true;
            }
            else _walkingForward = false;

            if (_walkingForward && Input.GetKeyDown(_controller[5]))
            {
                MyGame.Whoosh.Play();
                SetCycle(_animations[16], _animations[17], 8);
                _playingAnimation = true;
            }
            if (currentFrame == _animations[16] + _animations[17] - 1)
            {
                _playingAnimation = false;
            }
        }

        private void animation()
        {
            Animate();

            if (!_playingAnimation && _canJump && !_jumping)
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
                MyGame.Punch.Play(false, 0, 0.5f);
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

            if (damageTaken != 0)
            {
                _holdFlip = flip;
                if (_isBlocking && !invulnerable)
                {
                    _blockingStun = Time.time;
                    MyGame.Block.Play(false, 0, 0.2f);
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