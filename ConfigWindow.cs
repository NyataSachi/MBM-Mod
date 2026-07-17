using MBMScripts;
using UnityEngine;
using System.Reflection;
using System;

namespace MBM_Mod
{
    public static class ConfigWindow
    {

        public static bool EnableSellBonus = Global.ToggleSellBonus;
        public static int SellMultiplier = Global.SellBonus;
        private static string sellBonusText = Global.sellBonusText;

        private static string trait;

        private static int Xpos = 20;
        private static int Ypos = 120;
        private static float Alpha = 255;
        private static int overlayAlpha = 255;

        private static int clickCooldown;

        private static bool goldWasLocked;
        private static bool timeWasLocked;
        private static bool soulWasLocked;
        private static bool achWasLocked;
        private static bool repWasLocked;

        private static Rect windowRect = new Rect(Xpos, Ypos, 600, 400);
        private static Rect overlayRect = new Rect(0, 0, 30, 30);
        private static Rect textRect = new Rect(0, 0, 1000, 20);

        const int Left = 10;
        const int QLeft = 25;
        const int HalfMLeft = 50;
        const int QMLeft = 100;
        const int MiddleLeft = 150;
        const int Middle = 200;
        const int HalfMRight = 250;
        const int MiddleRight = 350;
        const int Right = 400;
        const int LabelWidth = 90;
        const int SmallButtonWidth = 20;
        const int ButtonWidth = 45;
        const int BigButtonWidth = 60;
        const int ToggleWidth = 140;
        const int RowHeight = 24;

        const int y = 40;

