using _2DGameLibrary.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameLibrary
{
    /// <summary>
    /// Represents a movement behavior for a Creature in the World.
    /// </summary>
    public interface IMovement
    {
        /// <summary>
        /// Moves the Creature to the given coordinates in the World.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="world"></param>
        /// <param name="creature"></param>
        void Move(int x, int y, World world, Creature creature);
    }
}
