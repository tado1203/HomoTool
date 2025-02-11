using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using VRC.SDKBase;

namespace HomoTool.Patches.VRC_EventHandler
{
    [HarmonyPatch]
    class TriggerEvent
    {
        static MethodBase TargetMethod()
        {
            var targetType = typeof(VRC.SDKBase.VRC_EventHandler);
            var method = targetType.GetMethod(
                "TriggerEvent",
                new Type[] {
                    typeof(VRC.SDKBase.VRC_EventHandler.VrcEvent),
                    typeof(VRC.SDKBase.VRC_EventHandler.VrcBroadcastType),
                    typeof(GameObject),
                    typeof(float),
                }
            );
            return method;
        }

        static void Prefix(VRC.SDKBase.VRC_EventHandler.VrcEvent e, VRC.SDKBase.VRC_EventHandler.VrcBroadcastType broadcast, GameObject instagator = null, float fastForward = 0f)
        {
            // Plugin.Log.LogMessage($"[VRC_EventHandler] {e.Name}: EventType={e.EventType}, Bool={e.ParameterBool}, BoolOp={e.ParameterBoolOp}, Float={e.ParameterFloat}, Int={e.ParameterInt}, String={e.ParameterString}");
        }
    }
}