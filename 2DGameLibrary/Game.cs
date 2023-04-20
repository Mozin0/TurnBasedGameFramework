using _2DGameLibrary.Creatures;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration.Internal;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace _2DGameLibrary
{
    public class Game
    {
        /// <summary>
        /// Starts the game by deserializing the World object from a JSON file located at a given path
        /// </summary>
        public void Start()
        {
            var world = JsonDeserializer.DeserializeWorld("D:/4th Semester/Advanced C#/2DGameLibrary/2DGameLibrary/world.json");

            Creature.Play(world.Creatures[0], world.Creatures[1], world);
        }
    }
}
