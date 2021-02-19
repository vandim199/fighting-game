using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class MainMenu : Sprite
    {
        Button _playButton;
        Button _controlsButton;
        Button _creditsButton;
        Button _exitButton;
        Sprite logo;
        Sprite credits = new Sprite("ui\\screens\\credits.png");
        Sprite controls = new Sprite("ui\\screens\\controls.png");

        Button[] buttons = new Button[4];

        int selection = 0;
        int previousSelection;

        public MainMenu() : base("ui\\menu\\bg.png")
        {
            logo = new Sprite("ui\\menu\\Logo.png");
            logo.SetXY(100, 200);
            logo.scale = 0.5f;
            AddChild(logo);

            _playButton = new Button(this, 1100, 300, "ui\\buttons\\Play.png", 1, 1, 2);
            _controlsButton = new Button(this, 1100, 400, "ui\\buttons\\Controls.png", 1, 1, 2);
            _creditsButton = new Button(this, 1100, 500, "ui\\buttons\\Credits.png", 1, 1, 2);
            _exitButton = new Button(this, 1100, 600, "ui\\buttons\\Exit.png", 1, 1, 2);

            buttons[0] = _playButton;
            buttons[1] = _controlsButton;
            buttons[2] = _creditsButton;
            buttons[3] = _exitButton;

            controls.alpha = 0;
            AddChild(controls);
            credits.alpha = 0;
            AddChild(credits);
        }

        void Update()
        {
            if (_playButton.clicked)
            {
                Console.WriteLine("Game Start.");
                game.AddChild(new CharacterSelect());
                this.LateDestroy();
            }
            if (_controlsButton.clicked)
            {
                controls.alpha = 1;
            }
            if (_creditsButton.clicked)
            {
                credits.alpha = 1;
            }
            if (_exitButton.clicked)
            {
                Console.WriteLine("Game Exit.");
                Environment.Exit(0);
            }

            selectButtons();
        }

        void selectButtons()
        {
            if (Input.GetKeyDown(Key.W)) selection--;
            if (Input.GetKeyDown(Key.S)) selection++;

            if (selection > 3)selection = 0;
            else if (selection < 0) selection = 3;

            buttons[selection].selected = true;

            if (selection != previousSelection)
            {
                buttons[previousSelection].selected = false;
                previousSelection = selection;
            }
            if (Input.GetKeyDown(Key.Q))
            {
                controls.alpha = 0;
                credits.alpha = 0;
            }
        }
    }
}