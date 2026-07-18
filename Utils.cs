using BepInEx;
using MBMScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MBM_Mod
{
    public static class Utils
    {
        public static void TrainingPoint(int point)
        {
            if (!(Global.SelectedUnit is Character character))
                return;
            character.Training = character.Training + point;
        }
    }
}
