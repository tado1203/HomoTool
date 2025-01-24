using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomoTool.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HomoTool.Module
{
    public abstract class ModuleBase
    {
        public string ModuleName { get; private set; }
        public bool Enabled { get; private set; }
        public bool Visible { get; private set; }
        public KeyCode KeyBind { get; private set; }
        public Vector2 ArrayListCurrentPosition = new Vector2(Screen.width, 0);

        protected ModuleBase(string moduleName, bool enabled, bool visible, KeyCode key = KeyCode.None)
        {
            ModuleName = moduleName;
            Enabled = enabled;
            Visible = visible;
            KeyBind = key;
        }

        public virtual void OnUpdate() { }
        public virtual void OnGUI() { }
        public virtual void OnPlayerJoined(GamePlayer player) { }
        public virtual void OnPlayerLeft(GamePlayer player) { }
        public virtual void OnMenu() { }
        public virtual void OnEnable() { }
        public virtual void OnDisable() { }

        public void Toggle()
        {
            Enabled = !Enabled;
            if (Enabled)
            {
                OnEnable();

                string message = $"Enabled {ModuleName}";
                Plugin.Log.LogMessage(message);
                NotificationManager.Instance.AddNotification(message);
            }
            else
            {
                OnDisable();

                string message = $"Disabled {ModuleName}";
                Plugin.Log.LogMessage(message);
                NotificationManager.Instance.AddNotification(message);
            }
        }
    }
}
