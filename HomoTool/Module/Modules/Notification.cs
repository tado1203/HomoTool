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
    public class Notification : ModuleBase
    {
        public Notification() : base("Notification", true, false) { }

        public override void OnPlayerJoined(GamePlayer player)
        {
            Log(player, true);
        }

        public override void OnPlayerLeft(GamePlayer player)
        {
            Log(player, false);
        }

        private void Log(GamePlayer player, bool joined)
        {
            VRCPlayerApi playerApi = player.prop_VRCPlayerApi_0;
            string playerName = playerApi.isLocal ? "You" : playerApi.displayName;
            string text = joined ? $"{playerName} joined the instance." : $"{playerName} left the instance.";

            Console.Instance.Log(text, Color.white);

            NotificationManager.Instance.AddNotification(text);
        }
    }
}