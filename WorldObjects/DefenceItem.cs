using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameLibrary.WorldObjects
{
    public class DefenceItem : WorldObject
    {
        public int ReduceHitpoint { get; set; }
        public bool IsEquipped { get; set; }
    }
}
