using HomoTool.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HomoTool.Module.Modules
{
    public class Watermark : ModuleBase
    {
        public Watermark() : base("Watermark", true, false, KeyCode.None) { }

        public override void OnGUI()
        {
            RenderHelper.RenderText(new Vector2(10, 10), "HomoTool", 50, Color.white, true, true);
        }
    }
}