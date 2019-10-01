using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Models.Nodes
{
    class FloorNode : Node
    {
        public override char CharRepresentation {
            get
            {
                var containsMovable = CharContainsMovable();

                if (containsMovable.Truth)
                {
                    return containsMovable.Value;
                }
                else
                {
                    return '.';
                }
                
            }
        }

        public override bool Walkable { get { return true; } }

        public FloorNode(int _x, int _y) : base(_x, _y)
        {
        }
    }
}
