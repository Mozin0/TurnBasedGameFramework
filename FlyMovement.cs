using _2DGameLibrary.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameLibrary
{
    public class FlyMovement : DefaultMovement
    {
        public override void Move(int x, int y, World world, Creature creature)
        {
            int newX = creature.Position.X + 2;
            int newY = creature.Position.Y + 2;

            if (newX < 0 || newX >= world.MaxX || newY < 0 || newY >= world.MaxY)
            {
                Logger.Warning($"Cannot move {creature.Name} to ({newX}, {newY}). Movement outside world boundaries.");
            }
            else
            {
                creature.Position.X = newX;
                creature.Position.Y = newY;
                base.Move(x, y, world, creature);
            }
        }
    }
}
