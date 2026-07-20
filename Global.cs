

using BepInEx;
using MBMScripts;
using System;

namespace MBM_Mod
{
    class Global
    {
        public static int SellBonus;
        public static string sellBonusText;
        public static bool ToggleSellBonus = false;
        public static bool ShowWindow;
        public static bool LockGold = false;
        public static decimal LockedGoldAmount = 0;
        public static bool LockSoul = false;
        public static int LockedSoulAmount = 0;
        public static bool LockTime = false;
        public static double LockedTimeAmount = 0;
        public static bool LockAch = false;
        public static int LockedAchAmount = 0;
        public static bool LockRep = false;
        public static int LockedRepAmount = 0;

        public static int CurrentPage = 1;
        public static string PageText;
        public static int EPageValue;
        public static int MaxPage = 3;

        public static bool extraInfo = false;

        public static Unit SelectedUnit;

        public static void init()
        {
            sellBonusText =  SellBonus.ToString();
        }
        public static void Update()
        {
            
            EPageValue = CurrentPage;
            EPage page = (EPage)CurrentPage;
            if (!Enum.IsDefined(typeof(EPage), CurrentPage))
            {
                PageText = "Unknown";
            }
            else
            {
                PageText = ((EPage)CurrentPage).ToString();
            }

            if (PageText.IsNullOrWhiteSpace())
            {
                PageText = "Null or Empty";
            }

            SelectedUnit = GameManager.Instance.PlayerData.SelectedUnit;
            if (SelectedUnit == null)
            {
                SelectedUnit = new Unit(10000);
            }

            if (CurrentPage < 1)
            {
                CurrentPage = 1;
            }
            if (CurrentPage > MaxPage)
            {
                CurrentPage = MaxPage;
            }
            var pd = GameManager.Instance.PlayerData;
            if (Global.LockGold)
            {
                pd.Gold =
                    Global.LockedGoldAmount;
            }
            if (Global.LockTime)
            {
                pd.PlayTime =
                    (double)Global.LockedTimeAmount;
            }
            if (Global.LockSoul)
            {
                pd.Soul =
                    Global.LockedSoulAmount;
            }
            if (Global.LockAch)
            {
                pd.AchievementPoint =
                    Global.LockedAchAmount;
            }
            if (Global.LockRep)
            {
                pd.Reputation =
                    Global.LockedRepAmount;
            }
        }
    }
}
