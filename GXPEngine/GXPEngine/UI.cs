using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

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

        public float remainingTime = 99;
        float timeStarted;

        public UI(Player _player1, Player _player2) : base(MyGame.main.width, MyGame.main.height, false)
        {
            healthBarFill1.graphics.FillRectangle(new SolidBrush(Color.DarkRed), new Rectangle(0, 0, healthBar1.width, healthBar1.height));
            healthBarFill1.SetXY(50, 70);
            AddChild(healthBarFill1);

            healthBar1.graphics.FillRectangle(new SolidBrush(Color.LimeGreen), new Rectangle(0, 0, healthBar1.width, healthBar1.height));
            healthBar1.SetXY(50, 70);
            AddChild(healthBar1);
            
            healthBarOutline1.graphics.DrawRectangle(new Pen(Color.Black, 6), new Rectangle(0, 0, healthBar1.width, healthBar1.height));
            healthBarOutline1.SetXY(healthBar1.x, healthBar1.y);
            AddChild(healthBarOutline1);

            healthBarFill2.graphics.FillRectangle(new SolidBrush(Color.DarkRed), new Rectangle(0, 0, healthBar2.width, healthBar2.height));
            healthBarFill2.SetXY(1150, 70);
            AddChild(healthBarFill2);

            healthBar2.graphics.FillRectangle(new SolidBrush(Color.LimeGreen), new Rectangle(0, 0, healthBar2.width, healthBar2.height));
            healthBar2.SetXY(1150, 70);
            AddChild(healthBar2);

            healthBarOutline2.graphics.DrawRectangle(new Pen(Color.Black, 6), new Rectangle(0, 0, healthBar2.width, healthBar2.height));
            healthBarOutline2.SetXY(healthBar2.x, healthBar2.y);
            AddChild(healthBarOutline2);

            player1 = _player1;
            player2 = _player2;
            
            AddChild(HPString);
            AddChild(Timer);

            timeStarted = Time.time / 1000;
            Console.WriteLine(timeStarted);
        }

        void Update()
        {
            if (player1.hp > 0) healthBar1.width = player1.hp * 7;
            else
            {
                healthBar1.width = 0;
                player1.LateDestroy();
            }

            if (player2.hp > 0) healthBar2.width = player2.hp * 7;
            else
            {
                healthBar2.width = 0;
                player2.LateDestroy();
            }

            HPString.Clear(Color.Transparent);
            HPString.Text(player1.hp.ToString(), 60, 100);
            HPString.Text(player2.hp.ToString(), 1800, 100);

            Timer.Clear(Color.Transparent);
            Timer.Text(remainingTime.ToString(), 850, 150);
            Timer.TextSize(70);
            
            remainingTime = 99 - Time.time / 1000 + timeStarted;
        }
    }
}
