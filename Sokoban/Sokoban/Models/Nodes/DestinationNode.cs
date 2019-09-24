﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Models.Nodes
{
    class DestinationNode : FloorNode
    {
        public override bool Walkable { get { return true; } }

        public DestinationNode(int _x, int _y) : base(_x, _y)
        {
        }
    }
}
