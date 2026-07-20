using HarmonyLib;
using MBMScripts;
using UnityEngine;

namespace MBM_Mod.Traits
{
    [HarmonyPatch(typeof(ReferenceCharacterStatePlate), nameof(ReferenceCharacterStatePlate.GetFloat))]
    class Patch_StatePlate
    {
        private static readonly AccessTools.FieldRef<
            ReferenceCharacterStatePlate,
            int> dataTypeRef =
            AccessTools.FieldRefAccess<
                ReferenceCharacterStatePlate,
                int>("m_DataType");

        static bool Prefix(ReferenceCharacterStatePlate __instance, ref float __result)
        {
            Character character = __instance.Updater?.TargetUnit?.Unit as Character;
            if (character == null)
                return true;

            if (!character.TraitContains((ETrait)1001))
                return true;

            int dataType = dataTypeRef(__instance);

            float mentalityPercent = character.Health / character.MaxHealth;

            const int MentalityPercent = 1;
            const int MentalityPercentForDisc = 20;

            switch (dataType)
            {
                case MentalityPercent:
                    __result = Mathf.Lerp(0f, 0.428f, mentalityPercent);
                    return false;

                case MentalityPercentForDisc:
                    __result = Mathf.Lerp(5.263913f, 2.581342f, mentalityPercent);
                    return false;
            }

            return true;
        }
    }
}