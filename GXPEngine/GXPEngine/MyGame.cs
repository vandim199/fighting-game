using System;									// System contains a lot of default C# libraries 
using System.Drawing;                           // System.Drawing contains a library used for canvas drawing below
using GXPEngine;								// GXPEngine contains the engine

public class MyGame : Game
{
	public MyGame() : base(1280, 720, false, false)
	{
        AddChild(new MainMenu());
        targetFps = 60;
        ShowMouse(true);
        scale = 0.8f;
        //SetXY(width / 2 - 768, 0);
    }

    void Update()
	{
        //SetXY(width / 2 - 768, 0);
        //scale = height / 1080f;
        scale = width / 1920f;
    }

	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}
}