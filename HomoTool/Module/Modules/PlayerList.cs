using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.SDKBase;
using HomoTool.Extensions;

namespace HomoTool.Module.Modules
{
    public class PlayerList : ModuleBase
    {
        private Vector2 listSize = new Vector2(400, 600);

        public PlayerList() : base("PlayerList", true, false) { }

        public override void OnGUI()
        {
            if (Networking.LocalPlayer == null)
                return;

            GUILayout.BeginArea(new Rect(Screen.width - listSize.x - 10, 10, listSize.x, listSize.y), GUI.skin.box);

            GUILayout.Label($"{VRCPlayerApi.AllPlayers.Count} players online");

            foreach (var player in VRCPlayerApi.AllPlayers)
            {
                PlayerNet_Internal playerNet = player.GetPlayer()._playerNet;
                int fps = playerNet.GetFramerate();
                int ping = playerNet.GetPing();

                Color fpsColor = Color.Lerp(Color.red, Color.green, Mathf.Clamp01((float)fps / 100f));
                string fpsText = $"<color={fpsColor.ToHex()}>[{fps} FPS]</color>";

                Color pingColor = Color.Lerp(Color.green, Color.red, Mathf.Clamp01((float)ping / 150f));
                string pingText = $"<color={pingColor.ToHex()}>[{ping} ms]</color>";

                string masterText = player.isMaster ? "<color=yellow>Master</color> - " : "";

                string displayName = player.displayName;
                Color playerColor = player.GetPlayer().prop_APIUser_0.GetPlayerColor();
                string nameText = $"<color={playerColor.ToHex()}>{displayName}</color>";

                string playerInfo = $"{masterText}{fpsText} - {pingText} - {nameText}";

                GUILayout.Label(playerInfo, GUILayout.Height(0));
            }

            GUILayout.EndArea();
        }
    }
}