        public static void init()
        {
        }
        public static void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Global.CurrentPage += 1;
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (Global.CurrentPage > 1)
                {
                    Global.CurrentPage -= 1;
                }
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Global.extraInfo = true;
            }
            else
            {
                Global.extraInfo = false;
            }
            if (Input.GetKeyDown(KeyCode.F8) || Input.GetKeyDown(KeyCode.X))
            {
                Global.ShowWindow = !Global.ShowWindow;
                if (Global.ShowWindow)
                {
                    Cheesy.Log.LogInfo("Show Window");
                    Cheesy.Popup.Popup("Show Window");
                }
                else
                {
                    Cheesy.Log.LogInfo("Hide Window");
                    Cheesy.Popup.Popup("Hide Window");
                }
            }
            if (clickCooldown > 0)
            {
                --clickCooldown;
            }
            if (clickCooldown < 0)
            {
                clickCooldown = 0;
            }
        }

        public static void OnGUI()
        {
            Color old = GUI.color;
            GUI.backgroundColor = new Color(
                1f,
                0f,
                0f,
                255f);

            GUI.color = new Color(
                1f,
                1f,
                1f,
                Alpha / 255f);


            if (!Global.ShowWindow)
            {
                Alpha = 0;
                GUI.Window(1,
                    new Rect(Xpos, Ypos + 200, 40, 40),
                    DrawAntWindow,
                    "");
            }
            if (Global.ShowWindow)
            {

                if (Global.CurrentPage == 1)
                {
                    Alpha = 255f;
                    windowRect = GUI.Window(0,
                    windowRect,
                    Page1,
                    "Cheesy Cheats Mod Menu By AltSac");
                }
                if (Global.CurrentPage == 2)
                {
                    Alpha = 255f;
                    windowRect = GUI.Window(0,
                    windowRect,
                    Page2,
                    "Cheesy Cheats Mod Menu By AltSac");
                }
                if(Global.CurrentPage == 3)
                {
                    Alpha = 255f;
                    windowRect = GUI.Window(0,
                        windowRect,
                        Page3,
                        "Cheesy Cheats Mod Menu By AltSac");
                }
                GUI.color = old;
                GUI.backgroundColor = new Color(
                0f,
                0f,
                0f,
                overlayAlpha / 255f);
                overlayRect.x = windowRect.x + windowRect.width - 30;
                overlayRect.y = windowRect.y;
                overlayAlpha = 0;
                overlayRect = GUI.Window(1, overlayRect, Overlay, "");

                textRect.x = windowRect.x + (windowRect.width / 2) - 25;
                textRect.y = windowRect.y + 15;
                
                GUI.color = old;
                textRect = GUI.Window(2, textRect, PageText, "");

            }


        }
        private static void PageText(int id)
        {
            GUI.BringWindowToFront(id);
            GUI.Label(
                new Rect(0, 0, 100, 20), Global.PageText);
        }
        private static void Overlay(int id)
        {
            GUI.BringWindowToFront(id);
            if (GUI.Button(
                new Rect(0, 0, 30, 30),
                " X"))
            {
                Global.ShowWindow = !Global.ShowWindow;
            }
            
        }


        private static void Page1(int id)
        {
            

            var pd = GameManager.Instance.PlayerData;

            // Gold
            // Sellbonus
            EnableSellBonus =
                GUI.Toggle(
                    new Rect(Left, 40, ToggleWidth, 20),
                    EnableSellBonus,
                    "Enable Sell Bonus");

            GUI.Label(
                new Rect(Left, 60, LabelWidth, 20),
                "Sell Bonus:");

            sellBonusText = GUI.TextField(
                new Rect(Left + 80, 60, 50, 20),
                sellBonusText);
            int value;
            if (int.TryParse(sellBonusText, out value))
            {
                Global.SellBonus = value;
                Cheesy.SellBonus.Value = value;
            }
            int num1;
            // Add gold
            if (GUI.RepeatButton(
                new Rect(MiddleLeft, 60, SmallButtonWidth, 20),
                "+1000"))
            {
                if (clickCooldown == 0)
                {
                    pd.Gold += 1000;
                    clickCooldown = 10;
                }
            }
            // Decrease gold
            if (GUI.RepeatButton(
                new Rect(Middle, 60, SmallButtonWidth, 20),
                "-1000"))
            {
                if (clickCooldown == 0)
                {
                    pd.Gold -= 1000;
                    clickCooldown = 10;
                }
            }
            // Lock gold
            Global.LockGold = GUI.Toggle(
                new Rect(Left, 80, ToggleWidth, 20),
                Global.LockGold,
                "Lock Gold");
            if (Global.LockGold && !goldWasLocked)
            {
                Global.LockedGoldAmount =
                    pd.Gold;

                goldWasLocked = true;
            }

            if (!Global.LockGold)
            {
                goldWasLocked = false;
            }

            // Time
            GUI.Label(
                new Rect(Left, 100, LabelWidth, 20),
                "Time:");
            if (GUI.Button(
                new Rect(HalfMLeft, 100, BigButtonWidth, 20),
                "Set Day"))
            {
                SetPlayTime.SetDay();
                Cheesy.Popup.Popup("Set Day");
            }
            if (GUI.Button(
                new Rect(Middle, 100, BigButtonWidth, 25),
                "Set Night"))
            {
                SetPlayTime.SetNight();
                Cheesy.Popup.Popup("Set Night");
            }
            // Lock time
            Global.LockTime = GUI.Toggle(
                new Rect(Left, 120, ToggleWidth, 20),
                Global.LockTime,
                "Lock Time");
            if (Global.LockTime && !timeWasLocked)
            {
                Global.LockedTimeAmount =
                    SetPlayTime.GetCurrentTime();

                timeWasLocked = true;
            }

            if (!Global.LockGold)
            {
                timeWasLocked = false;
            }

            // Soul
            GUI.Label(new Rect(Left, 140, LabelWidth, 20), "Soul:");

            if (GUI.RepeatButton(
                new Rect(HalfMLeft, 140, BigButtonWidth, 20), "+100"))
            {
                if (clickCooldown == 0)
                {
                    pd.Soul += 100;
                    clickCooldown = 10;
                }
            }
            if (GUI.RepeatButton(
                new Rect(Middle, 140, BigButtonWidth, 20), "-100"))
            {
                if (clickCooldown == 0)
                {
                    pd.Soul -= 100;
                    clickCooldown = 10;
                }
            }
            if (GUI.Button(
                new Rect(HalfMRight, 140, ButtonWidth, 20),
                " Max"))
            {
                pd.Soul = 666;
            }
            Global.LockSoul = GUI.Toggle(
                new Rect(Left, 160, ToggleWidth, 20), Global.LockSoul, "Lock Soul");
            if (Global.LockSoul && !soulWasLocked)
            {
                Global.LockedSoulAmount =
                    pd.Soul;

                soulWasLocked = true;
            }

            // Achievement
            GUI.Label(
                new Rect(Left, 180, LabelWidth, 20), "Achievement:");
            if (GUI.RepeatButton(
                new Rect
                (HalfMLeft, 180, ButtonWidth, 20), "+10"))
            {
                if (clickCooldown == 0)
                {
                    pd.AchievementPoint += 10;
                    clickCooldown = 10;
                }
            }
            if (GUI.RepeatButton(
                new Rect
                (145, 180, 40, 20), "-10"))
            {
                if (clickCooldown == 0)
                {
                    pd.AchievementPoint -= 10;
                    clickCooldown = 10;
                }
            }
            Global.LockAch = GUI.Toggle(
                new Rect(10, 200, 150, 20), Global.LockAch, "Lock Achievement");
            if (Global.LockAch && !achWasLocked)
            {
                Global.LockedAchAmount =
                    pd.AchievementPoint;
                achWasLocked = true;
            }
            // Reputation
            GUI.Label(
                new Rect(10, 220, 100, 20), "Reputation");
            if (GUI.RepeatButton(
                new Rect(100, 220, 40, 20), "+10"))
            {
                if (clickCooldown == 0)
                {
                    pd.Reputation += 10;
                    clickCooldown = 10;
                }
            }
            if (GUI.RepeatButton(
                new Rect(145, 220, 40, 20), "-10"))
            {
                if (clickCooldown == 0)
                {
                    pd.Reputation -= 10;
                    clickCooldown = 10;
                }
            }
            if (GUI.Button(
                new Rect(195, 220, 40, 20),
                " Max"))
            {
                pd.Reputation += pd.MaxReputation;
            }
            Global.LockRep = GUI.Toggle(
                new Rect(10, 240, 150, 20), Global.LockRep, "Lock Reputation");
            if (Global.LockRep && !repWasLocked)
            {
                Global.LockedRepAmount =
                    pd.Reputation;
                repWasLocked = true;
            }

            GUI.Label(
                new Rect(10, 260, 100, 20),
                "Pixy:");
            if (GUI.Button(
                new Rect(120, 260, 20, 20),
                "+1"))
            {
                pd.FloraPixyCount += 1;
            }
            if (GUI.Button(
                new Rect(195, 260, 20, 20),
                "-1"))
            {
                pd.FloraPixyCount -= 1;
            }
            GUI.Label(
                new Rect(10, 280, 100, 20), "Day");
            if (GUI.RepeatButton(
                new Rect(120, 280, 25, 20), "+1"))
            {
                if(clickCooldown == 0)
                {
                    pd.PlayTime += 300;
                    //pd.Pa
                }
                clickCooldown = 10;
            }
            if (GUI.RepeatButton(
                new Rect(195, 280, 25, 20), "-1"))
            {
                if(clickCooldown == 0)
                {
                    pd.PlayTime -= 300;
                }
                clickCooldown = 10;
            }
            if(GUI.Button(
                new Rect(10, 300, 100, 20), "Complete Pay"))
            {
                PayPatch.ForcePay(pd, Global.SelectedUnit);
            }
            GUI.Label(
                new Rect(200, 300, 1000, 20), "Select a Unit to Pay");

            if(GUI.RepeatButton(
                new Rect(10, 320, 40, 20), "TenEgg count" ))
            {
                if(clickCooldown == 0)
                {
                    if (Global.extraInfo)
                    {
                        pd.TentacleEggCount -= 1;
                    }
                    else
                        pd.TentacleEggCount += 1;
                    clickCooldown = 5;
                }
            }
            GUI.DragWindow();
        }
        private static void Page2(int id)
        {
            var pd = GameManager.Instance.PlayerData;

            if (GUI.RepeatButton(
                new Rect(140, 40, 45, 40),
                "Items"))
            {
                if (Global.extraInfo)
                {
                    if(clickCooldown == 0)
                    {
                        addItem(EItemType.Item_FertilityMedication, ESector.Inventory, -5);
                        addItem(EItemType.Item_Aphrodisiac, ESector.Inventory, -5);
                        addItem(EItemType.Item_Condom, ESector.Inventory, -5);
                        addItem(EItemType.Item_CosmeticPill, ESector.Inventory, 5);
                        addItem(EItemType.Item_LoveGel, ESector.Inventory, 5);
                        addItem(EItemType.Item_SlaveCosmeticPill, ESector.Inventory, -5);
                        addItem(EItemType.Item_TraitUpgradePill, ESector.Inventory, -5);
                        addItem(EItemType.MammaryGlandRecoveryInjection, ESector.Inventory, -5);
                        addItem(EItemType.OvumRecoveryInjection, ESector.Inventory, -5);
                        addItem(EItemType.TattooRemovalInjection, ESector.Inventory, -5);
                        addItem(EItemType.VenerealDiseaseRecoveryInjection, ESector.Inventory, -5);
                        addItem(EItemType.HealthRecoveryInjection, ESector.Inventory, -5);
                    }
                }
                else
                {
                    if(clickCooldown == 0)
                    {
                        addItem(EItemType.Item_FertilityMedication, ESector.Inventory, 5);
                        addItem(EItemType.Item_Aphrodisiac, ESector.Inventory, 5);
                        addItem(EItemType.Item_Condom, ESector.Inventory, 5);
                        addItem(EItemType.Item_CosmeticPill, ESector.Inventory, 5);
                        addItem(EItemType.Item_LoveGel, ESector.Inventory, 5);
                        addItem(EItemType.Item_SlaveCosmeticPill, ESector.Inventory, 5);
                        addItem(EItemType.Item_TraitUpgradePill, ESector.Inventory, 5);
                        addItem(EItemType.MammaryGlandRecoveryInjection, ESector.Inventory, 5);
                        addItem(EItemType.OvumRecoveryInjection, ESector.Inventory, 5);
                        addItem(EItemType.TattooRemovalInjection, ESector.Inventory, 5);
                        addItem(EItemType.VenerealDiseaseRecoveryInjection, ESector.Inventory, 5);
                        addItem(EItemType.HealthRecoveryInjection, ESector.Inventory, 5);
                    }
                }

            }
            if(GUI.RepeatButton(
                new Rect(300, 40, 40, 40),
                "DNAs"))
            {
                if(clickCooldown == 0)
                {
                    if(Global.extraInfo)
                    {
                        addItem(EItemType.InuMammaryGlandDna, ESector.Inventory, -5);
                        addItem(EItemType.Item_ElfDna, ESector.Inventory, -5);
                        addItem(EItemType.Item_GoblinDna, ESector.Inventory, -5);
                        addItem(EItemType.Item_HitsujiDna, ESector.Inventory, -5);
                        addItem(EItemType.Item_HumanDna, ESector.Inventory, -5);
                        addItem(EItemType.Item_InuDna, ESector.Inventory, -5);
                        addItem(EItemType.Item_MinotaurDna, ESector.Inventory, -5);
                        addItem(EItemType.Item_NekoDna, ESector.Inventory, -5);
                        addItem(EItemType.Item_OrcDna, ESector.Inventory, -5);
                        addItem(EItemType.Item_OriginDna, ESector.Inventory, -5);
                        addItem(EItemType.Item_SalamanderDna, ESector.Inventory, -5);
                        addItem(EItemType.Item_UsagiDna, ESector.Inventory, -5);
                        addItem(EItemType.Item_WerewolfDna, ESector.Inventory, -5);
                        addItem(EItemType.NecoOvarianDna, ESector.Inventory, -5);
                        addItem(EItemType.UsagiWombDna, ESector.Inventory, -5);
                    }
                    else
                    {
                        addItem(EItemType.InuMammaryGlandDna, ESector.Inventory, 5);
                        addItem(EItemType.Item_ElfDna, ESector.Inventory, 5);
                        addItem(EItemType.Item_GoblinDna, ESector.Inventory, 5);
                        addItem(EItemType.Item_HitsujiDna, ESector.Inventory, 5);
                        addItem(EItemType.Item_HumanDna, ESector.Inventory, 5);
                        addItem(EItemType.Item_InuDna, ESector.Inventory, 5);
                        addItem(EItemType.Item_MinotaurDna, ESector.Inventory, 5);
                        addItem(EItemType.Item_NekoDna, ESector.Inventory, 5);
                        addItem(EItemType.Item_OrcDna, ESector.Inventory, 5);
                        addItem(EItemType.Item_OriginDna, ESector.Inventory, 5);
                        addItem(EItemType.Item_SalamanderDna, ESector.Inventory, 5);
                        addItem(EItemType.Item_UsagiDna, ESector.Inventory, 5);
                        addItem(EItemType.Item_WerewolfDna, ESector.Inventory, 5);
                        addItem(EItemType.NecoOvarianDna, ESector.Inventory, 5);
                        addItem(EItemType.UsagiWombDna, ESector.Inventory, 5);
                        
                    }
                    clickCooldown = 10;
                }
            }
            if (GUI.Button(
                new Rect(10, 40, 85, 40),
                "EVERYTING"))
            {
                addItem(EItemType.BodyFluid, ESector.ByProduct, 1);
                addItem(EItemType.DawrfHeart, ESector.ByProduct, 1);
                addItem(EItemType.DragonianMilk, ESector.Inventory, 1);
                addItem(EItemType.DragonTailMeat, ESector.ByProduct, 1);
                addItem(EItemType.DwarfMilk, ESector.Inventory, 1);
                addItem(EItemType.ElfManaEngine, ESector.ByProduct, 1);
                addItem(EItemType.ElfMilk, ESector.Inventory, 1);
                addItem(EItemType.FurryMilk, ESector.Inventory, 1);
                addItem(EItemType.GoblinSemen, ESector.ByProduct, 1);
                addItem(EItemType.HealthRecoveryInjection, ESector.Inventory, 1);
                addItem(EItemType.HitsujiHorn, ESector.ByProduct, 1);
                addItem(EItemType.HumanMilk, ESector.Inventory, 1);
                addItem(EItemType.HumanPheromone, ESector.ByProduct, 1);
                addItem(EItemType.InuMammaryGlandDna, ESector.Inventory, 1);
                addItem(EItemType.Item_Aphrodisiac, ESector.Inventory, 1);
                addItem(EItemType.Item_CosmeticPill, ESector.Inventory, 1);
                addItem(EItemType.Item_DragonianDna, ESector.Inventory, 1);
                addItem(EItemType.Item_DwarfDna, ESector.Inventory, 1);
                addItem(EItemType.Item_ElfDna, ESector.Inventory, 1);
                addItem(EItemType.Item_FertilityMedication, ESector.Inventory, 1);
                addItem(EItemType.Item_GoblinDna, ESector.Inventory, 1);
                addItem(EItemType.Item_HitsujiDna, ESector.Inventory, 1);
                addItem(EItemType.Item_HumanDna, ESector.Inventory, 1);
                addItem(EItemType.Item_InuDna, ESector.Inventory, 1);
                addItem(EItemType.Item_LoveGel, ESector.Inventory, 1);
                addItem(EItemType.Item_MinotaurDna, ESector.Inventory, 1);
                addItem(EItemType.Item_MonsterCosmeticPill, ESector.Inventory, 1);
                addItem(EItemType.Item_NekoDna, ESector.Inventory, 1);
                addItem(EItemType.Item_OrcDna, ESector.Inventory, 1);
                addItem(EItemType.Item_OriginDna, ESector.Inventory, 1);
                addItem(EItemType.Item_SalamanderDna, ESector.Inventory, 1);
                addItem(EItemType.Item_SalamanderScalePiece, ESector.ByProduct, 1);
                addItem(EItemType.Item_Sensitivity3000x, ESector.Inventory, 1);
                addItem(EItemType.Item_SlaveCosmeticPill, ESector.Inventory, 1);
                addItem(EItemType.Item_TentacleEgg, ESector.ByProduct, 1);
                addItem(EItemType.Item_TraitUpgradePill, ESector.Inventory, 1);
                addItem(EItemType.Item_UsagiDna, ESector.Inventory, 1);
                addItem(EItemType.Item_ViOgra, ESector.Inventory, 1);
                addItem(EItemType.Item_WerewolfDna, ESector.Inventory, 1);
                addItem(EItemType.LoliMilk, ESector.Inventory, 1);
                addItem(EItemType.MammaryGlandRecoveryInjection, ESector.Inventory, 1);
                addItem(EItemType.Milk, ESector.Inventory, 1);
                addItem(EItemType.MinotaurSkin, ESector.ByProduct, 1);
                addItem(EItemType.NecoOvarianDna, ESector.Inventory, 1);
                addItem(EItemType.OrcHeart, ESector.ByProduct, 1);
                addItem(EItemType.OvumRecoveryInjection, ESector.Inventory, 1);
                addItem(EItemType.SmallFurryMilk, ESector.Inventory, 1);
                addItem(EItemType.TattooRemovalInjection, ESector.Inventory, 1);
                addItem(EItemType.UsagiWombDna, ESector.Inventory, 1);
                addItem(EItemType.VenerealDiseaseDna, ESector.ByProduct, 1);
                addItem(EItemType.VenerealDiseaseRecoveryInjection, ESector.Inventory, 1);
                addItem(EItemType.WerewolfTail, ESector.ByProduct, 1);
                addItem(EItemType.Item_Condom, ESector.Inventory, 1);
            }
            if (GUI.RepeatButton(
                new Rect(200, 40, 80, 40), "Ovum"))
            {
                if (Global.extraInfo)
                {
                    if (clickCooldown == 0)
                    {
                        addItem(EItemType.OvumRecoveryInjection, ESector.Inventory, -5);
                        clickCooldown = 10;
                    }
                }
                else
                {
                    if (clickCooldown == 0)
                    {
                        addItem(EItemType.OvumRecoveryInjection, ESector.Inventory, 5);
                        clickCooldown = 10;
                    }
                }
            }
            GUI.Label(
                new Rect(10, 60, 100, 20),
                "Likability");

            if(GUI.RepeatButton(
                new Rect(10, 80, 60, 20), "Amilia"))
            {
                if(clickCooldown == 0)
                {
                    if(Global.extraInfo)
                    {
                        pd.LikeabilityOfAmilia -= 1;
                        
                    }
                    else
                        pd.LikeabilityOfAmilia += 1;
                }
                if (pd.LikeabilityOfAmilia >= 8)
                {
                    pd.SetLikeability((ELikeability)1, 1);
                    pd.SetLikeability((ELikeability)2, 1);
                    pd.SetLikeability((ELikeability)3, 1);
                    pd.SetLikeability((ELikeability)4, 1);
                    pd.SetLikeability((ELikeability)5, 1);
                    pd.SetLikeability((ELikeability)6, 1);
                    pd.SetLikeability((ELikeability)7, 1);
                    pd.SetLikeability((ELikeability)8, 1);
                    pd.SetLikeability((ELikeability)9, 1);
                    pd.SetLikeability((ELikeability)10, 1);
                    pd.SetLikeability((ELikeability)11, 1);
                    pd.SetLikeability((ELikeability)12, 1);
                }
                clickCooldown = 10;

            }
            if(GUI.RepeatButton(
                new Rect(60, 80, 60, 20), "Barbara"))
            {
                if(clickCooldown == 0)
                {
                    if(Global.extraInfo)
                    {
                        pd.LikeabilityOfBarbara -= 1;
                            
                    }
                    else
                        pd.LikeabilityOfBarbara += 1;
                    
                    clickCooldown = 10;
                }
                if (pd.LikeabilityOfBarbara >= 8)
                {
                    pd.SetLikeability((ELikeability)401, 1);
                    pd.SetLikeability((ELikeability)402, 1);
                    pd.SetLikeability((ELikeability)403, 1);
                    pd.SetLikeability((ELikeability)404, 1);
                    pd.SetLikeability((ELikeability)405, 1);
                    pd.SetLikeability((ELikeability)406, 1);
                    pd.SetLikeability((ELikeability)407, 1);
                    pd.SetLikeability((ELikeability)408, 1);
                    pd.SetLikeability((ELikeability)409, 1);
                    pd.SetLikeability((ELikeability)410, 1);
                    pd.SetLikeability((ELikeability)411, 1);
                    pd.SetLikeability((ELikeability)412, 1);
                }

            }
            if(GUI.RepeatButton(
                new Rect(120, 80, 60, 20), "Flora"))
            {
                if(clickCooldown == 0)
                {
                    if(Global.extraInfo)
                    {
                        pd.LikeabilityOfFlora -= 1;
                    }
                    else
                        pd.LikeabilityOfFlora += 1;
                    clickCooldown = 10;
                }
                if (pd.LikeabilityOfFlora >= 8)
                {
                    pd.SetLikeability((ELikeability)101, 1);
                    pd.SetLikeability((ELikeability)102, 1);
                    pd.SetLikeability((ELikeability)103, 1);
                    pd.SetLikeability((ELikeability)104, 1);
                    pd.SetLikeability((ELikeability)105, 1);
                    pd.SetLikeability((ELikeability)106, 1);
                    pd.SetLikeability((ELikeability)107, 1);
                    pd.SetLikeability((ELikeability)108, 1);
                    pd.SetLikeability((ELikeability)109, 1);
                    pd.SetLikeability((ELikeability)110, 1);
                    pd.SetLikeability((ELikeability)111, 1);
                    pd.SetLikeability((ELikeability)112, 1);
                }

            }
            if(GUI.RepeatButton(
                new Rect(180, 80, 60, 20), "Niel"))
            {
                if(clickCooldown == 0)
                {
                    if (Global.extraInfo)
                    {
                        pd.LikeabilityOfNiel -= 1;
                    }
                    else
                        pd.LikeabilityOfNiel += 1;
                    clickCooldown = 10;
                }
                if (pd.LikeabilityOfNiel >= 8)
                {
                    pd.SetLikeability((ELikeability)201, 1);
                    pd.SetLikeability((ELikeability)202, 1);
                    pd.SetLikeability((ELikeability)203, 1);
                    pd.SetLikeability((ELikeability)204, 1);
                    pd.SetLikeability((ELikeability)205, 1);
                    pd.SetLikeability((ELikeability)206, 1);
                    pd.SetLikeability((ELikeability)207, 1);
                    pd.SetLikeability((ELikeability)208, 1);
                    pd.SetLikeability((ELikeability)209, 1);
                    pd.SetLikeability((ELikeability)210, 1);
                    pd.SetLikeability((ELikeability)211, 1);
                    pd.SetLikeability((ELikeability)212, 1);
                }

            }
            if (GUI.RepeatButton(
                new Rect(240, 80, 60, 20), "Sena and Lena"))
            {
                if (clickCooldown == 0)
                {
                    if (Global.extraInfo)
                    {
                        pd.LikeabilityOfSenaLena -= 1;
                    }
                    else
                        pd.LikeabilityOfSenaLena += 1;
                    clickCooldown = 10;
                }
                if (pd.LikeabilityOfSenaLena >= 8)
                {
                    pd.SetLikeability((ELikeability)301, 1);
                    pd.SetLikeability((ELikeability)302, 1);
                    pd.SetLikeability((ELikeability)303, 1);
                    pd.SetLikeability((ELikeability)304, 1);
                    pd.SetLikeability((ELikeability)305, 1);
                    pd.SetLikeability((ELikeability)306, 1);
                    pd.SetLikeability((ELikeability)307, 1);
                    pd.SetLikeability((ELikeability)308, 1);
                    pd.SetLikeability((ELikeability)309, 1);
                    pd.SetLikeability((ELikeability)310, 1);
                    pd.SetLikeability((ELikeability)311, 1);
                    pd.SetLikeability((ELikeability)312, 1);
                }

            }
            if(GUI.RepeatButton(
                new Rect(10, 140, 100, 20), "Deprave Point"))
            {
                if(clickCooldown == 0)
                {
                    if (Global.extraInfo)
                    {
                        Utils.TrainingPoint(-1);
                    }
                    else
                        Utils.TrainingPoint(1);
                    clickCooldown = 10;
                }
                
            }

            GUI.DragWindow();

        }
        private static void Page3(int id)
        {
            int traitID = 0;
            trait = GUI.TextField(
                new Rect(10, 40, 100, 20),
                trait);
            if (int.TryParse(trait, out int value))
            {
                traitID = value;
            }
            if (GUI.Button(
                new Rect(105, 40, 100, 20), "Add/Upgrade"))
            {
                Utils.AddUpgradeTrait(traitID);
            }
            if(GUI.Button(
                new Rect(210, 40, 60, 20), "Decrease"))
            {
                Utils.RemoveDecreaseTrait(false, traitID);
            }
            if (GUI.Button(
                new Rect(315, 40, 60, 20), "Remove"))
            {
                Utils.RemoveDecreaseTrait(true, traitID);
            }
            GUI.DragWindow();
        }
        private static void addItem(EItemType item, ESector sector, int count)
        {
            var pd = GameManager.Instance.PlayerData;

            pd.NewItem(item,
                    ESector.Inventory,
                    new ValueTuple<int, int>(1, 0),
                    -1,
                    count,
                    true);
        }

        private static void DrawAntWindow (int id)
        {
            if (GUI.Button(
                new Rect(0, 0, 25, 25),
                " X"))
            {
                Global.ShowWindow = !Global.ShowWindow;
            }
        }
    }
}