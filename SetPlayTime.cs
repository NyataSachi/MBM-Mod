using MBMScripts;
using System;
using System.Reflection;


namespace MBM_Mod
{
    public static class SetPlayTime
    {
        public static void SetDay()
        {
            var field = typeof(PlayData).GetField(
                "m_DayGauge",
                BindingFlags.Instance | BindingFlags.NonPublic);
         
            

            var pd = GameManager.Instance.PlayerData;
            double day = Math.Floor(pd.PlayTime / GameManager.ConfigData.SecondsOfDay);
            pd.PlayTime = day * 
                GameManager.ConfigData.SecondsOfDay +
                GameManager.ConfigData.SecondsOfDay * 0.0;
        }
        public static void SetNight()
        {
            var field = typeof(PlayData).GetField(
                "m_DayGauge",
                BindingFlags.Instance | BindingFlags.NonPublic);
            var pd = GameManager.Instance.PlayerData;
            double day =Math.Floor(pd.PlayTime / GameManager.ConfigData.SecondsOfDay);

            pd.PlayTime = day * 
               GameManager.ConfigData.SecondsOfDay + 
               GameManager.ConfigData.SecondsOfDay * 0.5;
        }
        public static double GetCurrentTime()
        {
            var pd = GameManager.Instance.PlayerData;
            return (pd.PlayTime);
        }
    }
}
