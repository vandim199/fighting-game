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
        public static int player1RoundsWon = 0, player2RoundsWon = 0, totalRounds = 0, previousTotalRounds = 0;
        int character1, character2;
        public static int lastRoundWinner = 0;

        public GameLoader(int newCharacter1 = 1, int newCharacter2 = 2, int p1Rounds = 0, int p2Rounds = 0, int newLastRoundWinner = 0) : base(false)
        {
            AddChild(new Backdrop("assets\\Background.png"));
            AddChild(new PopUp("ui\\gamefield\\FIGHT.png", -50));

            switch (p1Rounds + p2Rounds)
            {
                case 0:
                    MyGame.Round1.Play();
                    break;
                case 1:
                    MyGame.Round2.Play();
                    break;
                case 2:
                    MyGame.Round3.Play();
                    break;
            }

            character1 = newCharacter1;
            character2 = newCharacter2;
            player1RoundsWon = p1Rounds;
            player2RoundsWon = p2Rounds;
            Console.WriteLine("P1: " + player1RoundsWon + " P2: " + player2RoundsWon);

            floor = new Canvas(1920, 200);
            floor.SetXY(0, 1050);
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
                game.AddChild(new MainMenu());
                LateDestroy();
            }

            if (player1RoundsWon == 2 || player2RoundsWon == 2)
            {
                game.AddChild(new MainMenu());
                LateDestroy();
            }

            totalRounds = player1RoundsWon + player2RoundsWon;

            if(totalRounds > previousTotalRounds)
            {
                LateDestroy();
                game.AddChild(new GameLoader(character1, character2, player1RoundsWon, player2RoundsWon, lastRoundWinner));
                previousTotalRounds++;
            }

            if (player1.startInvulnerable || player2.startInvulnerable)
            {
                game.SetXY(Utils.Random(-10, 10), Utils.Random(-10, 10));
            }
            if (!player1.startInvulnerable && !player2.startInvulnerable)
            {
                game.SetXY(0, 0);
            }
        }

        void assignCharacters(int chosenCharacter)
        {
            character = chosenCharacter;

            characterColumns = 7;
            characterRows = 7;
            characterScale = 0.55;

            switch (chosenCharacter)
            {
                case 1:
                    characterFile = "assets/BoobBitch.png";
                    break;
                case 2:
                    characterFile = "assets/BoobBitchBlue.png";
                    break;
                case 3:
                    characterFile = "assets/BoobBitchPurple.png";
                    break;
            }
        }
    }
}