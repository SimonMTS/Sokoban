using Sokoban.Models;
using Sokoban.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Controllers
{
    class Controller
    {
        public Controller()
        {
            Maze level = new Maze(1);

            bool won = false;
            while (!won)
            {
                OutputView.draw(level.Map);

                int action = InputView.AwaitAction();
                if (action != Node.INVALID)
                {
                    level.ApplyAction(action);

                    won = level.HasWon();
                }
            }

            Console.Clear();
            Console.WriteLine("You did it, you stopped racism!");
            Console.ReadLine();
        }
    }
}
