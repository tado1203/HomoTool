using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomoTool.Helpers;
using UnityEngine;
using VRC.SDKBase;

namespace HomoTool.Module.Modules
{
    public class PlayerCounter : ModuleBase
    {
        private const float MaxDistance = 400f;

        public PlayerCounter() : base("PlayerCounter", false, true) { }

        public override void OnGUI()
        {
            if (Networking.LocalPlayer == null || !Enabled)
            return;

            VRCPlayerApi localPlayer = Networking.LocalPlayer;

            int filteredCount = CountNearbyPlayers(localPlayer);

            string message = GetPlayerCountMessage(filteredCount);
            RenderHelper.RenderText(new Vector2(300, 10), message, 40, Color.white, true, true);
        }

        private int CountNearbyPlayers(VRCPlayerApi localPlayer)
        {
            int count = 0;

            foreach (var player in VRCPlayerApi.AllPlayers)
            {
                if (Vector3.Distance(localPlayer.gameObject.transform.position, player.gameObject.transform.position) < MaxDistance)
                    count++;
            }

            return count;
        }

        private string GetPlayerCountMessage(int filteredCount)
        {
            int totalPlayers = VRCPlayerApi.AllPlayers.Count;

            if (filteredCount == totalPlayers)
                return "All players are alive";

            return $"{filteredCount} / {totalPlayers} players are alive";
        }
    }
}