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
		private Vector2 consoleSize = new Vector2(600, 200);

		private const int MaxLogCount = 60;

		private Console() { }

		public void OnGUI()
		{
			GUILayout.BeginArea(new Rect(Screen.width - consoleSize.x - 10, Screen.height - consoleSize.y - 10, consoleSize.x, consoleSize.y), GUI.skin.box);

			_scrollPosition = GUILayout.BeginScrollView(_scrollPosition, GUILayout.Width(consoleSize.x), GUILayout.Height(consoleSize.y - 50));

			foreach (var message in _consoleText)
				GUILayout.Label(message);

			GUILayout.EndScrollView();

			if (GUILayout.Button("Clear"))
				_consoleText.Clear();

			GUILayout.EndArea();
		}

		public void Log(string message, LogLevel level = LogLevel.Info)
		{
			string timestamp = DateTime.Now.ToString("HH:mm:ss.fff");

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

			if (_consoleText.Count > MaxLogCount)
			{
				_consoleText.RemoveAt(0);
			}	

			_scrollPosition.y = Mathf.Infinity;

			string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "HomoTool");
			Directory.CreateDirectory(directoryPath);

			string logFilePath = Path.Combine(directoryPath, "Log.txt");
			File.AppendAllText(logFilePath, plainMessage + Environment.NewLine);
		}
	}
}