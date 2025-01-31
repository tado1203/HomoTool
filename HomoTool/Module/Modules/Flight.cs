using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomoTool.Managers;
using UnityEngine;
using VRC.SDKBase;

namespace HomoTool.Module.Modules
{
    public class Flight : ModuleBase
    {
        private float speed = 8.0f;
        private bool noMovementPacket = true;
        private bool checkpoint = false;

        public Flight() : base("Flight", false, true, KeyCode.F) { }

        public override void OnUpdate()
        {
            VRCPlayerApi localPlayer = Networking.LocalPlayer;
            if (localPlayer != null && Enabled)
            {
                Transform transform = localPlayer.gameObject.transform;

                if (Input.GetKey(KeyCode.W))
                    transform.position += transform.forward * speed * Time.deltaTime;

                if (Input.GetKey(KeyCode.S))
                    transform.position -= transform.forward * speed * Time.deltaTime;

                if (Input.GetKey(KeyCode.A))
                    transform.position -= transform.right * speed * Time.deltaTime;

                if (Input.GetKey(KeyCode.D))
                    transform.position += transform.right * speed * Time.deltaTime;

                if (Input.GetKey(KeyCode.Space))
                    transform.position += transform.up * speed * Time.deltaTime;

                if (Input.GetKey(KeyCode.LeftShift))
                    transform.position -= transform.up * speed * Time.deltaTime;

                localPlayer.SetVelocity(new Vector3(0, 0, 0));
            }
        }

        public override void OnMenu()
        {
            noMovementPacket = GUILayout.Toggle(noMovementPacket, "NoMovementPacket");
            checkpoint = GUILayout.Toggle(checkpoint, "Checkpoint");
            GUILayout.Label($"Speed: {speed}");
            speed = GUILayout.HorizontalSlider(speed, 0f, 50f);
        }

        public override void OnEnable()
        {
            if (noMovementPacket)
                ModuleManager.Instance.GetModule("NoMovementPacket").Enable();
            if (checkpoint)
                ModuleManager.Instance.GetModule("Checkpoint").Enable();
            VRCPlayerApi localPlayer = Networking.LocalPlayer;
            if (localPlayer != null)
                localPlayer.gameObject.GetComponent<CharacterController>().enabled = false;
        }

        public override void OnDisable()
        {
            if (noMovementPacket)
                ModuleManager.Instance.GetModule("NoMovementPacket").Disable();
            ModuleManager.Instance.GetModule("Checkpoint").Disable();
            VRCPlayerApi localPlayer = Networking.LocalPlayer;
            if (localPlayer != null)
                localPlayer.gameObject.GetComponent<CharacterController>().enabled = true;
        }
    }
}
