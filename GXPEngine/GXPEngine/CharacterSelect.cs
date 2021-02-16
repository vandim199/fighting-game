using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class CharacterSelect : Sprite
    {
        int player1Selection = 1;
        int player2Selection = 2;
        public GameLoader round;
        AnimationSprite characterPortrait = new AnimationSprite("CharacterIcons.png", 3, 2);
        AnimationSprite characterPortrait2 = new AnimationSprite("CharacterIcons.png", 3, 2);
        int[] frameSelector = new int[2];
        private int[] controller1 = {Key.A, Key.D};
        private int[] controller2 = {Key.LEFT, Key.RIGHT};

        public CharacterSelect() : base("ui\\character-selection\\charselect-main.png")
        {

            characterPortrait.scale = 0.5f;
            characterPortrait.SetOrigin(characterPortrait.width / 2, characterPortrait.height / 2);
            characterPortrait.SetXY(200, 400);
            AddChild(characterPortrait);

            characterPortrait2.scale = 0.5f;
            characterPortrait2.SetOrigin(characterPortrait2.width / 2, characterPortrait2.height / 2);
            characterPortrait2.SetXY(1500, 400);
            AddChild(characterPortrait2);
        }

        void Update()
        {
            MenuButtons();
            PlayerInput();

            player1Selection = PlayerSelector(1);
            characterPortrait.SetFrame(player1Selection - 1);

            player2Selection = PlayerSelector(2);
            characterPortrait2.SetFrame(player2Selection - 1);
        }

        void MenuButtons(){
            // when Q is pressed the player will go back to the mainmenu
            if (Input.GetKeyDown(Key.Q))
            {
                Console.WriteLine("Back.");
                game.AddChild(new MainMenu());
                this.LateDestroy();
            }

            //when E is pressed the game will start
            if (Input.GetKeyDown(Key.E))
            {
                Console.WriteLine("Characters Selected.");
                round = new GameLoader(player1Selection, player2Selection);
                game.AddChild(round);
                this.LateDestroy();
            }
        }

        void PlayerInput()
        {
            if (Input.GetKeyDown(Key.SPACE))
            {
                Console.WriteLine(player1Selection + "||||||||||" + player2Selection);
            }
        }

        int PlayerSelector(int player = 1)
        {
            int[] controller;

            if (player == 1) controller = controller1;
            else controller = controller2;

            player--;

            if (Input.GetKeyDown(controller[0])) frameSelector[player]++;

            if (Input.GetKeyDown(controller[1])) frameSelector[player]--;

            if (frameSelector[player] < 1) frameSelector[player] = 3;
            if (frameSelector[player] > 3) frameSelector[player] = 1;

            return frameSelector[player];
        }
    }
}