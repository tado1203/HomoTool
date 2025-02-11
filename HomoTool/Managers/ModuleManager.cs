using HomoTool.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HomoTool.Managers
{
    public sealed class ModuleManager
    {
        private static readonly Lazy<ModuleManager> _instance = new Lazy<ModuleManager>(() => new ModuleManager());

        private readonly List<ModuleBase> _modules = new List<ModuleBase>();

        public static ModuleManager Instance => _instance.Value;

        private ModuleManager() { }

        public void AddModule(ModuleBase module)
        {
            if (module != null && !_modules.Contains(module))
            {
                _modules.Add(module);
            }
        }

        public List<ModuleBase> GetModules()
        {
            return new List<ModuleBase>(_modules);
        }

        public ModuleBase GetModule(string moduleName)
        {
            return _modules.FirstOrDefault(m => m.ModuleName == moduleName);
        }

        public void OnUpdate()
        {
            foreach (var module in _modules)
            {
                module.OnUpdate();

                if (Input.GetKeyDown(module.KeyBind))
                    module.Toggle();
            }
        }

        public void OnFixedUpdate()
        {
            foreach (var module in _modules)
            {
                module.OnFixedUpdate();
            }
        }

        public void OnGUI()
        {
            foreach (var module in _modules)
                module.OnGUI();
        }

        public void OnPlayerJoined(Player_Internal player)
        {
            foreach (var module in _modules)
                module.OnPlayerJoined(player);
        }

        public void OnPlayerLeft(Player_Internal player)
        {
            foreach (var module in _modules)
                module.OnPlayerLeft(player);
        }
    }
}
