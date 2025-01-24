using HomoTool.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HomoTool.Module.Modules
{
    public class Menu : ModuleBase
    {
        public Menu() : base("Menu", false, false, KeyCode.Insert) { }

        private Vector2 menuPosition = new Vector2(10, 100);

        public override void OnGUI()
        {
            if (Enabled)
            {
                Rect windowRect = new Rect(menuPosition.x, menuPosition.y, 400, 300);
                windowRect = GUILayout.Window(0, windowRect, (GUI.WindowFunction)DrawMenu, "HomoTool");
            }
        }

        private void DrawMenu(int windowID)
        {
            GUILayout.BeginVertical();

            foreach (var module in ModuleManager.Instance.GetModules())
            {
                if (!module.Visible)
                    continue;

                GUILayout.BeginHorizontal();

                GUILayout.Label($"<b>{module.ModuleName}</b>");

                bool isEnabled = GUILayout.Toggle(module.Enabled, "Enabled");
                if (isEnabled != module.Enabled)
                {
                    module.Toggle();
                }

                GUILayout.EndHorizontal();

                module.OnMenu();

                GUILayout.Space(10);
            }

            GUILayout.EndVertical();
        }
    }
}
