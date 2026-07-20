using HarmonyLib;
using MBM_Mod;
using MBMScripts;

[HarmonyPatch(typeof(Character), "get_MentalityPercent")]
class Patch_MentalityPercent
{
    static void Postfix(Character __instance, ref float __result)
    {
        if (__instance.TraitContains((ETrait)1001))
        {
            __result = 0f;
        }
    }
}
