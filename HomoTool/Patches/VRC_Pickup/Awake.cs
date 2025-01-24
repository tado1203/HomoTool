using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using PatchTargetClass = VRC.SDKBase.VRC_Pickup;

namespace HomoTool.Patches.VRC_Pickup
{
    [HarmonyPatch(typeof(PatchTargetClass), nameof(PatchTargetClass.Awake))]
    class Awake
    {
        static void Prefix(PatchTargetClass __instance)
        {
            __instance.DisallowTheft = false;
            __instance.pickupable = true;
            __instance.allowManipulationWhenEquipped = true;
            __instance.proximity = float.MaxValue;
        }
    }
}