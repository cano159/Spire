using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Monocle;
using Spire.Command;

namespace MiscAdditions.Commands
{
    public class SpawnEntityConsoleCommand : ConsoleCommand
    {
        //Base constructor.
        public SpawnEntityConsoleCommand() : base("spawn_entity")
        {
        }

        /// <summary>
        ///     The console command's invoke method upon the command being entered.
        /// </summary>
        /// <param name="args"></param>
        public override void Invoke(string[] args)
        {
            //if args is not empty
            if (args.Length > 0)
            {
                string entityName = args[0];

                if (!entityName.Contains("TowerFall.")) entityName = GetAdjustedEntityName(entityName);

                Type entityType = GetType(entityName, "TowerFall");

                if (entityType != null)
                {
                    if (entityType.IsAbstract)
                    {
                        Log($"Type {entityType} is abstract and can't be spawned.");
                        return;
                    }

                    ConstructorInfo[] constructors = entityType.GetConstructors();

                    if (constructors.Any(x => !x.GetParameters().Any()) && args.Length == 1)
                    {
                        TrySpawnEntity((Entity) Activator.CreateInstance(entityType));
                    }

                    else if (args.Length > 1)
                    {
                        IEnumerable<object> convertedParameters =
                            TryConvertParameters(constructors, args.Skip(1).ToList());

                        var entity = (Entity) Activator.CreateInstance(entityType, convertedParameters.ToArray());

                        TrySpawnEntity(entity);
                    }
                    else
                    {
                        Log($"Object {entityType} has no parameterless constructor.");
                        Log(
                            $"Constructor with lowest amount of parameters: {constructors.OrderBy(x => x.GetParameters().Length).First()}");
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
            var paramsCounter = 0;

            foreach (ConstructorInfo constructor in constructors)
            {
                if (constructor.GetParameters().Length != list.Count)
                    continue;

                foreach (ParameterInfo parameter in constructor.GetParameters())
                {
                    if (list[paramsCounter] == "null")
                        yield return null;

                    if (!TryCast(list[paramsCounter], parameter.ParameterType, out object castedObject))
                        continue;

                    paramsCounter++;
                    yield return castedObject;
                }
            }
        }

        private static bool TryCast(object obj, Type typeToCastTo, out object result)
        {
            try
            {
                object castResult = Convert.ChangeType(obj, typeToCastTo);
                result = castResult;
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }
    }
}