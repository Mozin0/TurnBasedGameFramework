using _2DGameLibrary.Creatures;
using _2DGameLibrary.WorldObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameLibrary.Decorators
{
    /// <summary>
    /// Abstract class representing a decorator for a Creature object.
    /// </summary>
    public abstract class CreatureDecorator : Creature
    {
        protected Creature BaseCreature;

        public CreatureDecorator(Creature baseCreature) : base(baseCreature.Name, baseCreature.Position, baseCreature.Hp)
        {
            BaseCreature = baseCreature;
            Name = baseCreature.Name;
            Position = baseCreature.Position;
            Hp = baseCreature.Hp;   
            DefenceItems = baseCreature.DefenceItems;
            Type = baseCreature.Type;
            Movement = baseCreature.Movement;
            AttackItems = baseCreature.AttackItems;
        }
    }
}
