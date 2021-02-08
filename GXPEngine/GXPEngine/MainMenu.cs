using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class MainMenu : Sprite
    {
        Sprite _playButton;
        Sprite _exitButton;

        public MainMenu() : base("MainMenuBG.png")
        {
            _playButton = new Sprite("PlayButton.png");
            _playButton.SetXY(1100, 500);
            AddChild(_playButton);

            _exitButton = new Sprite("ExitButton.png");
            _exitButton.SetXY(1100, 600);
            AddChild(_exitButton);
        }

        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (_playButton.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    Console.WriteLine("Game Start.");
                    game.AddChild(new CharacterSelect());
                    this.LateDestroy();
                }

                if (_exitButton.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    Console.WriteLine("Game Exit.");
                }
            }
        }
    }
}
