using System.Collections.Generic;
using System.Reflection;
using MBMScripts;

namespace MBM_Mod
{
    public static class LocalizationInjector
    {
        public static void InjectAll()
        {
            foreach (var trait in CustomTraitRegistry.All.Values)
            {
                Inject(trait);
            }
        }
        public static void Inject(CustomTrait trait)
        {
            var field = typeof(SeqLocalization)
                .GetField(
                    "LocalizedTextDictionary",
                    BindingFlags.Static | BindingFlags.NonPublic);

            var dict =
                (Dictionary<string, string>)field.GetValue(null);

            dict[$"#Trait{trait.Id}"] = trait.Name;
            dict[$"#Trait{trait.Id}Tooltip"] = trait.Tooltip;
        }
    }
}