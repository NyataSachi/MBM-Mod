using System.Collections.Generic;
using System.Reflection;
using MBMScripts;

namespace MBM_Mod
{
    public static class TraitInjector
    {
        public static void Inject(CustomTrait trait)
        {
            TraitData data = new TraitData();

            SetPrivate(data, "m_DataName", $"Trait{trait.Id}");
            SetPrivate(data, "m_DataId", trait.Id);

            SetPrivate(data, "m_Name", $"#Trait{trait.Id}");
            SetPrivate(data, "m_Tooltip", $"#Trait{trait.Id}Tooltip");

            SetPrivate(data, "m_IsPositive", trait.IsPositive);
            SetPrivate(data, "m_Price", trait.Price);
            SetPrivate(data, "m_IsMonsterTrait", trait.IsMonsterTrait);
            SetPrivate(data, "m_IsSlaveTrait", trait.IsSlaveTrait);

            SetPrivate(data, "m_ValueList", new float[0]);

            var dbType = typeof(Database<TraitData>);

            var listField = dbType.GetField(
                "List",
                BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);

            var idField = dbType.GetField(
                "DataIdDictionary",
                BindingFlags.Static | BindingFlags.NonPublic);

            var nameField = dbType.GetField(
                "DataNameDictionary",
                BindingFlags.Static | BindingFlags.NonPublic);

            var list = (List<TraitData>)listField.GetValue(null);
            var idDict = (Dictionary<int, TraitData>)idField.GetValue(null);
            var nameDict = (Dictionary<string, TraitData>)nameField.GetValue(null);

            if (!idDict.ContainsKey(trait.Id))
            {
                list.Add(data);
                idDict.Add(trait.Id, data);
                nameDict.Add(data.DataName, data);
            }
        }

        private static void SetPrivate(object obj, string field, object value)
        {
            obj.GetType()
               .GetField(field,
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
               .SetValue(obj, value);
        }
    }
}
