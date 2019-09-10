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
            Maze level = new Maze();

            while (true)
            {
                OutputView.draw(level.Map);

                int action = InputView.AwaitAction();
                if (action != Node.INVALID)
                {
                    level.ApplyAction(action);
                }
            }
        }
    }
}
