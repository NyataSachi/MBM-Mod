using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using MBM_Mod;
using MBMScripts;
using UnityEngine;

[BepInPlugin("altsac.mbmmod", "MBM Cheesy cheats", "1.0")]
public class Cheesy : BaseUnityPlugin
{
    public static ConfigEntry<bool> ToggleSellBonus;
    public static ConfigEntry<int> SellBonus;
    public static ConfigEntry<bool> LockGold;
    public static ConfigEntry<bool> LockSoul;
    public static ConfigEntry<bool> LockAch;
    public static ConfigEntry<bool> LockRep;
    public static ConfigEntry<bool> LockTime;
    public static BepInEx.Logging.ManualLogSource Log;
    public static PopupManager Popup;
    private void Awake()
    {
        Log = Logger;
        Logger.LogInfo("Cheesy cheats Loaded");
        Harmony harmony = new Harmony("altsac.mbmmod");
        harmony.PatchAll();
        Popup = new PopupManager();

        ToggleSellBonus = Config.Bind(
            "General",
            "EnableSellBonus",
            false,
            "Enable sell bonus");

        SellBonus = Config.Bind(
            "General",
            "SellBonus",
            0,
            "Sell Bonus");
        LockGold = Config.Bind(
            "General",
            "LockGold",
            false,
            "Lock Gold");
        LockTime = Config.Bind(
            "General",
            "LockTime",
            false,
            "Lock Time");
        LockSoul = Config.Bind(
            "General",
            "LockSoul",
            false,
            "Lock Soul");
        LockAch = Config.Bind(
            "General",
            "LockAch",
            false,
            "Lock Ach");
        LockRep = Config.Bind(
            "General",
            "LockRep",
            false,
            "Lock Rep");
        Global.ToggleSellBonus = ToggleSellBonus.Value;
        Global.SellBonus = SellBonus.Value;
        // Begin init construct
        Global.init();
        // End init construct
    }
    private void Update()
    {

        ConfigWindow.Update();
        Popup.Update();
        Global.Update();
    }

    private void OnGUI()
    {
        ConfigWindow.OnGUI();
        Popup.OnGUI();
    }
    
}