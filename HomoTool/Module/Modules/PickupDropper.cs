using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Il2CppSystem.Security.Cryptography;
using UnityEngine;
using VRC.SDKBase;

namespace HomoTool.Module.Modules
{
    public class PickupDropper : ModuleBase
    {
        private float dropDelay = 0.5f;
        private Coroutine coroutine = null;
        private List<VRC_Pickup> cachedPickups = new List<VRC_Pickup>();

        public PickupDropper() : base("Pickup", false, true, KeyCode.None) { }

        public override void OnMenu()
        {
            GUILayout.Label($"Drop Delay: {dropDelay}");
            dropDelay = GUILayout.HorizontalSlider(dropDelay, 0f, 1f);
        }

        public override void OnEnable()
        {
            RefreshPickups();
            coroutine = Managers.CoroutineManager.Instance.StartManagedCoroutine(Dropper());
        }

        public override void OnDisable()
        {
            Managers.CoroutineManager.Instance.StopManagedCoroutine(coroutine);
            cachedPickups.Clear();
        }

        private IEnumerator Dropper() 
        {
            while (true)
            {
                Drop();
                yield return new WaitForSecondsRealtime(dropDelay);
            }
        }

        private void Drop()
        {
            VRCPlayerApi localPlayer = Networking.LocalPlayer;
            if (localPlayer == null)
                return;

            foreach (var pickup in cachedPickups)
            {
                if (pickup == null)
                    continue;
                Networking.SetOwner(localPlayer, pickup.gameObject);
                pickup.Drop();
            }
        }

        private void RefreshPickups()
        {
            cachedPickups.Clear();
            cachedPickups.AddRange(UnityEngine.Object.FindObjectsOfType<VRC_Pickup>());
        }
    }
}