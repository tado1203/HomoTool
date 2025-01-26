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
    public class Speed : ModuleBase
    {
        private float speed = 6f;
        private float jumpHeight = 4f;

        public Speed() : base("Speed", false, true, KeyCode.E) { }

        public override void OnUpdate()
        {
            VRCPlayerApi localPlayer = Networking.LocalPlayer;
            if (localPlayer != null && Enabled)
            {
                Transform transform = localPlayer.gameObject.transform;
                bool isMoving = false;

                if (Input.GetKey(KeyCode.W))
                {
                    transform.position += transform.forward * speed * Time.deltaTime;
                    isMoving = true;
                }

                if (Input.GetKey(KeyCode.S))
                {
                    transform.position -= transform.forward * speed * Time.deltaTime;
                    isMoving = true;
                }

                if (Input.GetKey(KeyCode.A))
                {
                    transform.position -= transform.right * speed * Time.deltaTime;
                    isMoving = true;
                }

                if (Input.GetKey(KeyCode.D))
                {
                    transform.position += transform.right * speed * Time.deltaTime;
                    isMoving = true;
                }

                if (isMoving && localPlayer.IsPlayerGrounded())
                {
                    localPlayer.SetVelocity(new Vector3(0f, jumpHeight, 0f));
                }
            }
        }

        public override void OnMenu()
        {
            GUILayout.Label($"Speed: {speed}");
            speed = GUILayout.HorizontalSlider(speed, 4f, 20f);
            GUILayout.Label($"Jump Height: {jumpHeight}");
            jumpHeight = GUILayout.HorizontalSlider(jumpHeight, 3f, 10f);
        }

        public override void OnEnable()
        {
            ModuleManager.Instance.GetModule("NoMovementPacket").Enable();
        }

        public override void OnDisable()
        {
            ModuleManager.Instance.GetModule("NoMovementPacket").Disable();
        }
    }
}
