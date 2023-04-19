using _2DGameLibrary.Creatures;
using _2DGameLibrary.WorldObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameLibrary.Decorators
{
    public class DefenceBoostDecorator : CreatureDecorator
    {
        public DefenceBoostDecorator(Creature baseCreature) : base(baseCreature) { }
        public override List<DefenceItem> DefenceItems
        {
            get
            {
                foreach (var item in base.DefenceItems)
                {
                    item.ReduceHitpoint += 3;
                }
                return base.DefenceItems;
            }
            set { base.DefenceItems = value; }
        }
    }
}
