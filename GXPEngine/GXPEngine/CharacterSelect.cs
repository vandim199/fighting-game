using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GXPEngine
{
    class CharacterSelect : Sprite
    {
        Button _backButton;
        Button _nextButton;
        Button _boobBitchButton;
        int player1Selection = 2;
        int player2Selection = 2;

        public CharacterSelect() : base("CharacterSelect.png")
        {
            _backButton = new Button(this, 100, 700, "BackButton.png");

            _nextButton = new Button(this, 1200, 700, "NextButton.png");

            _boobBitchButton = new Button(this, 300, 300, "BoobBitchPortrait.png", 0.7f);
        }

        void Update()
        {
            if (_backButton.clicked)
            {
                Console.WriteLine("Back.");
                game.AddChild(new MainMenu());
                this.LateDestroy();
            }

            if (_nextButton.clicked)
            {
                Console.WriteLine("Characters Selected.");
                game.AddChild(new GameLoader(player1Selection, player2Selection));
                this.LateDestroy();
            }

            if (_boobBitchButton.clicked)
            {
                player1Selection = 2;
                player2Selection = 2;
            }
        }
    }
}