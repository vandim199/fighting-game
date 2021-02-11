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
        public static Canvas UI;
        public static GameObject[] enviroment = new GameObject[1];
        string characterFile;
        int characterColumns;
        int characterRows;
        double characterScale;
        int character;

        public GameLoader(int character1 = 1, int character2 = 1) : base(false)
        {
            new Backdrop("Gameplay.png");

            floor = new Canvas(1920, 200);
            floor.SetXY(0, 830);
            floor.graphics.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, 1920, 300));
            AddChild(floor);


            assignCharacters(character1);
            player1 = new Player(character1, 1, player2, characterFile, characterColumns, characterRows, characterScale);
            AddChild(player1);

            assignCharacters(character2);
            player2 = new Player(character2, 2, player1, characterFile, characterColumns, characterRows, characterScale);
            AddChild(player2);

            enviroment[0] = floor;

            UI = new UI();
        }

        void Update()
        {

        }

        void assignCharacters(int chosenCharacter)
        {
            character = chosenCharacter;

            switch (chosenCharacter)
            {
                case 1:
                    characterFile = "FilliaTest.png";
                    characterColumns = 12;
                    characterRows = 3;
                    characterScale = 0.7;
                    break;
                case 2:
                    characterFile = "BoobBitch.png";
                    characterColumns = 9;
                    characterRows = 2;
                    characterScale = 0.55;
                    break;
            }
        }
    }
}