using HarmonyLib;
using HomoTool.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.SDKBase;

namespace HomoTool.Patches.UdonSync
{
    [HarmonyPatch(typeof(UdonSync_Internal), nameof(UdonSync_Internal.UdonSyncRunProgramAsRPC))]
    class UdonSyncRunProgramAsRPC
    {
        static bool Prefix(string param_1, Player_Internal param_2)
        {
            if (param_2.prop_VRCPlayerApi_0.displayName == Networking.LocalPlayer.displayName && param_1.Contains("Play") && ModuleManager.Instance.GetModule("SilentWalk").Enabled)
            {
                return false;
            }
            
            //Console.Instance.Log($"UdonSyncRunProgramAsRPC({param_1}, {param_2.prop_VRCPlayerApi_0.displayName}) was called.", LogLevel.Debug);
            //Plugin.Log.LogMessage($"UdonSyncRunProgramAsRPC({param_1}, {param_2.prop_VRCPlayerApi_0.displayName}) was called.");

            return true;
        }
    }
}
