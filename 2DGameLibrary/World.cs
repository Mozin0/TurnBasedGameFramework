using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2DGameLibrary.Creatures;
using _2DGameLibrary.WorldObjects;

namespace _2DGameLibrary
{
    public class World
    {
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public List<Creature> Creatures { get; set; }
        public List<AttackItem> AttackItems { get; set; }
        public List<DefenceItem> DefenceItems { get; set; }

        public World(int maxX, int maxY)
        {
            MaxX = maxX;
            MaxY = maxY;
            Creatures = new();
            AttackItems = new();
            DefenceItems = new();
        }

        /// <summary>
        /// Adds a world object to the appropriate list of objects in the world.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="worldObject"></param>
        /// <returns></returns>
        public T AddWorldObject<T>(T worldObject) where T : WorldObject
        {
            if (worldObject is AttackItem attackitem)
            {
                AttackItems.Add(attackitem);
            }
            else if (worldObject is DefenceItem defenceItem)
            {
                DefenceItems.Add(defenceItem);
            }
            return worldObject;

        }

        /// <summary>
        /// Removes a world object from the appropriate list of objects in the world.
        /// </summary>
        /// <param name="worldObject"></param>
        /// <returns></returns>
        public WorldObject RemoveWorldObject(WorldObject worldObject)
        {
            if (worldObject is AttackItem attackitem)
            {
                AttackItems.Remove(attackitem);
            }
            else if (worldObject is DefenceItem defenceItem)
            {
                DefenceItems.Remove(defenceItem);
            }
            return worldObject;
        }

        /// <summary>
        /// Adds a creature to the list of creatures in the world.
        /// </summary>
        /// <param name="creature"></param>
        /// <returns></returns>
        public Creature AddCreature(Creature creature)
        {
            Creatures.Add(creature);
            return creature;
        }

        /// <summary>
        /// Removes a creature from the list of creatures in the world.
        /// </summary>
        /// <param name="creature"></param>
        /// <returns></returns>
        public Creature RemoveCreature(Creature creature)
        {
            Creatures.Remove(creature);
            return creature;
        }

        /// <summary>
        /// Gets a list of nearby objects of a specified type within a given range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="position"></param>
        /// <param name="range"></param>
        /// <returns></returns>
        public List<T> GetNearby<T>(Position position, int range) where T : WorldObject
        {
            List<T> nearbyObjects = new List<T>();

            foreach (T obj in AttackItems.OfType<T>().Concat(DefenceItems.OfType<T>()).Concat(Creatures.OfType<T>()))
            {
                int distanceX = Math.Abs(position.X - obj.Position.X);
                int distanceY = Math.Abs(position.Y - obj.Position.Y);

                if (distanceX <= range && distanceY <= range)
                {
                    nearbyObjects.Add(obj);
                }
            }
            return nearbyObjects;
        }
    }
}
