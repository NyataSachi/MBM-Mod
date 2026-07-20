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
        private static readonly Dictionary<Character, List<CustomTrait>> traits
            = new Dictionary<Character, List<CustomTrait>>();

        public static void AddTrait(Character character, int id, float value = 0f)
        {
            var list = Get(character);

            if (list.Any(x => x.Id == id))
                return;

            list.Add(new CustomTrait
            {
                Id = id,
            });
        }

        public static List<CustomTrait> Get(Character character)
        {
            if (!traits.TryGetValue(character, out var list))
            {
                list = new List<CustomTrait>();
                traits[character] = list;
            }

            return list;
        }

        public static bool HasTrait(Character character, int id)
            => Get(character).Any(x => x.Id == id);

    }
}
