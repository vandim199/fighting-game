﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class MainMenu : Sprite
    {
        Button _playButton;
        Button _exitButton;

        public MainMenu() : base("MainMenuBG.png")
        {
            _playButton = new Button(this, 1100, 500, "PlayButton.png", 0.6);

            _exitButton = new Button(this, 1100, 600, "ExitButton.png", 0.6);
        }

        void Update()
        {
            if (_playButton.clicked)
            {
                Console.WriteLine("Game Start.");
                game.AddChild(new CharacterSelect());
                this.LateDestroy();
            }

            if (_exitButton.clicked)
            {
                Console.WriteLine("Game Exit.");
                Environment.Exit(0);
            }
        }
    }
}