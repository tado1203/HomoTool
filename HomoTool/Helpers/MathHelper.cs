using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HomoTool.Helpers
{
    public static class MathHelper
    {
        public static float ExpLerp(float currentValue, float targetValue, float speed, float deltaTime)
        {
            return Mathf.Lerp(currentValue, targetValue, 1 - Mathf.Exp(-speed * deltaTime));
        }

        public static Vector2 ExpLerp(Vector2 currentValue, Vector2 targetValue, float speed, float deltaTime)
        {
            return new Vector2(
                Mathf.Lerp(currentValue.x, targetValue.x, 1 - Mathf.Exp(-speed * deltaTime)),
                Mathf.Lerp(currentValue.y, targetValue.y, 1 - Mathf.Exp(-speed * deltaTime))
            );
        }
    }
}