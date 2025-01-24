using HomoTool.Helpers;
using HomoTool.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HomoTool.Module.Modules
{
    public class ArrayList : ModuleBase
    {
        public ArrayList() : base("ArrayList", true, false, KeyCode.None) { }

        private readonly Vector2 padding = new Vector2(10, 10);
        private readonly int fontSize = 24;

        public override void OnGUI()
        {
            var modules = ModuleManager.Instance.GetModules();
            var visibleModules = modules.FindAll(module => module.Visible);

            visibleModules.Sort((module1, module2) =>
            {
                float width1 = RenderHelper.GetTextSize(module1.ModuleName, fontSize).x;
                float width2 = RenderHelper.GetTextSize(module2.ModuleName, fontSize).x;
                return width2.CompareTo(width1);
            });

            int index = 0;
            foreach (var module in visibleModules)
            {
                Vector2 textSize = RenderHelper.GetTextSize(module.ModuleName, fontSize);

                ref Vector2 currentPosition = ref module.ArrayListCurrentPosition;
                currentPosition.x = MathHelper.ExpLerp(currentPosition.x, 
                    module.Enabled ? Screen.width - textSize.x - padding.x : Screen.width, 10f, Time.deltaTime);
                currentPosition.y = MathHelper.ExpLerp(currentPosition.y, 
                    padding.y + (fontSize * index), 10f, Time.deltaTime);

                bool shouldRenderText = module.Enabled || (!module.Enabled && currentPosition.x <= Screen.width);
                if (shouldRenderText)
                {
                    RenderHelper.RenderText(currentPosition, module.ModuleName, fontSize, Color.white, true);
                }

                if (module.Enabled)
                    index++;
            }
        }
    }
}