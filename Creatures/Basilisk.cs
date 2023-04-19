using _2DGameLibrary.WorldObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameLibrary.Creatures
{
    public class Basilisk : Creature
    {        
        public Basilisk(string name, Position position, int hp) : base(name, position, hp + 20)
        {
            Type = "Basilisk";
            Movement = new CrawlMovement();
        }

    }
}
