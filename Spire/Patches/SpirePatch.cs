using System;
using System.Linq;
using System.Reflection;
using Harmony;

namespace Spire.Patches
{
    public abstract class SpirePatch
    {
        public abstract void Patch(HarmonyInstance harmony);

        protected void PatchTranspiler(HarmonyInstance harmony, MethodBase targetMethod,
            string transpilerMethod = "Transpiler")
        {
            PatchMultiple(harmony, targetMethod, null, null, transpilerMethod);
        }

        protected void PatchPrefix(HarmonyInstance harmony, MethodBase targetMethod, string prefixMethod = "Prefix")
        {
            if (prefixMethod == null) throw new ArgumentNullException(nameof(prefixMethod));
            PatchMultiple(harmony, targetMethod, prefixMethod);
        }

        protected void PatchPostfix(HarmonyInstance harmony, MethodBase targetMethod, string postfixMethod = "Postfix")
        {
            PatchMultiple(harmony, targetMethod, null, postfixMethod);
        }

        protected void PatchMultiple(HarmonyInstance harmony, MethodBase targetMethod, bool prefix, bool postfix,
            bool transpiler)
        {
            string prefixMethod = prefix ? "Prefix" : null;
            string postfixMethod = postfix ? "Postfix" : null;
            string transpilerMethod = transpiler ? "Transpiler" : null;

            PatchMultiple(harmony, targetMethod, prefixMethod, postfixMethod, transpilerMethod);
        }

        protected void PatchMultiple(HarmonyInstance harmony, MethodBase targetMethod,
            string prefixMethod = null, string postfixMethod = null, string transpilerMethod = null)
        {
            HarmonyMethod harmonyPrefixMethod = prefixMethod != null ? GetHarmonyMethod(prefixMethod) : null;
            HarmonyMethod harmonyPostfixMethod = postfixMethod != null ? GetHarmonyMethod(postfixMethod) : null;
            HarmonyMethod harmonyTranspilerMethod =
                transpilerMethod != null ? GetHarmonyMethod(transpilerMethod) : null;
            harmony.Patch(targetMethod, harmonyPrefixMethod, harmonyPostfixMethod, harmonyTranspilerMethod);
        }

        public HarmonyMethod GetHarmonyMethod(string methodName)
        {
            MethodInfo method = GetType().GetMethod(methodName,
                BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
            return new HarmonyMethod(method);
        }

        protected static int GetLocalVariableIndex<T>(MethodBase method)
        {
            LocalVariableInfo[] matchingVariables =
                method.GetMethodBody()?.LocalVariables.Where(v => v.LocalType == typeof(T)).ToArray();

            if (matchingVariables != null && matchingVariables.Length != 1)
                throw new ArgumentException(
                    $"{method} has {matchingVariables.Length} local variables of type {nameof(T)}");

            if (matchingVariables != null) return matchingVariables[0].LocalIndex;
            return -1;
        }
    }
}