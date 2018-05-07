using Harmony;
using Monocle;
using Spire.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace MiscAdditions.Commands
{
    public class SpawnEntityConsoleCommand : ConsoleCommand
    {
        //Base constructor.
        public SpawnEntityConsoleCommand() : base("spawn_entity")
        {
        }

        /// <summary>
        /// The console command's invoke method upon the command being entered. 
        /// </summary>
        /// <param name="args"></param>
        public override void Invoke(string[] args)
        {
            //if args is not empty
            if (args.Length > 0)
            {
                string entityName = args[0];

                if (!entityName.Contains("TowerFall."))
                {
                    entityName = GetAdjustedEntityName(entityName);
                }

                Type entityType = GetType(entityName, "TowerFall");

                if (entityType != null)
                {
                    if (entityType.IsAbstract)
                    {
                        Log($"Type {entityType} is abstract and can't be spawned.");
                        return;
                    }

                    var constructors = entityType.GetConstructors();

                    if (constructors.Any(x => x.GetParameters().Count() == 0) && args.Length == 1)
                    {
                        TrySpawnEntity((Entity)Activator.CreateInstance(entityType));
                    }

                    else if (args.Length > 1)
                    {
                        var convertedParameters = TryConvertParameters(constructors, args.Skip(1).ToList());

                        Entity entity = (Entity)Activator.CreateInstance(entityType, convertedParameters.ToArray());

                        TrySpawnEntity(entity);
                    }
                    else
                    {
                        Log($"Object {entityType} has no parameterless constructor.");
                        Log($"Constructor with lowest amount of parameters: {constructors.OrderBy(x => x.GetParameters().Count()).First()}");
                    }

                }
                else
                {
                    Log($"Type: {entityName} was not found.");
                }
            }
        }

        private string GetAdjustedEntityName(string str)
        {
            return $"TowerFall.{str}";
        }

        private static Type GetType(string str, string nameSpace)
        {
            return Type.GetType($"{str},{nameSpace}");
        }

        private static void TrySpawnEntity(Entity entity)
        {
            Engine.Instance.Scene.Add(entity);
        }

        private IEnumerable<object> TryConvertParameters(IEnumerable<ConstructorInfo> constructors, List<string> list)
        {
            int paramsCounter = 0;

            foreach (var constructor in constructors)
            {
                if (constructor.GetParameters().Count() != list.Count())
                {
                    continue;
                }

                foreach (var parameter in constructor.GetParameters())
                {
                    if (list[paramsCounter] == "null")
                    {
                        yield return null;
                    }

                    var type = parameter.ParameterType;

                    if (TryCast(list[paramsCounter], parameter.ParameterType, out object castedObject))
                    {
                        paramsCounter++;
                        yield return castedObject;
                    }
                    else
                    {
                        continue;
                    }
                }

            }
            
        }

        private bool TryCast(object obj, Type typeToCastTo, out object result)
        {
            try
            {
                var castResult = Convert.ChangeType(obj, typeToCastTo);
                result = castResult;
                return true;
            }
            catch(Exception)
            {
                result = null;
                return false;
            }
        }

    }
}
