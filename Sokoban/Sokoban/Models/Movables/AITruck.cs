using Sokoban.Models.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Sokoban.Controllers.Controller;

namespace Sokoban.Models.Movables
{
    class AITruck : Truck
    {
        public override char CharRepresentation { get { return (Awake ? '$' : 'Z'); } }

        public bool Awake = false;

        public void WakeUp()
        {
            Awake = true;
        }

        public void DoAction()
        {
            int RandomNumber = (new Random()).Next(100);

            if (Awake)
            {
                if (RandomNumber <= 25)
                {
                    Awake = false;
                }

                GameAction RandomDir = (GameAction)(new Random()).Next(4)+20;

                TryMove(RandomDir);
            }
            else
            {
                if (RandomNumber <= 10)
                {
                    Awake = true;
                }
            }

        }

        new public bool TryMove(GameAction direction)
        {
            Node neighbour = this.GetNode().getNeighbour(direction);

            if (!neighbour.Walkable) { return false; }

            if (neighbour.ContainsTruck)
            {
                if (neighbour.Contains().Truck.TryMove(direction))
                {
                    this.Move(direction);
                    return true;
                }
                else
                {
                    return false;
                }
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
