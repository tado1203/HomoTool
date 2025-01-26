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

	public class LogEntry
	{
		public string StyledMessage { get; set; }
		public string LatestTimestamp { get; set; }
		public int Count { get; set; }

		public LogEntry(string styledMessage, string latestTimestamp, int count)
		{
			StyledMessage = styledMessage;
			LatestTimestamp = latestTimestamp;
			Count = count;
		}
	}

	public class Console
	{
		private static readonly Lazy<Console> _instance = new Lazy<Console>(() => new Console());
		public static Console Instance => _instance.Value;

		private Dictionary<string, LogEntry> _logMessages = new();
    	private List<string> _consoleOrder = new();
		private Vector2 _scrollPosition;
		private Vector2 consoleSize = new Vector2(600, 200);

		private const int MaxLogCount = 60;
		private bool _autoScroll = true;

		private Console() { }

		public void OnGUI()
		{
			GUILayout.BeginArea(new Rect(Screen.width - consoleSize.x - 10, Screen.height - consoleSize.y - 10, consoleSize.x, consoleSize.y), GUI.skin.box);

			_autoScroll = GUILayout.Toggle(_autoScroll, "Auto-Scroll");

			_scrollPosition = GUILayout.BeginScrollView(_scrollPosition, GUILayout.Width(consoleSize.x), GUILayout.Height(consoleSize.y - 50));

			foreach (var key in _consoleOrder)
			{
				var logEntry = _logMessages[key];

				string displayMessage = logEntry.Count > 1 ? $"{logEntry.StyledMessage} <b>(x{logEntry.Count})</b>" : logEntry.StyledMessage;

				GUILayout.Label(displayMessage);
			}

			GUILayout.EndScrollView();

			if (GUILayout.Button("Clear"))
			{
				_logMessages.Clear();
				_consoleOrder.Clear();

				string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "HomoTool");
				string logFilePath = Path.Combine(directoryPath, "Log.txt");

				if (File.Exists(logFilePath))
				{
					File.WriteAllText(logFilePath, string.Empty);
				}
			}

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

			if (_logMessages.ContainsKey(message))
			{
				// increment the count for duplicate messages
				var existingEntry = _logMessages[message];
            	_logMessages[message] = new LogEntry(existingEntry.StyledMessage, timestamp, existingEntry.Count + 1);
			}
			else
			{
				// add new log
				if (_logMessages.Count >= MaxLogCount)
				{
					string oldestMessage = _consoleOrder[0];
					_logMessages.Remove(oldestMessage);
					_consoleOrder.RemoveAt(0);
				}

				_logMessages[message] = new LogEntry(styledMessage, timestamp, 1);
				_consoleOrder.Add(message);
			}

			if (_autoScroll)
				_scrollPosition.y = Mathf.Infinity;

			string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "HomoTool");
			Directory.CreateDirectory(directoryPath);

			string logFilePath = Path.Combine(directoryPath, "Log.txt");
			File.AppendAllText(logFilePath, plainMessage + Environment.NewLine);
		}
	}
}