using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomoTool.Managers;
using UnityEngine;
using VRC.SDKBase;

namespace HomoTool.Module.Modules
{
    public class SilentWalk : ModuleBase
    {
        public SilentWalk() : base("SilentWalk", false, true, KeyCode.None) { }
    }
}