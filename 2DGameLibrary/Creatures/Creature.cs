using System;
using _2DGameLibrary.Decorators;
using _2DGameLibrary.WorldObjects;

namespace _2DGameLibrary.Creatures
{

    public class Creature    
    {
        public int Hp { get; set; }
        public string Name { get; set; }
        public bool isDead { get; set; }
        public string Type { get; set; }
        public Position Position { get; set; }
        public virtual List<AttackItem> AttackItems { get; set; }
        public virtual List<DefenceItem> DefenceItems { get; set; }
        public IMovement Movement { get; set; } = new DefaultMovement();

        public Creature(string name, Position position, int hp)
        {
            Name = name;
            Position = position;
            Hp = hp;
            AttackItems = new List<AttackItem>();
            DefenceItems = new List<DefenceItem>();
            isDead = false;
        }

        public static void Play(Creature creature, Creature creature1, World world)
        {
            Creature currentCreature = creature;

            while (!creature.isDead && !creature1.isDead)
            {
                currentCreature.Move(0, 0, world);
                Logger.Info($"{currentCreature.Type} {currentCreature.Name} moved to ({currentCreature.Position.X}, {currentCreature.Position.Y})");

                bool lootSuccess = currentCreature.Loot(world);
                if (lootSuccess)
                {
                    Logger.Info($"{currentCreature.Type} {currentCreature.Name} successfully looted an object.");
                }

                Creature otherCreature = currentCreature == creature ? creature1 : creature;
                Combat(currentCreature, otherCreature);

                if (currentCreature.isDead)
                {
                    world.RemoveCreature(currentCreature);
                    Logger.Info($"{currentCreature.Type} {currentCreature.Name} has been removed from the world");
                }

                currentCreature = otherCreature;
            }
        }


        /// <summary>
        /// Simulates a combat between two creatures.
        /// </summary>
        /// <param name="myCreature">The creature initiating the combat.</param>
        /// <param name="enemyCreature">The creature being attacked.</param>
        public static void Combat(Creature creature1, Creature creature2)
        {
            // Check if creatures are in range
            AttackItem equippedAttackItem1 = creature1.GetEquippedAttackItem();
            AttackItem equippedAttackItem2 = creature2.GetEquippedAttackItem();

            DefenceItem testDef = new DefenceItem { ReduceHitpoint = 10, Name = "Testdef", Position = new Position(65,77), IsLootable = true };
            AttackItem testS = new AttackItem { Hitpoint = 10, Name = "test", IsEquipped = true, Position = new Position(55,60), Range = 5};
            AttackItem test2 = new AttackItem { Hitpoint = 100, Name = "test2", IsEquipped = true, Position = new Position(50, 65), Range = 5 };

            creature1.AttackItems.Add(testS);
            creature2.AttackItems.Add(test2);
            creature2.DefenceItems.Add(testDef);

            if (!creature1.IsInRange(creature2, equippedAttackItem1) || !creature2.IsInRange(creature1, equippedAttackItem2))
            {
                return;
            }

            // Start combat
            Logger.Info($"Starting combat between {creature1.Type} {creature1.Name} and {creature2.Type} {creature2.Name}...");

            while (!creature1.isDead && !creature2.isDead)
            {
                creature1.Hit(creature2);

                if (creature2.isDead)
                {
                    Logger.Info($"{creature1.Type}{creature1.Name} has defeated {creature2.Type}{creature2.Name}.");
                    break;
                }

                creature2.Hit(creature1);

                if (creature1.isDead)
                {
                    Logger.Info($"{creature2.Name} has defeated {creature1.Name}.");
                    
                }
            }
        }

        /// <summary>
        /// The creature attacks the enemy creature with its equipped attack item.
        /// </summary>
        /// <param name="enemyCreature">The creature being attacked.</param>
        public void Hit(Creature enemyCreature)
        {
            AttackItem equippedAttackItem = GetEquippedAttackItem();

            if (equippedAttackItem == null || enemyCreature.isDead)
            {
                Logger.Warning($"{Name} cannot attack {enemyCreature.Name}.");
                return;
            }

            if (IsInRange(enemyCreature, equippedAttackItem))
            {
                Logger.Info($"{Name} attacks {enemyCreature.Name} with {equippedAttackItem.Name}.");
                enemyCreature.ReceiveHit(this, equippedAttackItem);
            }
            else
            {
                Logger.Warning($"{Name} cannot attack {enemyCreature.Name}.");
                return;
            }
        }


