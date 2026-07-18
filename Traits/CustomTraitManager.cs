using MBMScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM_Mod
{
    public static class CustomTraitManager
    {
        private static readonly Dictionary<Character,
            List<CustomTrait>> traits =
                new Dictionary<Character, List<CustomTrait>>();

        public static List<CustomTrait> Get(Character c)
        {
            if (!traits.TryGetValue(c, out var list))
            {
                list = new List<CustomTrait>();
                traits[c] = list;
            }

            return list;
        }
        public static void AddTrait(Character c,
                            string id,
                            string name)
        {
            var list = Get(c);

            if (list.Any(x => x.Id == id))
                return;

            list.Add(new CustomTrait()
            {
                Id = id,
                Name = name,
                Value = 0
            });
        }
        public static bool HasTrait(Character c,
                            string id)
        {
            return Get(c).Any(x => x.Id == id);
        }
        public static float GetValue(Character c,
                             string id)
        {
            var t = Get(c).FirstOrDefault(x => x.Id == id);

            return t?.Value ?? 0;
        }
    }
}
