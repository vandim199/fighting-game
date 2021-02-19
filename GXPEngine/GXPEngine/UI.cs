using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Text;

namespace GXPEngine
{
    class UI : Canvas
    {
        Canvas healthBar1 = new Canvas(700, 90);
        Canvas healthBar2 = new Canvas(700, 90);
        Canvas healthBarOutline1 = new Canvas(700, 90);
        Canvas healthBarOutline2 = new Canvas(700, 90);
        Canvas healthBarFill1 = new Canvas(700, 90);
        Canvas healthBarFill2 = new Canvas(700, 90);
        Player player1, player2;

        EasyDraw HPString = new EasyDraw(1920, 1080, false);
        EasyDraw Timer = new EasyDraw(1920, 1080, false);
        EasyDraw TimerBG = new EasyDraw(1920, 1080, false);

        public float remainingTime = 99;
        float timeStarted;

        AnimationSprite[] roundPills = new AnimationSprite[4];

        bool roundEnd = false;

        PrivateFontCollection fonts = new PrivateFontCollection();
        private Font knewave;

        bool player1won = false, player2won = false, draw = false;
        float timer1, timer2, timerDraw;

        Sprite dildoBG1 = new Sprite("ui\\gamefield\\DildoBackdrop.png");
        AnimationSprite[] dildoBar1 = new AnimationSprite[100];

        Sprite dildoBG2 = new Sprite("ui\\gamefield\\DildoBackdrop.png");
        AnimationSprite[] dildoBar2 = new AnimationSprite[100];

        public UI(Player _player1, Player _player2) : base(MyGame.main.width, MyGame.main.height, false)
        {
            fonts.AddFontFile("ui\\Knewave-Regular.ttf");
            knewave = new Font(fonts.Families[0], 100);

            player1 = _player1;
            player2 = _player2;

            AddChild(TimerBG);
            AddChild(Timer);

            timeStarted = Time.time / 1000;
            Console.WriteLine(timeStarted);

            for (int i = 0; i < roundPills.Length; i++)
            {
                int offset = 0;
                if (i > 1) offset = 1240;
                roundPills[i] = new AnimationSprite("ui\\gamefield\\round-pill.png", 1, 2, -1, false);
                roundPills[i].SetXY(150 + i * 100 + offset, 100);
                AddChild(roundPills[i]);
            }

            dildoBG1.scale = 2;
            AddChild(dildoBG1);
            for (int i = 0; i < 100; i++)
            {
                dildoBar1[i] = new AnimationSprite("ui\\gamefield\\DildoForeground.png", 100, 1);
                dildoBar1[i].SetXY(i * 6.5f, 0);
                dildoBar1[i].SetFrame(i);
                dildoBar1[i].scale = 2;
                AddChild(dildoBar1[i]);
            }

            dildoBG2.scale = 2;
            dildoBG2.SetXY(1275, 0);
            dildoBG2.Mirror(true, false);
            AddChild(dildoBG2);
            for (int i = 0; i < 100; i++)
            {
                dildoBar2[i] = new AnimationSprite("ui\\gamefield\\DildoForeground.png", 100, 1);
                dildoBar2[i].SetXY(1820 + (i * 6.5f - 100) * -1 , 0);
                dildoBar2[i].SetFrame(i);
                dildoBar2[i].scale = 2;
                dildoBar2[i].Mirror(true, false);
                AddChild(dildoBar2[i]);
            }
        }

        void Update()
        {
            player1.remainingTime = remainingTime;

            for (int i = 0; i < dildoBar1.Length; i++)
            {
                if (player1.hp >= i)
                {
                    dildoBar1[i].alpha = 1;
                }
                else dildoBar1[i].alpha = 0;
            }

            for (int i = 0; i < dildoBar2.Length; i++)
            {
                if (player2.hp >= i)
                {
                    dildoBar2[i].alpha = 1;
                }
                else dildoBar2[i].alpha = 0;
            }

            for (int i = 0; i < GameLoader.player1RoundsWon; i++)
            {
                roundPills[i].SetFrame(1);
            }

            for (int i = 0; i < GameLoader.player2RoundsWon; i++)
            {
                roundPills[i + 2].SetFrame(1);
            }

            if (player1.hp > 0) healthBar1.width = player1.hp * 7;
            else
            {
                GameLoader.lastRoundWinner = 2;
                healthBar1.width = 0;
            }

            if (player2.hp > 0) healthBar2.width = player2.hp * 7;
            else
            {
                GameLoader.lastRoundWinner = 1;
                healthBar2.width = 0;
            }

            HPString.Clear(Color.Transparent);
            HPString.Text(player1.hp.ToString(), 60, 100);
            HPString.Text(player2.hp.ToString(), 1800, 100);

            Timer.Clear(Color.Transparent);
            Timer.TextFont(knewave);
            Timer.Text(remainingTime.ToString(), 850, 250);
            //Timer.SetColor(0, 255, 240);
            Timer.color = 0xED3193;

            TimerBG.Clear(Color.Transparent);
            TimerBG.TextFont(knewave);
            TimerBG.Text(remainingTime.ToString(), 857, 257);
            TimerBG.color = 0x94D6DD;

            remainingTime = 99 - Time.time / 1000 + timeStarted;
            

            if (remainingTime <= 0 || GameLoader.player1.hp <= 0 || GameLoader.player2.hp <= 0)
            {
                if (!roundEnd)
                {
                    if (player1.hp > player2.hp)
                    {
                        AddChild(new PopUp("ui\\gamefield\\PLAYER 1 WON.png", -600, 150));
                        MyGame.P1Wins.Play();
                        player1won = true;
                        timer1 = Time.time;
                        roundEnd = true;
                    }
                    else if (player1.hp < player2.hp)
                    {
                        AddChild(new PopUp("ui\\gamefield\\PLAYER 2 WON.png", -550, 150));
                        MyGame.P2Wins.Play();
                        player2won = true;
                        timer2 = Time.time;
                        roundEnd = true;
                    }
                    else if (player1.hp == player2.hp)
                    {
                        AddChild(new PopUp("ui\\gamefield\\TIME OUT.png", -250));
                        MyGame.TimeOut.Play();
                        draw = true;
                        timerDraw = Time.time;
                        roundEnd = true;
                    }
                }
            }
            

            if (player1won && Time.time > timer1 + 1500)
            {
                GameLoader.player1RoundsWon++;
                player1won = false;
            }
            if (player2won && Time.time > timer2 + 2000)
            {
                GameLoader.player2RoundsWon++;
                player2won = false;
            }
            if (draw && Time.time > timerDraw + 2000)
            {
                GameLoader.player1RoundsWon++;
                GameLoader.player2RoundsWon++;
                draw = false;
            }
        }
    }
}
