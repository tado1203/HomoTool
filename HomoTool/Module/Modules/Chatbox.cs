using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomoTool.Helpers;
using HomoTool.Managers;
using Il2CppSystem.Security.Cryptography;
using UnityEngine;
using VRC.SDKBase;

namespace HomoTool.Module.Modules
{
    public class Chatbox : ModuleBase
    {
        Coroutine c = null;

        public Chatbox() : base("Chatbox", false, true) { }

        public override void OnEnable()
        {
            if (Networking.LocalPlayer == null)
                return;

            c = CoroutineManager.Instance.StartManagedCoroutine(enumerator());
        }

        public override void OnDisable()
        {
            CoroutineManager.Instance.StopManagedCoroutine(c);
        }

        private IEnumerator enumerator()
        {
            while (true)
            {
                PhotonHelper.SendChat("");
                yield return new WaitForSecondsRealtime(0.5f);
            }
        }
    }
}