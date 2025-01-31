using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomoTool.Managers;
using UnityEngine;
using UnityEngine.AI;
using VRC.SDKBase;
using VRC.Udon;

namespace HomoTool.Module.Modules
{
    public class ToNFucker : ModuleBase
    {
        private List<NavMeshAgent> navMeshAgents = new List<NavMeshAgent>();

        public ToNFucker() : base("ToNFucker", false, true) { }

        public override void OnUpdate()
        {
            if (Networking.LocalPlayer == null || !Enabled)
                return;

            foreach (var shit in navMeshAgents)
            {
                if (!shit.gameObject.activeSelf)
                    continue;
                
                shit.destination = Vector3.zero;
            }
        }

        public override void OnEnable()
        {
            if (Networking.LocalPlayer == null)
                return;

            navMeshAgents.Clear();

            foreach (var monster in Resources.FindObjectsOfTypeAll<NavMeshAgent>())
			{
				navMeshAgents.Add(monster);
			}
        }

        public override void OnDisable()
        {
            navMeshAgents.Clear();
        }
    }
}