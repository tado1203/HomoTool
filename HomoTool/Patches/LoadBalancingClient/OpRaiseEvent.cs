using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExitGames.Client.Photon;
using HarmonyLib;
using HomoTool.Managers;

namespace HomoTool.Patches.LoadBalancingClient
{
    [HarmonyPatch(typeof(GameLoadBalancingClient), nameof(GameLoadBalancingClient.Method_Public_Virtual_New_Boolean_Byte_Object_ObjectPublicObByObInByObObUnique_SendOptions_0))]
    class OpRaiseEvent
    {
        static bool Prefix(byte param_1, Object param_2, ObjectPublicObByObInByObObUnique param_3, SendOptions param_4)
        {
            // movement event
            if (param_1 == 12)
                if (ModuleManager.Instance.GetModule("Flight").Enabled || ModuleManager.Instance.GetModule("Speed").Enabled)
                    return false;

            return true;
        }
    }
}