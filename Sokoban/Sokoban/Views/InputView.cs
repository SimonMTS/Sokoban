using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Views
{
    class InputView
    {
        public static int AwaitAction()
        {
            int action;

            var ch = Console.ReadKey(false).Key;
            switch (ch)
            {
                case ConsoleKey.UpArrow:
                    action = Node.NORTH;
                    break;
                case ConsoleKey.RightArrow:
                    action = Node.EAST;
                    break;
                case ConsoleKey.DownArrow:
                    action = Node.SOUTH;
                    break;
                case ConsoleKey.LeftArrow:
                    action = Node.WEST;
                    break;
                default:
                    action = Node.INVALID;
                    break;
            }

            return action;
        }
    }
}
