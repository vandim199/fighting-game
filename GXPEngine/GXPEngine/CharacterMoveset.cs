using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ExtensionMethods;
using System.Globalization;

namespace GXPEngine
{
    class CharacterMoveset : GameObject
    {
        Player player;

        public CharacterMoveset(Player newPlayer, string svgFile) : base()
        {
            player = newPlayer as Player;
            player.AddChild(this);

            readSVG(svgFile);
            //readSVG("Test Box 4.svg");
        }

        private void collisionMaker(string collisionX, string collisionY, string collisionW, string collisionH, string color, string collisionFrame, string collisionDuration = "1")
        {
            //hurtboxes =
            new HurtboxCreator(IntConverter(collisionX), IntConverter(collisionY), IntConverter(collisionW), IntConverter(collisionH), color, IntConverter(collisionFrame), IntConverter(collisionDuration), player);
        }

        public static int IntConverter(string value)
        {
            int roundedInt = 1;
            if (value.Length > 0)
            {
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
            }
            return roundedInt;
        }

        void readSVG(string moveset)
        {
            string line;
            int counter = 0;
            
            System.IO.StreamReader file = new System.IO.StreamReader(moveset);
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
    }
}