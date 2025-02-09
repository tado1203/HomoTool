using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VRC.SDKBase;

namespace HomoTool.Module.Modules
{
    public class SlashCoFucker : ModuleBase
    {
        public SlashCoFucker() : base("SlashCoFucker", false, true) { }

        public override void OnEnable()
        {
            if (Networking.LocalPlayer == null)
                return;

            List<GameObject> generators = new List<GameObject>();
            List<VRC_Pickup> fuels = new List<VRC_Pickup>();
            List<VRC_Pickup> batteries = new List<VRC_Pickup>();

            foreach (var obj in GameObject.FindObjectsOfType<GameObject>())
			{
				if (obj.name.Contains("SC_generator"))
				{
					generators.Add(obj);
				}
			}

            foreach (var pickup in GameObject.FindObjectsOfType<VRC_Pickup>())
            {
                if (pickup.InteractionText == "ガソリン缶")
                    fuels.Add(pickup);
                
                if (pickup.InteractionText == "バッテリー")
                    batteries.Add(pickup);
            }

            int generatorCount = generators.Count;
            int fuelCount = fuels.Count;
            int batteryCount = batteries.Count;

            for (int i = 0; i < generatorCount; i++)
            {
                int fuelToAssign = Mathf.Min(4, fuelCount - i * 4);
                for (int j = 0; j < fuelToAssign; j++)
                {
                    fuels[i * 4 + j].transform.position = generators[i].transform.position + new Vector3(0, 2f, 0);
                }

                if (i < batteryCount)
                {
                    batteries[i].transform.position = generators[i].transform.position  + new Vector3(0, 2f, 0);
                }
            }
        }
    } 
}