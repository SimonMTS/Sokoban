using Sokoban.Models.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sokoban.Controllers.Controller;

namespace Sokoban.Models.Movables
{
    class Truck : Movable
    {
        public override char CharRepresentation { get { return '@'; } }

        public void DoAction(GameAction direction)
        {
            TryMove(direction);
        }

        public bool TryMove(GameAction direction)
        {
            Node neighbour = this.GetNode().getNeighbour(direction);

            if (!neighbour.Walkable) { return false; }

            if (neighbour.ContainsTruck)
            {
                ((AITruck)neighbour.Contains().Truck).WakeUp();
                return false;
            }
            else if (neighbour.ContainsCrate && neighbour.Contains().Crate.TryMove(direction))
            {
                this.Move(direction);
                return true;
            }
            else if (!neighbour.ContainsCrate)
            {
                this.Move(direction);
                return true;
            }

            return false;
        }
    }
}
