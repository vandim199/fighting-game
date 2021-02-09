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
        public static Canvas floor;

        public GameLoader() : base(false)
        {
            new Backdrop("Gameplay.png");

            floor = new Canvas(1920, 200);
            floor.SetXY(0, 830);
            floor.graphics.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, 1920, 300));
            AddChild(floor);

            player1 = new Player1();
            AddChild(player1);
        }

        void Update()
        {
            
        }
    }
}
