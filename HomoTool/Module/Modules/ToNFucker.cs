using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomoTool.Managers;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace HomoTool.Module.Modules
{
    public class ToNFucker : ModuleBase
    {
        private GameObject killplayer;
        private Coroutine coroutine;

        public ToNFucker() : base("ToNFucker", false, true) { }

        public override void OnEnable()
        {
            if (Networking.LocalPlayer == null)
                return;

            killplayer = GameObject.Find("killplayer");
            coroutine = CoroutineManager.Instance.StartManagedCoroutine(fucker());
        }

        public override void OnDisable()
        {
            CoroutineManager.Instance.StopManagedCoroutine(coroutine);
        }

        IEnumerator fucker()
        {
            while (true)
            {
                killplayer.GetComponent<UdonBehaviour>().RunProgram("TellKillersToFindNewTargets");
                yield return new WaitForSecondsRealtime(0.5f);
            }
        }
    }
}