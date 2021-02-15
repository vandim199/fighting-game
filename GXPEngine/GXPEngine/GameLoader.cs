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
        public static Canvas wallLeft;
        public static Canvas wallRight;
        public static Canvas UI;
        public static GameObject[] enviroment = new GameObject[3];
        // = new GameObject[3]
        string characterFile;
        int characterColumns;
        int characterRows;
        double characterScale;
        int character;

        public GameLoader(int character1 = 1, int character2 = 1) : base(false)
        {
            new Backdrop("Gameplay.png");

            floor = new Canvas(1920, 200);
            floor.SetXY(0, 880);
            //floor.graphics.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, 1920, 300));
            AddChild(floor);

            wallLeft = new Canvas(100, 1080);
            wallLeft.SetXY(-200, 0);
            wallLeft.graphics.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, wallLeft.width, wallLeft.height));
            AddChild(wallLeft);

            wallRight = new Canvas(100, 1080);
            wallRight.SetXY(2050, 0);
            wallRight.graphics.FillRectangle(new SolidBrush(Color.Black), new Rectangle(0, 0, wallRight.width, wallRight.height));
            AddChild(wallRight);

            enviroment = new GameObject[] { floor, wallLeft, wallRight };

            assignCharacters(character1);
            player1 = new Player(character1, 1, player2, characterFile, characterColumns, characterRows, characterScale);
            AddChild(player1);

            assignCharacters(character2);
            player2 = new Player(character2, 2, player1, characterFile, characterColumns, characterRows, characterScale);
            AddChild(player2);

            UI = new UI(player1, player2);
            AddChild(UI);
        }

        void Update()
        {
            if (Input.GetKeyDown(Key.P))
            {
                game.AddChild(new CharacterSelect());
                LateDestroy();
            }
        }

        void assignCharacters(int chosenCharacter)
        {
            character = chosenCharacter;

            switch (chosenCharacter)
            {
                case 1:
                    characterFile = "BoobBitch.png";
                    characterColumns = 7;
                    characterRows = 4;
                    characterScale = 0.55;
                    break;
                case 2:
                    characterFile = "BoobBitchBlue.png";
                    characterColumns = 7;
                    characterRows = 4;
                    characterScale = 0.55;
                    break;
                case 3:
                    characterFile = "FilliaTest.png";
                    characterColumns = 12;
                    characterRows = 3;
                    characterScale = 0.7;
                    break;
            }
        }
    }
}