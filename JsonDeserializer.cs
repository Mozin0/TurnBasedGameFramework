using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Text.Json;
using _2DGameLibrary.Creatures;
using _2DGameLibrary.WorldObjects;
using _2DGameLibrary.Decorators;

namespace _2DGameLibrary
{
    public static class JsonDeserializer
    {
        /// <summary>
        /// Deserializes a World object from a JSON file located at the given path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static World DeserializeWorld(string path)
        {
            var worldConfig = GetFromJsonFile<World>(path);
            var worldSizeX = worldConfig.MaxX;
            var worldSizeY = worldConfig.MaxY;

            var world = new World(worldSizeX, worldSizeY);

            var creatures = DeserializeCreatures(path);
            var worldObjects = DeserializeWorldObjects(path);

            foreach (var worldObject in worldObjects)
            {
                //world.AddWorldObject(worldObject);

                if (worldObject is AttackItem attackItem)
                {
                    world.AddWorldObject(attackItem);
                }
                else if (worldObject is DefenceItem defenceItem)
                {
                    world.AddWorldObject(defenceItem);
                }
            }

            foreach (var creature in creatures)
            {
                world.AddCreature(creature);
            }

            return world;
        }

        /// <summary>
        /// Deserializes a list of Creature objects from a JSON file located at the given path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static List<Creature> DeserializeCreatures(string path)
        {
            var world = GetFromJsonFile<World>(path);
            var creatures = new List<Creature>();

            foreach (var creature in world.Creatures)
            {
                Creature baseCreature;
                switch (creature.Type)
                {
                    case "Dragon":
                        baseCreature = CreatureFactory.Create(creature.Type, creature.Name, creature.Position, creature.Hp);
                        baseCreature = new AttackBoostDecorator(baseCreature);
                        break;
                    case "Basilisk":
                        baseCreature = CreatureFactory.Create(creature.Type, creature.Name, creature.Position, creature.Hp);
                        baseCreature = new DefenceBoostDecorator(baseCreature);
                        break;
                    default:
                        throw new ArgumentException($"Unknown creature type: {creature.Type}");
                }
                creatures.Add(baseCreature);
            }
            return creatures;
        }

        /// <summary>
        /// Deserializes a list of WorldObject objects from a JSON file located at the given path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<WorldObject> DeserializeWorldObjects(string path)
        {
            var world = GetFromJsonFile<World>(path);
            var worldObjects = new List<WorldObject>();

            foreach (var item in world.AttackItems)
            {
                if (!worldObjects.Contains(item))
                {
                    worldObjects.Add(item);
                }
            }

            foreach (var item in world.DefenceItems)
            {
                if (!worldObjects.Contains(item))
                {
                    worldObjects.Add(item);
                }
            }

            return worldObjects;
        }

        /// <summary>
        /// Deserializes an object of type T from a JSON file located at the given path.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T GetFromJsonFile<T>(string path)
        {
            var data = File.ReadAllText(path);
            
            return JsonSerializer.Deserialize<T>(data);
        }

    }
}
