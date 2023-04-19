using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameLibrary.WorldObjects
{
    public class WorldObject
    {
        public string Name { get; set; }
        public bool IsLootable { get; set; }
        public bool IsRemoveable { get; set; }
        public Position Position { get; set; }

    }
}
