using System;									// System contains a lot of default C# libraries 
using System.Drawing;                           // System.Drawing contains a library used for canvas drawing below
using GXPEngine;								// GXPEngine contains the engine

public class MyGame : Game
{
    public static Sound P1Wins = new Sound("sounds\\player1Wins.wav");
    public static Sound P2Wins = new Sound("sounds\\player2Wins.wav");
    public static Sound Fight = new Sound("sounds\\Fight.wav");
    public static Sound TimeOut = new Sound("sounds\\TimeOut.wav");
    public static Sound Round1 = new Sound("sounds\\round1.wav");
    public static Sound Round2 = new Sound("sounds\\round2.wav");
    public static Sound Round3 = new Sound("sounds\\round3.wav");
    public static Sound BGMusic = new Sound("sounds\\music_heavy.mp3", true);
    public static Sound Walking = new Sound("sounds\\example_walking2.wav");
    public static Sound Whoosh = new Sound("sounds\\kick_whoosh.mp3");
    public static Sound Punch = new Sound("sounds\\punch.wav");
    public static Sound Laser = new Sound("sounds\\laser_example3.mp3");
    public static Sound Block = new Sound("sounds\\block_sample2.wav");

    float lastScale;

    public MyGame() : base(1280, 720, false, false)
	{
        AddChild(new MainMenu());
        targetFps = 60;
        ShowMouse(true);
        scale = 0.8f;
        BGMusic.Play(false, 0 , 0.4f);
        //SetXY(width / 2 - 768, 0);
        scale = width / 1920f;
    }

    void Update()
	{
        //SetXY(width / 2 - 768, 0);
        //scale = height / 1080f;
        if (width != lastScale)
        {
            scale = width / 1920f;
            lastScale = width;
        }
    }

	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}
}