using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HomoTool.Extensions
{
    public static class ColorExtensions
    {
        public static string ToHex(this Color color, bool includeAlpha = true)
        {
            int r = Mathf.RoundToInt(color.r * 255);
            int g = Mathf.RoundToInt(color.g * 255);
            int b = Mathf.RoundToInt(color.b * 255);
            int a = Mathf.RoundToInt(color.a * 255);

            if (includeAlpha)
            {
                return $"#{r:X2}{g:X2}{b:X2}{a:X2}";
            }
            else
            {
                return $"#{r:X2}{g:X2}{b:X2}";
            }
        }
    }
}
