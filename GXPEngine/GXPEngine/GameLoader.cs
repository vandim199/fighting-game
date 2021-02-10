using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GXPEngine
{
    class GameLoader : GameObject
    {
        public static Player player1;
        public static Player player2;
        public static Canvas floor;
        public static GameObject[] enviroment = new GameObject[1];

        public GameLoader() : base(false)
        {
            new Backdrop("Gameplay.png");

            floor = new Canvas(1920, 200);
            floor.SetXY(0, 830);
            floor.graphics.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, 1920, 300));
            AddChild(floor);

            player1 = new Player(1, player2);
            AddChild(player1);

            player2 = new Player(2, player1);
            AddChild(player2);

            enviroment[0] = floor;
        }

        void Update()
        {
            
        }
    }
}
