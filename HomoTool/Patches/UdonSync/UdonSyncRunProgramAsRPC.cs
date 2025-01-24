using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HomoTool.Patches.UdonSync
{
    [HarmonyPatch(typeof(GameUdonSync), nameof(GameUdonSync.UdonSyncRunProgramAsRPC))]
    class UdonSyncRunProgramAsRPC
    {
        static void Prefix(string param_1, GamePlayer param_2)
        {
            Console.Instance.Log($"UdonSyncRunProgramAsRPC({param_1}, {param_2.prop_VRCPlayerApi_0.displayName}) called.", LogLevel.Debug);
        }
    }
}
