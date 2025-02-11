using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ExitGames.Client.Photon;
using HarmonyLib;
using HomoTool.Helpers;
using HomoTool.Managers;
using UnityEngine;
using VRC.SDKBase;

namespace HomoTool.Patches.LoadBalancingClient
{
    [HarmonyPatch(typeof(LoadBalancingClient_Internal), nameof(LoadBalancingClient_Internal.Method_Public_Virtual_New_Boolean_Byte_Object_ObjectPublicObByObInByObObUnique_SendOptions_0))]
    class OpRaiseEvent
    {
        static bool Prefix(byte param_1, ref System.Object param_2, ObjectPublicObByObInByObObUnique param_3, SendOptions param_4)
        {
            // Plugin.Log.LogMessage($"[OpRaiseEvent] EventCode: {param_1}");
            if (param_1 == 12)
            {
                if (ModuleManager.Instance.GetModule("NoMovementPacket").Enabled)
                    return false;
                
                if (ModuleManager.Instance.GetModule("MovementFucker").Enabled)
                {
                    byte[] data = SerializationHelper.FromIL2CPPToManaged<byte[]>((Il2CppSystem.Object)param_2);
                    // position data
                    int startIndex = 21;
                    // size of vector3
                    int fillLength = 12;

                    for (int i = startIndex; i < startIndex + fillLength; i++)
                    {
                        data[i] = 0x46;
                    }

                    param_2 = SerializationHelper.FromManagedToIL2CPP<Il2CppSystem.Object>(data);
                }

                // string byteString = string.Join(",", data.Select(b => b.ToString("X2")));
                // Console.Instance.Log($"[OpRaiseEvent] {byteString}");
            }

            return true;
        }
    }
}