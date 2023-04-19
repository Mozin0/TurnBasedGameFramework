using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameLibrary.WorldObjects
{
    public class AttackItem : WorldObject
    {
        public int Hitpoint { get; set; }
        public int Range { get; set; }
        public bool IsEquipped { get; set; }

    }
}
