using HarmonyLib;
using HomoTool.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomoTool.Patches
{
    [HarmonyPatch(typeof(MonoBehaviourPublicDaBoAc1ObDiOb2InHaUnique), nameof(MonoBehaviourPublicDaBoAc1ObDiOb2InHaUnique.OnPlayerLeft))]
    class OnPlayerLeft
    {
        static void Postfix(GamePlayer param_1)
        {
            ModuleManager.Instance.OnPlayerLeft(param_1);
        }
    }
}
