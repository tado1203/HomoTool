using BepInEx.Unity.IL2CPP.Utils.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HomoTool.Managers
{
    public sealed class CoroutineManager : MonoBehaviour
    {
        public static CoroutineManager Instance;

        public Coroutine StartManagedCoroutine(IEnumerator coroutine)
        {
            return StartCoroutine(coroutine.WrapToIl2Cpp());
        }

        public void StopManagedCoroutine(Coroutine coroutine)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
        }
    }
}
