using MBMScripts;
using System.Collections.Generic;
using System.Reflection;


namespace MBM_Mod
{
    public static class CustomTraitRegistry
    {
        private static readonly Dictionary<int, CustomTrait> Traits =
            new Dictionary<int, CustomTrait>();

        public static IReadOnlyDictionary<int, CustomTrait> All => Traits;

        public static void Register(CustomTrait trait)
        {
            if (Traits.ContainsKey(trait.Id))
                throw new System.Exception($"Trait ID {trait.Id} already exists.");

            Traits.Add(trait.Id, trait);

            // Inject TraitData into Database<TraitData>
            TraitInjector.Inject(trait);

            // Inject localization
            LocalizationInjector.Inject(trait);
            var field = typeof(SeqLocalization)
    .GetField(
        "LocalizedTextDictionary",
        BindingFlags.Static | BindingFlags.NonPublic);

            var dict = (Dictionary<string, string>)field.GetValue(null);

            Cheesy.Log.LogInfo(dict.ContainsKey("#Trait1001"));
            Cheesy.Log.LogInfo(dict.ContainsKey("#Trait1001Tooltip"));


        }
        private static bool initialized;

        public static void Initialize()
        {
            if (initialized)
                return;

            initialized = true;

            Register(
                new CustomTrait
                {
                    Id = 1001,
                    Name = "Exhausted",
                    Tooltip = "Always exhausted",
                    Price = -50,
                    IsPositive = false,
                    IsMonsterTrait = false,
                    IsSlaveTrait = true
                });
        }
        public static bool TryGet(int id, out CustomTrait trait)
        {
            return Traits.TryGetValue(id, out trait);
        }
    }
}
