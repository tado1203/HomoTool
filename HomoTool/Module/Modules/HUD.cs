using HomoTool.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.SDKBase;

namespace HomoTool.Module.Modules
{
    public class HUD : ModuleBase
    {
        private readonly int fontSize = 24;
        private List<string> hudItems = new List<string>();
        private Vector3 lastPosition = Vector3.zero;
        private float playerSpeed = 0f;
        private bool isInitialized = false;

        public HUD() : base("HUD", true, false, KeyCode.None) { }

        public override void OnUpdate()
        {
            VRCPlayerApi localPlayer = Networking.LocalPlayer;
            if (localPlayer != null)
            {
                Vector3 currentPosition = localPlayer.gameObject.transform.position;

                if (isInitialized)
                {
                    float deltaTime = Time.deltaTime;
                    if (deltaTime > 0f)
                    {
                        playerSpeed = (currentPosition - lastPosition).magnitude / deltaTime;
                    }
                }
                else
                {
                    lastPosition = currentPosition;
                    isInitialized = true;
                }

                lastPosition = currentPosition;
            }
        }

        public override void OnGUI()
        {
            hudItems.Clear();

            float fps = 1.0f / Time.smoothDeltaTime;
            string fpsText = $"FPS: {Mathf.Ceil(fps)}";
            hudItems.Add(fpsText);

            VRCPlayerApi localPlayer = Networking.LocalPlayer;
            if (localPlayer != null)
            {
                Vector3 currentPosition = localPlayer.gameObject.transform.position;
                string positionText = $"Position: ({currentPosition.x:F2}, {currentPosition.y:F2}, {currentPosition.z:F2})";
                string speedText = $"Speed: {playerSpeed:F2} m/s";
                hudItems.Add(positionText);
                hudItems.Add(speedText);

                string welcome = $"Welcome, {localPlayer.displayName}!";
                float textWidth = RenderHelper.GetTextSize(welcome, fontSize).x;
                RenderHelper.RenderText(new Vector2((Screen.width - textWidth) / 2f, 10f), welcome, fontSize, Color.green, true);
            }

            Vector2 startPos = new Vector2 (10, Screen.height - fontSize - 10);
            for (int i = 0; i < hudItems.Count; ++i)
            {
                var item = hudItems[i];
                RenderHelper.RenderText(new Vector2(startPos.x, startPos.y - i * fontSize), item, fontSize, Color.white, true, false);
            }
        }
    }
}
