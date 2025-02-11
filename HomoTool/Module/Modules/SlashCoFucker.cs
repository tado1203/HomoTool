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

            foreach (var pickup in GameObject.FindObjectsOfType<VRC_Pickup>())
            {
                if (pickup.InteractionText == "ガソリン缶" || pickup.InteractionText == "バッテリー")
                {
                    if (Networking.GetOwner(pickup.gameObject) != Networking.LocalPlayer)
                        Networking.SetOwner(Networking.LocalPlayer, pickup.gameObject);

                    pickup.gameObject.transform.position = Networking.LocalPlayer.gameObject.transform.position + new Vector3(0, 1f, 0);
                }
            }
        }
    } 
}