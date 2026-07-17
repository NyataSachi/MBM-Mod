using MBMScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBM_Mod
{
    static class PayPatch

    {
        public static bool ForcePay(PlayData pd, Unit unit)
        {
            for (int i = 0; i < pd.PaySeqList.Count; i++)
            {
                if (pd.PaySeqList[i].Item1 == unit.Race &&
                    pd.PaySeqList[i].Item3 < pd.PaySeqList[i].Item2)
                {
                    pd.PaySeqList[i] = (
                        pd.PaySeqList[i].Item1,
                        pd.PaySeqList[i].Item2,
                        pd.PaySeqList[i].Item3 + 1);

                    return true;
                }
            }

            return false;
        }
    }
}
