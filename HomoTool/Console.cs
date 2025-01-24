using HomoTool.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using HomoTool.Extensions;
using System.IO;

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

		public void Log(string message, LogLevel level = LogLevel.Info)
		{
			string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            string colorHex = level switch
            {
                LogLevel.Info => "#FFFFFF",      // White
                LogLevel.Warning => "#FFFF00",   // Yellow
                LogLevel.Error => "#FF0000",     // Red
                LogLevel.Debug => "#00FFFF",     // Cyan
                LogLevel.Success => "#00FF00",   // Green
                _ => "#FFFFFF"                   // Default to white
            };

            string styledPrefix = $"<b><color={colorHex}>[{level.ToString().ToUpper()}]</color></b>";

            string styledMessage = $"{styledPrefix} <color={colorHex}>[{timestamp}] {message}</color>";
			string plainMessage = $"[{timestamp}] [{level.ToString().ToUpper()}] {message}";

			_consoleText.Add(styledMessage);
			_scrollPosition.y = Mathf.Infinity;

			string logFilePath = Path.Combine("HomoTool", "Log.txt");
			File.AppendAllText(logFilePath, plainMessage + Environment.NewLine);
		}
	}
}