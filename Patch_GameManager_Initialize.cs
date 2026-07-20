using HarmonyLib;
using MBM_Mod;
using MBMScripts;

[HarmonyPatch(typeof(GameManager), "Initialize")]
class Patch_GameManager_Initialize
{
    static void Postfix()
    {
        CustomTraitRegistry.Initialize();
    }
}
