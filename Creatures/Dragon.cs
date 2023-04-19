using _2DGameLibrary.Decorators;
using _2DGameLibrary.WorldObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameLibrary.Creatures
{
    public class Dragon : Creature
    {
            public Dragon(string name, Position position, int hp) : base(name, position, hp + 50)
            {
                Type = "Dragon";
                Movement = new FlyMovement();
            }
    }
}