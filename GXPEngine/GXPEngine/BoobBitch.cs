using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ExtensionMethods;
using System.Globalization;

namespace GXPEngine
{
    class BoobBitch : GameObject
    {
        Player player;
        HurtboxCreator hurtboxes;

        public BoobBitch(Player newPlayer) : base()
        {
            player = newPlayer as Player;
            player.AddChild(this);

            string line;
            int counter = 0;

            //remember to add customizable string
            System.IO.StreamReader file = new System.IO.StreamReader("Test Box 3.svg");
            while ((line = file.ReadLine()) != null)
            {
                if (line.StartsWith("<rect") || line.StartsWith(" <rect") || line.StartsWith("  <rect"))
                {
                    if (line.Contains("stroke"))
                    {
                        Console.WriteLine(line);
                        collisionMaker(line.Between("x=\"", "\""),
                        line.Between("y=\"", "\""),
                        line.Between("width=\"", "\""),
                        line.Between("height=\"", "\""),
                        line.Between("fill=\"#", "\""),
                        line.Between("stroke=\"#", "\""),
                        line.Between("stroke-width=\"", "\""));
                    }
                }
                counter++;
            }
        }

        void Update()
        {
            if (player.numberOfHurtboxes == 0)
            {
                if (player.currentFrame >= 0 && player.currentFrame <= 12)
                {
                    //Hurtbox hurtbox = new Hurtbox(120, 150, 500, 700, player.currentFrame, player.playerID, player);
                }
                else if (player.currentFrame >= 0 && player.currentFrame <= 12)
                {
                    Hurtbox hurtbox = new Hurtbox(120, 150, 500, 700, player.currentFrame, player.playerID, player);
                }
                else if (player.currentFrame == 14)
                {
                    Hurtbox hurtbox = new Hurtbox(100, 100, 500, 700, player.currentFrame, player.playerID, player);

                    Hitbox hitbox = new Hitbox(600, 10, 400, 300, player.currentFrame, player.playerID, player);
                    AddChild(hitbox);
                }
                else if (player.currentFrame == 15)
                {
                    Hurtbox hurtbox = new Hurtbox(100, 100, 500, 700, player.currentFrame, player.playerID, player);

                    Hitbox hitbox = new Hitbox(550, 80, 350, 250, player.currentFrame, player.playerID, player);
                    AddChild(hitbox);
                }
                else if (player.currentFrame >= 24 && player.currentFrame <= 28)
                {
                    Hurtbox hurtbox = new Hurtbox(100, 280, 500, 450, player.currentFrame, player.playerID, player);
                }
            }
        }

        private void collisionMaker(string collisionX, string collisionY, string collisionW, string collisionH, string color, string collisionFrame, string collisionDuration = "1")
        {
            Console.WriteLine("X: " + collisionX);
            //hurtboxes =
            new HurtboxCreator(IntConverter(collisionX), IntConverter(collisionY), IntConverter(collisionW), IntConverter(collisionH), color, IntConverter(collisionFrame), IntConverter(collisionDuration), player);
        }

        public static int IntConverter(string value)
        {
            int roundedInt;

            if (value.Contains("."))
            {
                double newValue = double.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
                roundedInt = (int)Math.Ceiling(newValue);
            }
            else
            {
                Console.WriteLine("value is " + value);
                roundedInt = int.Parse(value);
            }
            return roundedInt;
        }
    }
}
