using _2DGameLibrary.Creatures;
using _2DGameLibrary.WorldObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameLibrary.Decorators
{
    public class AttackBoostDecorator : CreatureDecorator
    {
       private bool _isBoosted = false;

       public AttackBoostDecorator(Creature baseCreature) : base(baseCreature) { }

       public override List<AttackItem> AttackItems
       {
           get
           {
               var baseAttackItems = base.AttackItems;
               if (!_isBoosted)
               {
                   var boostedAttackItems = new List<AttackItem>();
                   foreach (var item in baseAttackItems)
                   {
                       boostedAttackItems.Add(new AttackItem { Name = item.Name, Position = item.Position, Hitpoint = item.Hitpoint + 5 });
                   }
                   baseAttackItems = boostedAttackItems;
                   _isBoosted = true;
               }
               return baseAttackItems;
           }
           set
           {
               base.AttackItems = value;
           }
       }

    }
}
