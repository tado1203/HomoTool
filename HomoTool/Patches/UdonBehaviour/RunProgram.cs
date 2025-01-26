using HarmonyLib;
using HomoTool.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HomoTool.Patches.UdonBehaviour
{
    [HarmonyPatch]
    class RunProgram
    {
        static MethodBase TargetMethod()
        {
            var targetType = typeof(VRC.Udon.UdonBehaviour);
            var method = targetType.GetMethod(
                "RunProgram",
                new Type[] {
                    typeof(string),
                }
            );
            return method;
        }

        static void Prefix(VRC.Udon.UdonBehaviour __instance, string eventName)
        {
            // Console.Instance.Log($"{__instance.gameObject.name}.RunProgram({eventName}) was called.", LogLevel.Debug);
        }
    }
}