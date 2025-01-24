using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using HomoTool.Managers;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace HomoTool
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BasePlugin
    {
        internal static new ManualLogSource Log;

        public override void Load()
        {
            Log = base.Log;

            AddComponent<MainMonoBehaviour>();

            CoroutineManager.Instance = AddComponent<CoroutineManager>();

            ModuleManager.Instance.AddModule(new Module.Modules.Flight());
            ModuleManager.Instance.AddModule(new Module.Modules.Watermark());
            ModuleManager.Instance.AddModule(new Module.Modules.Speed());
            ModuleManager.Instance.AddModule(new Module.Modules.ArrayList());
            ModuleManager.Instance.AddModule(new Module.Modules.ESP());
            ModuleManager.Instance.AddModule(new Module.Modules.Menu());
            ModuleManager.Instance.AddModule(new Module.Modules.HUD());
            ModuleManager.Instance.AddModule(new Module.Modules.PickupDropper());
            ModuleManager.Instance.AddModule(new Module.Modules.AirJump());
            ModuleManager.Instance.AddModule(new Module.Modules.Notification());

            Harmony harmony = new Harmony("HomoTool");

            harmony.PatchAll(typeof(Patches.VRC_Pickup.Awake));
            harmony.PatchAll(typeof(Patches.LoadBalancingClient.OpRaiseEvent));
            harmony.PatchAll(typeof(Patches.OnPlayerJoined));
            harmony.PatchAll(typeof(Patches.OnPlayerLeft));
        }
    }

    public class MainMonoBehaviour : MonoBehaviour
    {
        public MainMonoBehaviour(IntPtr handle) : base(handle) { }

        private void Update()
        {
            ModuleManager.Instance.OnUpdate();

            NotificationManager.Instance.OnUpdate();
        }

        private void OnGUI()
        {
            ModuleManager.Instance.OnGUI();

            Console.Instance.OnGUI();

            NotificationManager.Instance.OnGUI();
        }
    }
}
