using HarmonyLib;
using MBMScripts;
using MBM_Mod;



namespace MBM_Mod
{
    [HarmonyPatch(typeof(Unit), "Sell")]
    class Patch_Unit_Sell
    {
        static void Postfix(int __result)
        {
            if (__result > 0)
            {
                GameManager.Instance.PlayerData.Gold += __result * Global.SellBonus;
            }
        }
    }
}
