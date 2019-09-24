using System;
using static Sokoban.Controllers.Controller;

namespace Sokoban.Views
{
    class InputView
    {
        public static GameAction AwaitActionGame()
        {
            GameAction action;

            var ch = Console.ReadKey(false).Key;
            switch (ch)
            {
                case ConsoleKey.UpArrow:
                    action = GameAction.MoveNorth;
                    break;
                case ConsoleKey.RightArrow:
                    action = GameAction.MoveEast;
                    break;
                case ConsoleKey.DownArrow:
                    action = GameAction.MoveSouth;
                    break;
                case ConsoleKey.LeftArrow:
                    action = GameAction.MoveWest;
                    break;
                case ConsoleKey.S:
                    action = GameAction.Stop;
                    break;
                case ConsoleKey.R:
                    action = GameAction.Reset;
                    break;
                case ConsoleKey.U:
                    action = GameAction.Undo;
                    break;
                default:
                    action = GameAction.Invalid;
                    break;
            }

            return action;
        }

        public static GameAction AwaitActionMenu()
        {
            GameAction action;

            var ch = Console.ReadKey(false).Key;
            switch (ch)
            {
                case ConsoleKey.S:
                    action = GameAction.Stop;
                    break;
                case ConsoleKey.D1:
                    action = GameAction.Level1;
                    break;
                case ConsoleKey.D2:
                    action = GameAction.Level2;
                    break;
                case ConsoleKey.D3:
                    action = GameAction.Level3;
                    break;
                case ConsoleKey.D4:
                    action = GameAction.Level4;
                    break;
                case ConsoleKey.D5:
                    action = GameAction.Level5;
                    break;
                case ConsoleKey.D6:
                    action = GameAction.Level6;
                    break;
                default:
                    action = GameAction.Invalid;
                    break;
            }

            return action;
        }

        public static void AwaitAnyKey()
        {
            Console.ReadKey();
        }
    }
}
