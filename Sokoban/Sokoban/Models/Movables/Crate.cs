using Sokoban.Models.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sokoban.Controllers.Controller;

namespace Sokoban.Models.Movables
{
    class Crate : Movable
    {
        public override char CharRepresentation {
            get {
                if (GetNode() is DestinationNode)
                {
                    return '0';
                }
                else
                {
                    return '#';
                }
            }
        }

        public bool TryMove(GameAction direction)
        {
            Node neighbour = this.GetNode().getNeighbour(direction);

            if (neighbour.Walkable && !neighbour.ContainsCrate && !neighbour.ContainsTruck)
            {
                this.Move(direction);

                if (this.GetNode() is BrokenFloorNode && ((BrokenFloorNode)this.GetNode()).IsBroken)
                {
                    x = 0;
                    y = 0;
                }

                return true;
            }

            return false;
        }
    }
}
