using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.SDKBase;

namespace HomoTool.Module.Modules
{
    public class Checkpoint : ModuleBase
    {
        Vector3 targetPos;
        Quaternion targetRotation;

        public Checkpoint() : base("Checkpoint", false, true) { }

        public override void OnEnable()
        {
            VRCPlayerApi localPlayer = Networking.LocalPlayer;
            if (localPlayer == null)
                return;

            targetPos = localPlayer.gameObject.transform.position;
            targetRotation = localPlayer.gameObject.transform.rotation;
        }

        public override void OnDisable()
        {
            VRCPlayerApi localPlayer = Networking.LocalPlayer;
            if (localPlayer == null)
                return;

            localPlayer.gameObject.transform.position = targetPos;
            localPlayer.gameObject.transform.rotation = targetRotation;
        }
    }
}