        private bool IsInRange(Creature enemyCreature, AttackItem equippedAttackItem)
        {
            if (equippedAttackItem == null)
            {
                //Logger.Info("Equipped attack item is null.");
                return false;
            }

            int distanceX = Position.X - enemyCreature.Position.X;
            int distanceY = Position.Y - enemyCreature.Position.Y;

            return distanceX <= equippedAttackItem.Range && distanceY <= equippedAttackItem.Range;
        }

        public bool Loot(World world)
        {
            var nearbyObjects = world.GetNearby<WorldObject>(new Position(Position.X, Position.Y), 5);
            var lootableObjects = nearbyObjects.Where(obj => obj is AttackItem || obj is DefenceItem);
            var lootableObject = lootableObjects.FirstOrDefault();

            if (lootableObject == null)
            {
                return false;
            }

            if (!IsInRangeWorldObject(lootableObject, 5))
            {
                return false;
            }

            if (lootableObject is AttackItem attackItem)
            {
                AttackItems.Add(attackItem);
                if (AttackItems.Count == 1)
                {
                    attackItem.IsEquipped = true;
                }
                Logger.Info($"{Name} has looted the attack item {attackItem.Name}.");

                world.RemoveWorldObject(attackItem);
            }
            else if (lootableObject is DefenceItem defenceItem)
            {
                DefenceItems.Add(defenceItem);
                if (DefenceItems.Count == 1)
                {
                    defenceItem.IsEquipped = true;
                }
                Logger.Info($"{Name} has looted the defence item {defenceItem.Name}.");

                world.RemoveWorldObject(defenceItem);
            }

            return true;
        }

        private bool IsInRangeWorldObject(WorldObject worldObject, int range)
        {
            int distanceX = Math.Abs(Position.X - worldObject.Position.X);
            int distanceY = Math.Abs(Position.Y - worldObject.Position.Y);

            return distanceX <= range && distanceY <= range;
        }

        /// <summary>
        /// Receives a hit from a creature using a specified attack item.
        /// </summary>
        /// <param name="creature">The attacking creature.</param>
        /// <param name="attackItem">The attack item used in the attack.</param>

        public void ReceiveHit(Creature creature, AttackItem attackItem)
        {
            if (!CanBeHitBy(creature, attackItem))
            {
                Logger.Info($"{Name} blocks {creature.Name}'s attack with a defense item.");
                return;
            }

            int hitpoint = attackItem.Hitpoint;

            DefenceItem equippedDefenseItem = GetEquippedDefenseItem();
            if (equippedDefenseItem != null)
            {
                hitpoint -= equippedDefenseItem.ReduceHitpoint;
                Logger.Info($"{Name} blocks {creature.Name}'s attack with a defense item ({equippedDefenseItem.Name}).");
            }

            if (hitpoint > 0)
            {
                Logger.Info($"{Name} receives {hitpoint} damage from {creature.Name}'s {attackItem.Name}.");
                Hp -= hitpoint;

                if (Hp <= 0)
                {
                    isDead = true;
                    Logger.Info($"{Name} is dead.");
                }
            }

            Logger.Info($"{Name}'s HP is now at {Hp}.");
        }


        private bool CanBeHitBy(Creature creature, AttackItem attackItem)
        {
            int distanceX = Math.Abs(Position.X - creature.Position.X);
            int distanceY = Math.Abs(Position.Y - creature.Position.Y);

            if (creature.AttackItems.Contains(attackItem) && distanceX <= attackItem.Range && distanceY <= attackItem.Range)
            {
                return true;
            }

            // Add a log message to indicate that the attack was out of range.
            Logger.Info($"{creature.Name}'s {attackItem.Name} missed {Name} due to distance.");

            return false;
        }

        public void Move(int x, int y, World world)
        {
            Movement.Move(x, y, world, this);
        }
        
        public AttackItem GetEquippedAttackItem()
        {
            return AttackItems.FirstOrDefault(x => x.IsEquipped);
        }

        /// <summary>
        /// Gets the equipped defence item.
        /// </summary>
        /// <returns> The equipped defence item, or null if no defence item is equipped.</returns>
        public DefenceItem GetEquippedDefenseItem()
        {
            return DefenceItems.FirstOrDefault(x => x.IsEquipped);
        }

    }
}