using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRC.SDKBase;

namespace HomoTool.Module.Modules
{
    public class AirJump : ModuleBase
    {
        private float jumpHeight = 5f;

        public AirJump() : base("AirJump", false, true, KeyCode.None) { }

        public override void OnUpdate()
        {
            VRCPlayerApi localPlayer = Networking.LocalPlayer;
            if (localPlayer == null || !Enabled)
                return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                localPlayer.SetVelocity(new Vector3(0f, jumpHeight, 0f));
            }
        }

        public override void OnMenu()
        {
            GUILayout.Label($"Jump Height: {jumpHeight}");
            jumpHeight = GUILayout.HorizontalSlider(jumpHeight, 0f, 10f);
        }
    }
}
