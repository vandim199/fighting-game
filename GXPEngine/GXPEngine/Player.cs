using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using ExtensionMethods;

namespace GXPEngine
{
    class Player : AnimationSprite
    {
        float _speed = 50;
        float _speedX, _speedY = 20;
        float _timeJumped;
        bool canJump;
        bool _playingAnimation;
        public int onFrame;
        GameObject enemy;
        int posX;
        int playerID;
        public int numberOfHurtboxes;
        String moveset;

        int[] controller1 = {Key.W, Key.A, Key.S, Key.D, Key.E};
        int[] controller2 = {Key.UP, Key.LEFT, Key.DOWN, Key.RIGHT, Key.RIGHT_SHIFT};
        int[] controller;

        string BoobBitchX;
        string BoobBitchY;
        string BoobBitchW;
        string BoobBitchH;

        public Player(int playerNumber, Player newEnemy) : base("FilliaTest.png", 12, 3, -1, false, true)
        {
            scale = 0.7f;
            SetOrigin(width / 2, height / 2);

            if(playerNumber == 1)
            {
                posX = 0;
                controller = controller1;
            }
            if(playerNumber == 2)
            {
                posX = 1200;
                controller = controller2;
            }
            SetXY(posX, 0);

            enemy = newEnemy;

            playerID = playerNumber;

            //moveset = File.ReadAllText("Test Box.svg");
            //Console.WriteLine(moveset);
            //String[] moves = moveset.Split(new[] { "<rect " }, StringSplitOptions.RemoveEmptyEntries);
            //Console.WriteLine(moves[1]);

            string line;
            int counter = 0;

            System.IO.StreamReader file = new System.IO.StreamReader("Test Box.svg");
            while ((line = file.ReadLine()) != null)
            {
                if(line.StartsWith("<rect"))
                {
                    BoobBitchX = line.Between("x=\"", "\"");
                    BoobBitchY = line.Between("y=\"", "\"");
                    BoobBitchW = line.Between("width=\"", "\"");
                    BoobBitchH = line.Between("height=\"", "\"");
                }
                counter++;
            }
            Console.WriteLine(BoobBitchX);
            Console.WriteLine(BoobBitchY);
            Console.WriteLine(BoobBitchW);
            Console.WriteLine(BoobBitchH);
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
                if (x > enemy.x) scaleX = -0.7f;
                if (x < enemy.x) scaleX = 0.7f;
            }
            if (numberOfHurtboxes == 0)
            {
                if (currentFrame >= 0 && currentFrame <= 12)
                {
                    Hurtbox hurtbox = new Hurtbox(120, 150, 500, 700, currentFrame, playerID, this);
                }
                else if (currentFrame == 14)
                {
                    Hurtbox hurtbox = new Hurtbox(100, 100, 500, 700, currentFrame, playerID, this);

                    Hitbox hitbox = new Hitbox(600, 10, 400, 300, currentFrame, playerID, this);
                    AddChild(hitbox);
                }
                else if (currentFrame == 15)
                {
                    Hurtbox hurtbox = new Hurtbox(100, 100, 500, 700, currentFrame, playerID, this);

                    Hitbox hitbox = new Hitbox(550, 80, 350, 250, currentFrame, playerID, this);
                    AddChild(hitbox);
                }
                else if (currentFrame >= 24 && currentFrame <= 28)
                {
                    Hurtbox hurtbox = new Hurtbox(100, 280, 500, 450, currentFrame, playerID, this);
                }
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
                _speed = 18;
            }

            if (this.collider.GetCollisionInfo(GameLoader.floor.collider) != null)
            {
                y -= 5;
                canJump = true;
                _speed = 50;
            }
            else canJump = false;

            if (!_playingAnimation)
            {
                MoveUntilCollision(_speedX, _speedY, GameLoader.enviroment);
            }
        }

        private void combat()
        {
            if (Input.GetKeyDown(controller[4]))
            {
                SetCycle(12, 8, 5);
                _playingAnimation = true;
            }
            if (currentFrame == 19) _playingAnimation = false;
        }

        private void animation()
        {
            Animate();
            onFrame = currentFrame;

            if (!_playingAnimation)
            {
                if (_speedX != 0) SetCycle(0, 12, 5);
                
                else SetCycle(0, 1, 5);
                
            }

            if (Input.GetKey(controller[2]) && currentFrame != 28)
            {
                _playingAnimation = true;
                SetCycle(24, 5, 5);
            }
            else if (currentFrame == 28) SetCycle(28, 1, 5);
            if (Input.GetKeyUp(controller[2]))
            {
                SetCycle(0, 1, 5);
                _playingAnimation = false;
            }
        }
    }
}
