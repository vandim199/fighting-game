using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class MainMenu : Sprite
    {
        Button _playButton;
        Button _exitButton;
        Sprite logo;

        public MainMenu() : base("bg.png")
        {
            logo = new Sprite("Logo.png");
            logo.SetXY(100, 200);
            logo.scale = 0.5f;
            AddChild(logo);

            _playButton = new Button(this, 1100, 500, "PlayButton.png", 0.6);

            _exitButton = new Button(this, 1100, 600, "ExitButton.png", 0.6);
        }

        void Update()
        {
            if (_playButton.clicked || Input.GetKeyDown(Key.E))
            {
                Console.WriteLine("Game Start.");
                game.AddChild(new CharacterSelect());
                this.LateDestroy();
            }

            if (_exitButton.clicked || Input.GetKeyDown(Key.Q))
            {
                Console.WriteLine("Game Exit.");
                Environment.Exit(0);
            }
        }
    }
}