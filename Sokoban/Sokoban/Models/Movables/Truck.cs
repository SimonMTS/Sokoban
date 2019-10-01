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
    }
}
