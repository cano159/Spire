using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Harmony;
using Monocle;
using Spire.Arrow;
using Spire.Atlas;
using Spire.Command;
using Spire.Events;
using TowerFall;
using static Spire.Logger.Logger;

namespace Spire
{
    public class SpireController
    {
        public const string ModIdentifier = "Spire";
        public const string HarmonyInstanceIdentifier = "Spire.Harmony";

        private static bool _isInitializationInProgress;

        private static SpireController _instance;

        public static SpireController Instance => _instance ?? (_instance = new SpireController());

        internal static HarmonyInstance HarmonyInst { get; private set; }

        public IEnumerable<Mod> LoadedMods => _loadedMods.Values;

        public ObjectRegistrar<ArcherData> ArcherDataRegistrar = new ObjectRegistrar<ArcherData>();
        public ObjectRegistrar<ArcherPortrait> ArcherPortraitRegistrar = new ObjectRegistrar<ArcherPortrait>();

        public ObjectRegistrar<CustomArrow> ArrowRegistrar = new ObjectRegistrar<CustomArrow>();
        public ObjectRegistrar<AtlasAddition> AtlasAdditionRegistrar = new ObjectRegistrar<AtlasAddition>();
        public ObjectRegistrar<ConsoleCommand> ConsoleCommandsRegistrar = new ObjectRegistrar<ConsoleCommand>();
        public ObjectRegistrar<Entity> EntityRegistrar = new ObjectRegistrar<Entity>();
        public ObjectRegistrar<LevelEntity> LevelEntityRegistrar = new ObjectRegistrar<LevelEntity>();
        public ObjectRegistrar<Level> LevelRegistrar = new ObjectRegistrar<Level>();
        public ObjectRegistrar<OptionsButton> OptionsButtonRegistrar = new ObjectRegistrar<OptionsButton>();
        public ObjectRegistrar<RoundLogic> RoundLogicRegistrar = new ObjectRegistrar<RoundLogic>();
        public ObjectRegistrar<Variant> VariantsRegistrar = new ObjectRegistrar<Variant>();

        private readonly HashSet<Assembly> _autoHarmonyPatchedAssemblies = new HashSet<Assembly>();

        private readonly ConcurrentDictionary<int, Mod> _loadedMods = new ConcurrentDictionary<int, Mod>();

        public void Initialize()
        {
            try
            {
                EventController.Instance.OnGameLoaded += Instance_OnGameLoaded;

                ApplyHarmonyPatches();
                LoadAndInitializeMods();

            }
            catch (Exception e)
            {
                LogExceptionOnLoad(e);
                throw;
            }
        }

        private void Instance_OnGameLoaded(object sender, EventArgs e)
        {
            _isInitializationInProgress = false;
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

        private void LoadAndInitializeMods()
        {
            if (!Directory.Exists("Mods"))
            {
                LogMessageOnLoad("The 'Mods' directory was not found, creating...");
                Directory.CreateDirectory("Mods");
            }

            foreach (string currentFile in EnumerateModFiles())
            {
                try
                {
                    Assembly assembly = Assembly.LoadFrom(currentFile);

                    List<Type> types = assembly.GetExportedTypes().Where(x => x.BaseType == typeof(Mod)).ToList();

                    if (types.Count <= 0)
                        continue;

                    foreach (Type modType in types)
                    {
                        Mod loadedMod = TryLoadModFromAssembly(modType);

                        LogMessageOnLoad($"Loaded {loadedMod.ModName} from {Path.GetFileName(assembly.Location)}");
                    }

                }
                catch (Exception e)
                {
                    LogExceptionOnLoad(e);
                }

            }
        }

        private Mod TryLoadModFromAssembly(Type modType)
        {
            var mod = (Mod)Activator.CreateInstance(modType, false);

            return TryLoadModFromModObject(mod);
        }

        private Mod TryLoadModFromModObject(Mod modObject)
        {
            _loadedMods.TryAdd(_loadedMods.Count, modObject);

            modObject.ApplyHarmonyPatches();

            modObject.IsActive = true;
            modObject.OnModEnabled();

            return modObject;
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
            IEnumerable<string> dllsInModDirectory = Directory.EnumerateFiles(Globals.SpireModsDirectory, "*.dll",
                SearchOption.AllDirectories);
            
            return from modFile in dllsInModDirectory 
                let toLower = modFile.ToLower()
                where !toLower.Contains("0harmony") && !toLower.Contains("towerfall") && !toLower.Contains("spire") 
                select modFile;
        }
    }
}