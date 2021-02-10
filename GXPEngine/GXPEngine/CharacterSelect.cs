using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class CharacterSelect : Sprite
    {
        Sprite _backButton;
        Sprite _nextButton;

        public CharacterSelect() : base("CharacterSelect.png")
        {
            _backButton = new Sprite("BackButton.png");
            _backButton.SetXY(100, 700);
            AddChild(_backButton);

            _nextButton = new Sprite("NextButton.png");
            _nextButton.SetXY(1200, 700);
            AddChild(_nextButton);
        }

        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (_nextButton.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    Console.WriteLine("Characters Selected.");
                    game.AddChild(new GameLoader());
                    this.LateDestroy();
                }

                if (_backButton.HitTestPoint(Input.mouseX, Input.mouseY))
                {
                    Console.WriteLine("Back.");
                    game.AddChild(new MainMenu());
                    this.LateDestroy();
                }
            }
        }
    }
}
