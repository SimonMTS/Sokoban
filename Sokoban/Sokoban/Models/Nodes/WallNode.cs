using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Models
{
    class WallNode : Node
    {
        public WallNode(int _x, int _y) : base(_x, _y)
        {
            x = _x;
            y = _y;
        }
    }
}
