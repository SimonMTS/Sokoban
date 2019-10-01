using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban.Models.Movables
{
    class AITruck : Truck
    {
        public override char CharRepresentation { get { return 'Z'; } }

        private bool Awake = false;
    }
}
