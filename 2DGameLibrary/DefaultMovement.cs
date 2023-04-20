using _2DGameLibrary.Creatures;
using _2DGameLibrary.WorldObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameLibrary
{
    public class DefaultMovement : IMovement
    {
        /// <summary>
        /// Moves a creature to a new position in the world, while checking for obstacles.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="world"></param>
        /// <param name="creature"></param>
        public virtual void Move(int x, int y, World world, Creature creature)
        {
            // Generate random values for x and y
            int randomX = Random.Shared.Next(-2, 3);
            int randomY = Random.Shared.Next(-2, 3);

            int newX = creature.Position.X + randomX + x;
            int newY = creature.Position.Y + randomY + y;

            // Check if the new position is outside the world's boundaries
            if (newX < 0 || newY < 0 || newX >= world.MaxX || newY >= world.MaxY)
            {
                Logger.Warning($"Cannot move {creature.Type} {creature.Name} to ({newX}, {newY}). Movement outside world boundaries.");
                return;
            }

            // Check if there are any obstacles in the way
            if (CheckObstacles(newX, newY, creature))
            {
                Logger.Warning($"Obstacle detected. Cannot move {creature.Type}{creature.Name}.");
                return;
            }

            // Move the creature
            creature.Position.X = newX;
            creature.Position.Y = newY;
        }

        /// <summary>
        /// Checks if there are any attack or defense items in a given position that could block movement.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="creature"></param>
        /// <returns>True if an obstacle is detected, false otherwise.</returns>
        private bool CheckObstacles(int x, int y, Creature creature)
        {
            // Check if there are any attack or defense items in the way
            foreach (AttackItem item in creature.AttackItems)
            {
                if (item.Position.X == x && item.Position.Y == y)
                {
                    return true; // obstacle found
                }
            }

            foreach (DefenceItem item in creature.DefenceItems)
            {
                if (item.Position.X == x && item.Position.Y == y)
                {
                    return true; // obstacle found
                }
            }
            return false; // no obstacles found
        }
    }

}
