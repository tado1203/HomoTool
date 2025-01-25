using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using HomoTool.Managers;
using HomoTool.Module;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HomoTool
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BasePlugin
    {
        internal static new ManualLogSource Log;

        public override void Load()
        {
            Log = base.Log;

            AddCoreComponents();
            RegisterModules();
            PatchMethods();
        }

        private void AddCoreComponents()
        {
            AddComponent<MainMonoBehaviour>();
            CoroutineManager.Instance = AddComponent<CoroutineManager>();
        }

        private void RegisterModules()
        {
            var modules = new List<ModuleBase>
            {
                new Module.Modules.Flight(),
                new Module.Modules.Watermark(),
                new Module.Modules.Speed(),
                new Module.Modules.ArrayList(),
                new Module.Modules.ESP(),
                new Module.Modules.Menu(),
                new Module.Modules.HUD(),
                new Module.Modules.PickupDropper(),
                new Module.Modules.AirJump(),
                new Module.Modules.Notification(),
                new Module.Modules.PlayerList(),
            };

            foreach (var module in modules)
            {
                ModuleManager.Instance.AddModule(module);
            }
        }

        private void PatchMethods()
        {
            var harmony = new Harmony("HomoTool");

            harmony.PatchAll(typeof(Patches.VRC_Pickup.Awake));
            harmony.PatchAll(typeof(Patches.LoadBalancingClient.OpRaiseEvent));
            harmony.PatchAll(typeof(Patches.OnPlayerJoined));
            harmony.PatchAll(typeof(Patches.OnPlayerLeft));
            harmony.PatchAll(typeof(Patches.UdonSync.UdonSyncRunProgramAsRPC));
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
