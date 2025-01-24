using HomoTool.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace HomoTool.Helpers
{
    public static class RenderHelper
    {
        public static void RenderText(Vector2 pos, string text, int fontSize, Color color, bool shadow = false, bool bold = false)
        {
            string colorHex = color.ToHex(true);

            string boldTagStart = bold ? "<b>" : "";
            string boldTagEnd = bold ? "</b>" : "";

            string formattedText = $"{boldTagStart}<color={colorHex}><size={fontSize}>{text}</size></color>{boldTagEnd}";

            if (shadow)
            {
                string shadowFormattedText = $"{boldTagStart}<color=black><size={fontSize}>{text}</size></color>{boldTagEnd}";
                float shadowOffset = 2f;

                GUI.Label(new Rect(pos.x, pos.y - shadowOffset, 1000f, 1000f), shadowFormattedText);
                GUI.Label(new Rect(pos.x, pos.y + shadowOffset, 1000f, 1000f), shadowFormattedText);
                GUI.Label(new Rect(pos.x - shadowOffset, pos.y, 1000f, 1000f), shadowFormattedText);
                GUI.Label(new Rect(pos.x + shadowOffset, pos.y, 1000f, 1000f), shadowFormattedText);
            }

            GUI.Label(new Rect(pos.x, pos.y, 1000f, 1000f), formattedText);
        }

        public static Vector2 GetTextSize(string text, int fontSize, bool bold = false)
        {
            string boldTagStart = bold ? "<b>" : "";
            string boldTagEnd = bold ? "</b>" : "";

            string formattedText = $"{boldTagStart}<size={fontSize}>{text}</size>{boldTagEnd}";

            GUIContent content = new GUIContent(formattedText);

            GUIStyle style = GUI.skin.box;

            float width = style.CalcSize(content).x;
            float height = style.CalcHeight(content, width);

            return new Vector2(width, height);
        }
    }
}