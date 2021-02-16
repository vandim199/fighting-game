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
        int player1Selection = 1;
        int player2Selection = 2;
        int maxCharacters = 3;
        public GameLoader round;

        public CharacterSelect() : base("CharacterSelect.png")
        {
            _backButton = new Button(this, 100, 700, "BackButton.png");

            _nextButton = new Button(this, 1200, 700, "NextButton.png");

        }

        void Update()
        {
            MenuButtons();
            PlayerInput();
            MenuSelector();
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
        void PlayerInput(){
            // if D is pressed player 1 will go to the next character
            if (Input.GetKeyDown(Key.D)){
                if (player1Selection <= maxCharacters){
                player1Selection = player1Selection + 1;
                }
                if( player1Selection == player2Selection){
                player1Selection += 1;}
                if( player1Selection > maxCharacters){ player1Selection = 1;
                    if( player1Selection == player2Selection){
                    player1Selection += 1;}
                } 
                Console.WriteLine("player 1 = "+ player1Selection);
            }
            // if A is pressed player 2 will go 1 character back
            if (Input.GetKeyDown(Key.A)){
                if (player1Selection >= 1){
                player1Selection = player1Selection - 1;
                }
                if( player1Selection == player2Selection){
                player1Selection -= 1;}
                if( player1Selection < 1){ player1Selection = 3;
                    if( player1Selection == player2Selection){
                player1Selection -= 1;}
                }
                Console.WriteLine("player 1 = "+ player1Selection);
            }
            // if the left arrowkey is pressed player 2 will go to the next character
            if (Input.GetKeyDown(Key.LEFT)){
                if (player2Selection <= maxCharacters){
                player2Selection = player2Selection + 1;
                }
                if( player2Selection == player1Selection){
                player2Selection += 1;}
                if( player2Selection > maxCharacters){
                    player2Selection = 1;
                    if( player2Selection == player1Selection){
                    player2Selection += 1;}
                } 
                Console.WriteLine("player 2 = "+ player2Selection);
            }
            // if the right arrowkey is pressed player 2 will go 1 character back
            if (Input.GetKeyDown(Key.RIGHT)){
                if (player2Selection >= 1){
                player2Selection = player2Selection - 1;
                }
                if( player2Selection == player1Selection){
                player2Selection -= 1;}
                if( player2Selection < 1){
                    player2Selection = 3;
                    if( player2Selection == player1Selection){
                    player2Selection -= 1;}
                }
                Console.WriteLine("player 2 = "+ player2Selection);
            }
            if (Input.GetKeyDown(Key.SPACE))
            {
                Console.WriteLine(player1Selection + "||||||||||" + player2Selection);
            }
        }
        void MenuSelector(){
            if (player1Selection == 1){
                _boobBitchButton = new Button(this, 300, 300, "BoobBitchPortrait.png", 0.7f);
            }
            else
            {            
                _boobBitchButton = new Button(this,300, 300, "circle.png", 0.7f);
            }
            if(player1Selection == 2)
            {            
                _boobBitchButton = new Button(this,300, 300, "circle.png", 0.7f);
            }
            if(player1Selection == 3){
                 _boobBitchButton = new Button(this, 300, 300, "BoobBitch3.png", 0.7f);
            }
        }
    }
}