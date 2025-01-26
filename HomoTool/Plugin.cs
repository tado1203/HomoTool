using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using HomoTool.Managers;
using HomoTool.Module;
using System;
using System.Collections.Generic;
using UnityEngine;
using HomoTool.Module.Modules;

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
                new Flight(),
                new Watermark(),
                new Speed(),
                new ArrayList(),
                new ESP(),
                new Menu(),
                new HUD(),
                new PickupDropper(),
                new AirJump(),
                new Notification(),
                new PlayerList(),
                new NoMovementPacket(),
                new ToNFucker(),
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
            harmony.PatchAll(typeof(Patches.UdonBehaviour.RunProgram));
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
