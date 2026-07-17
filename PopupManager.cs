using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MBM_Mod
{
    public class PopupManager
    {
        private bool ShowPopup;
        private string PopupText;

        private int Lifetime;      // frames remaining
        private int Alpha = 255;   // transparency
        private int YPos;
        public void Popup(string text)
        {
            PopupText = text;
            ShowPopup = true;

            Lifetime = 120; // 2 seconds @ 60 FPS
            Alpha = 255;

            YPos = 40;
        }
        public void Update()
        {
            if (!ShowPopup)
                return;

            Lifetime--;

            // Wait 80 frames before fading
            if (Lifetime < 40)
            {
                Alpha -= 6;
                YPos--;
            }

            if (Lifetime <= 0 || Alpha <= 0)
            {
                ShowPopup = false;
            }
        }
        public void OnGUI()
        {
            if (!ShowPopup)
                return;

            Color old = GUI.color;

            GUI.color = new Color(
                1f,
                1f,
                1f,
                Alpha / 255f);

            GUI.Label(
                new Rect(Screen.width / 2, YPos, 200, 20),
                PopupText);

            GUI.color = old;
        }
    }
}
