using System;
using Sokoban.Models;
using Sokoban.Views;

namespace Sokoban.Controllers
{
    class Controller
    {
        public enum GameAction {
            Stop = 10,
            Reset = 11,
            Undo = 12,
            Invalid = -1,
            MoveNorth = 20, MoveEast = 21, MoveSouth = 22, MoveWest = 23,
            Level1 = 1, Level2 = 2, Level3 = 3, Level4 = 4
        };


        public Controller()
        {
            while (true)
            {
                OutputView.DrawMenu();
                GameAction action = InputView.AwaitActionMenu();

                if ( action == GameAction.Stop)
                {
                    break;
                }
                else if (action != GameAction.Invalid)
                {
                    StartLevel((int)action);
                }
            }
        }

        private void StartLevel(int levelNumber)
        {
            Game level = new Game(levelNumber);

            bool won = false;
            while (!won)
            {
                if ( level.Map.PrevMaze != null )
                {
                    OutputView.DrawLevel(level.Map.PrevMaze, false);
                    Console.WriteLine("PREV");
                    Console.ReadKey();
                }
                OutputView.DrawLevel(level.Map, false);

                GameAction action = InputView.AwaitActionGame();
                if (action == GameAction.Reset)
                {
                    level = new Game(levelNumber);
                }
                else if (action == GameAction.Stop)
                {
                    break;
                }
                else if (action != GameAction.Invalid)
                {
                    level.ApplyAction(action);

                    won = level.HasWon();
                }
            }

            if (won)
            {
                OutputView.DrawLevel(level.Map, true);
                InputView.AwaitAnyKey();
            }
        }
    }
}
