using HomoTool.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using HomoTool.Extensions;

namespace HomoTool
{
    public enum LogLevel
    {
        Info,
        Warning,
        Error,
        Debug,
        Success
    }

    public class Console
    {
        private static readonly Lazy<Console> _instance = new Lazy<Console>(() => new Console());

        public static Console Instance => _instance.Value;

        private List<string> _consoleText = new List<string>();
        private Vector2 _scrollPosition;

        private Console() { }

        public void OnGUI()
        {
            GUILayout.BeginArea(new Rect(Screen.width - 510, Screen.height - 310, 500, 300), GUI.skin.box);

            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, GUILayout.Width(500), GUILayout.Height(250));

            foreach (var message in _consoleText)
                GUILayout.Label(message);

            GUILayout.EndScrollView();

            if (GUILayout.Button("Clear"))
                _consoleText.Clear();
            GUILayout.EndArea();
        }

        public void Log(string message, Color color, LogLevel level = LogLevel.Info)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            string colorHex = color.ToHex();

            string styledPrefix = level switch
            {
                LogLevel.Info => "<b>[INFO]</b>",
                LogLevel.Warning => "<b><color=FFFF00>[WARNING]</color></b>",
                LogLevel.Error => "<b><color=FF0000>[ERROR]</color></b>",
                LogLevel.Debug => "<b><color=00FFFF>[DEBUG]</color></b>",
                LogLevel.Success => "<b><color=#00FF00>[SUCCESS]</color></b>",
                _ => "<b>[INFO]</b>"
            };

            string styledMessage = $"{styledPrefix} <color={colorHex}>[{timestamp}] {message}</color>";

            _consoleText.Add(styledMessage);

            _scrollPosition.y = Mathf.Infinity;
        }
    }
}