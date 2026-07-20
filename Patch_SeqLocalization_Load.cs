using HarmonyLib;
using MBMScripts;


namespace MBM_Mod
{
    [HarmonyPatch(typeof(SeqLocalization), nameof(SeqLocalization.Load))]
    class Patch_SeqLocalization_Load
    {
        static void Postfix()
        {
            LocalizationInjector.InjectAll();
        }
    }
}
