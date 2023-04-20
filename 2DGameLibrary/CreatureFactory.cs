using _2DGameLibrary.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameLibrary
{
    public class CreatureFactory
    {
        /// <summary>
        /// Creates a new instance of a creature based on the specified parameters.
        /// </summary>
        /// <param name="creatureType"></param>
        /// <param name="name"></param>
        /// <param name="position"></param>
        /// <param name="hp"></param>
        /// <returns>A new instance of the specified creature type.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static Creature Create(string creatureType, string name, Position position, int hp)
        {
            return creatureType switch
            {
                "Dragon" => new Dragon(name, position, hp) { Type = "Dragon" },
                "Basilisk" => new Basilisk(name, position, hp) { Type = "Basilisk" },
                _ => throw new ArgumentException("Invalid creature type"),
            };
        }


    }


}
