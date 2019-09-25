using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Models.Nodes
{
    class WallNode : Node
    {
        public override char CharRepresentation { get { return '█'; } }

        public override bool Walkable { get { return false; } }

        public WallNode(int _x, int _y) : base(_x, _y)
        {
        }
    }
}
