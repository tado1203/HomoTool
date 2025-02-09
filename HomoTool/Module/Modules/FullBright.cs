using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.SDKBase;

namespace HomoTool.Module.Modules
{
    public class FullBright : ModuleBase
    {
        private Light light;

        public FullBright() : base("FullBright", false, true) { }

        public override void OnEnable()
        {
            if (Networking.LocalPlayer == null)
                return;

            light = Networking.LocalPlayer.gameObject.AddComponent<Light>();
            light.type = LightType.Directional;
            light.intensity = 1;
            light.range = 1000;
        }

        public override void OnDisable()
        {
            if (Networking.LocalPlayer == null)
            return;

            if (light != null)
            {
                MonoBehaviour.Destroy(light);
            }
        }
    }
}