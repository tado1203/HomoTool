using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using HomoTool.Managers;
using VRC.SDKBase;

namespace HomoTool.Patches
{
    [HarmonyPatch(typeof(MonoBehaviourPublicDaBoAc1ObDiOb2InHaUnique), nameof(MonoBehaviourPublicDaBoAc1ObDiOb2InHaUnique.OnPlayerJoined))]
    class OnPlayerJoined
    {
        static void Postfix(Player_Internal param_1)
        {
            ModuleManager.Instance.OnPlayerJoined(param_1);
        }
    }
}