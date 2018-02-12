using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Harmony;
using Spire.Atlas;
using Spire.Command;
using Spire.Patches;
using TowerFall;
using static Spire.Logger.Logger;

namespace Spire
{
    public class SpireController
    {
        private const string ModIdentifier = "Spire";
        private const string HarmonyInstanceIdentifier = "Spire.Harmony";

        private static SpirePatch[] _patches;

        private static bool _isInitializationInProgress;

        private static SpireController _instance;

        public static SpireController Instance => _instance ?? (_instance = new SpireController());

        internal static HarmonyInstance HarmonyInst { get; private set; }

        public ObjectRegistrar<ArcherData> ArcherDataRegistrar = new ObjectRegistrar<ArcherData>();
        public ObjectRegistrar<ArcherPortrait> ArcherPortraitRegistrar = new ObjectRegistrar<ArcherPortrait>();
        public ObjectRegistrar<AtlasAddition> AtlasAdditionRegistrar = new ObjectRegistrar<AtlasAddition>();
        public ObjectRegistrar<ConsoleCommand> ConsoleCommandsRegistrar = new ObjectRegistrar<ConsoleCommand>();
        public ObjectRegistrar<Level> LevelRegistrar = new ObjectRegistrar<Level>();
        public ObjectRegistrar<OptionsButton> OptionsButtonRegistrar = new ObjectRegistrar<OptionsButton>();
        public ObjectRegistrar<RoundLogic> RoundLogicRegistrar = new ObjectRegistrar<RoundLogic>();

        private readonly HashSet<Assembly> _autoHarmonyPatchedAssemblies = new HashSet<Assembly>();

        private readonly ConcurrentDictionary<int, Mod> _loadedMods = new ConcurrentDictionary<int, Mod>();

        public void Initialize()
        {
            try
            {
                ApplyHarmonyPatches();
                LoadAndInitializeMods();
                ApplyPersistentGamePatches();
            }
            catch (Exception e)
            {
                LogExceptionOnLoad(e);
                throw;
            }
        }

        private void ApplyHarmonyPatches()
        {
            try
            {
                if (!ShouldHarmonyAutoPatch(typeof(SpireController).Assembly, ModIdentifier)) return;

                HarmonyInst = HarmonyInstance.Create(HarmonyInstanceIdentifier);
                HarmonyInst.PatchAll(typeof(SpireController).Assembly);
            }
            catch (Exception e)
            {
                LogExceptionOnLoad(e);
                throw;
            }
        }

        private static void ApplyPersistentGamePatches()
        {
            if (_patches != null)
                return;

            IEnumerable<SpirePatch> discoveredPatches =
                ExtensionMethods.GetEnumerableOfType<SpirePatch>(Assembly.GetExecutingAssembly());

            var counter = 0;
            var total = 0;

            foreach (SpirePatch patch in discoveredPatches)
            {
                try
                {
                    patch.Patch(HarmonyInst);
                    _patches.AddToArray(patch);
                    counter++;
                }
                catch (Exception e)
                {
                    LogMessageOnLoad($"Patch {patch.GetType().Name} failed to apply.");
                    LogExceptionOnLoad(e);
                }

                total++;
            }

            LogMessageOnLoad($"Applied {counter} of {total} patches.");
        }


        private void LoadAndInitializeMods()
        {
            foreach (string currentFile in EnumerateModFiles())
            {
                try
                {
                    Assembly assembly = Assembly.LoadFrom(currentFile);

                    List<Type> types = assembly.GetExportedTypes().Where(x => x.BaseType == typeof(Mod)).ToList();

                    if (types.Count < 0)
                        continue;

                    foreach (Type modType in types)
                    {
                        var mod = (Mod)Activator.CreateInstance(modType, false);

                        LogMessageOnLoad($"Loaded {mod.ModName} from {assembly.Location}");

                        _loadedMods.TryAdd(_loadedMods.Count, mod);

                        mod.ApplyHarmonyPatches();

                        mod.IsActive = true;
                        mod.OnModLoad();
                    }
                }
                catch (Exception e)
                {
                    LogExceptionOnLoad(e);
                }
            }
        }

        internal void OnPreInitialize()
        {
            _isInitializationInProgress = true;
        }

        internal void OnPostInitialize()
        {
            _isInitializationInProgress = false;
        }

        internal void DisableMod(Mod mod)
        {
            ChangeModState(mod, false);
        }

        internal void EnableMod(Mod mod)
        {
            ChangeModState(mod, true);
        }

        private static void ChangeModState(Mod mod, bool isEnable)
        {
            mod.IsActive = isEnable;
        }

        internal bool ShouldHarmonyAutoPatch(Assembly assembly, string modId)
        {
            if (_autoHarmonyPatchedAssemblies.Contains(assembly)) return false;

            _autoHarmonyPatchedAssemblies.Add(assembly);

            return true;
        }

        private static IEnumerable<string> EnumerateModFiles()
        {
            return Directory.EnumerateFiles(Globals.SpireModsDirectory, "*.dll",
                SearchOption.AllDirectories);
        }
    }
}