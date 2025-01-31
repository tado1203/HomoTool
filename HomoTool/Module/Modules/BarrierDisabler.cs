using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.SDKBase;

namespace HomoTool.Module.Modules
{
    public class BarrierDisabler : ModuleBase
    {
        public BarrierDisabler() : base("BarrierDisabler", false, true) { }

        public override void OnEnable()
        {
            VRCPlayerApi localPlayer = Networking.LocalPlayer;
            if (localPlayer == null)
                return;
            
            foreach (var b in GameObject.FindObjectsOfType<GameObject>())
            {
                if (!b.name.Contains("barrier"))
                    continue;
                
                b.SetActive(false);
            }
        }
    }
}