using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class CharacterSelect : Sprite
    {
        int[] playerSelection = { 0, 1 };
        bool[] confirm = { false, false };
        public GameLoader round;
        AnimationSprite[] characterPortrait = { new AnimationSprite("ui\\character-selection\\CharacterIcons.png", 3, 2), new AnimationSprite("ui\\character-selection\\CharacterIcons.png", 3, 2) };
        int[] frameSelector = {1, 1};
        private int[] controller1 = {Key.A, Key.D, Key.E, Key.Q};
        private int[] controller2 = {Key.LEFT, Key.RIGHT, Key.RIGHT_SHIFT, Key.DOT};
        int[] controller;
        Sprite[] selectedBorder = { new Sprite("ui\\character-selection\\SelectedPortrait.png") , new Sprite("ui\\character-selection\\SelectedPortrait.png") };

        public CharacterSelect() : base("ui\\character-selection\\charselect-main.png")
        {
            for (int i = 0; i < 2; i++)
            {
                characterPortrait[i].scale = 0.5f;
                characterPortrait[i].SetOrigin(characterPortrait[i].width / 2, characterPortrait[i].height / 2);
                AddChild(characterPortrait[i]);

                selectedBorder[i].SetOrigin(selectedBorder[i].width / 2 - characterPortrait[i].width / 2, selectedBorder[i].height / 2 - characterPortrait[i].height / 2);
                characterPortrait[i].AddChild(selectedBorder[i]);
            }
            characterPortrait[0].SetXY(200, 400);
            characterPortrait[1].SetXY(1500, 400);
        }

        void Update()
        {
            MenuButtons();
            PlayerInput();

            for (int i = 0; i < 2; i++)
            {
                if (!confirm[i])
                {
                    playerSelection[i] = PlayerSelector(i + 1);
                    characterPortrait[i].SetFrame(playerSelection[i] - 1);
                }
                selectedBorder[i].alpha = confirm[i] ? 1 : 0;
            }
        }

        void MenuButtons(){
            // when Q is pressed the player will go back to the mainmenu
            if (Input.GetKeyDown(controller1[3]) && !confirm[0])
            {
                Console.WriteLine("Back.");
                game.AddChild(new MainMenu());
                this.LateDestroy();
            }
            if (Input.GetKeyDown(controller2[3]) && !confirm[1])
            {
                Console.WriteLine("Back.");
                game.AddChild(new MainMenu());
                this.LateDestroy();
            }
            
            if (confirm[0] && confirm[1])
            {
                if (playerSelection[0] == playerSelection[1])
                {
                    if (playerSelection[1] < 3) playerSelection[1]++;
                    else playerSelection[1] = 1;
                }
                round = new GameLoader(playerSelection[0], playerSelection[1]);
                game.AddChild(round);
                MyGame.Fight.Play();
                this.LateDestroy();
            }
        }

        void PlayerInput()
        {
            for (int players = 0; players < 2; players++)
            {
                int enemy;
                if (players == 0)
                {
                    controller = controller1;
                    enemy = 1;
                }
                else
                {
                    controller = controller2;
                    enemy = 0;
                }
                Console.WriteLine(playerSelection[players] + " ------ " + playerSelection[enemy]);
                if (Input.GetKeyDown(controller[2]))
                {

                    if (!confirm[enemy]) confirm[players] = true;
                    else
                    {
                        if (playerSelection[enemy] != playerSelection[players] - 3)
                        {
                            confirm[players] = true;
                        }
                    }
                }
                
                if (Input.GetKeyDown(controller[3])) confirm[players] = false;
            }
        }

        int PlayerSelector(int player = 1)
        {
            int enemy;

            if (player == 1)
            {
                enemy = 2;
                controller = controller1;
            }
            else
            {
                enemy = 1;
                controller = controller2;
            }

            player--;
            enemy--;

            if (Input.GetKeyDown(controller[0])) frameSelector[player]++;

            if (Input.GetKeyDown(controller[1])) frameSelector[player]--;
            
            if (frameSelector[player] < 1) frameSelector[player] = 3;
            if (frameSelector[player] > 3) frameSelector[player] = 1;

            if (playerSelection[enemy] == frameSelector[player] && confirm[enemy])
            {
                return frameSelector[player] + 3;
            }
            else return frameSelector[player];
        }
    }
}