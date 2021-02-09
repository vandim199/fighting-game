using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GXPEngine
{
    class GameLoader : GameObject
    {
        Player1 player1;

        public GameLoader() : base(false)
        {
            new Backdrop("Gameplay.png");
            player1 = new Player1();
            AddChild(player1);

            Canvas canvas = new Canvas(1920, 200);
            canvas.SetXY(0, 830);
            canvas.graphics.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, 1920, 300));
            AddChild(canvas);
        }

        void Update()
        {
            
        }
    }
}
