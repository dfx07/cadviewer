using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadViewer.Common
{
	public static class Logger
	{
		public enum LogLevel
		{
			None,       // No logging
			DebugOnly,  // Log to Debug output only
			All         // Log to both Debug output and file
		}

		// Current log level
		public static LogLevel CurrentLogLevel { get; set; } = LogLevel.DebugOnly;

		private static readonly string logFilePath = "log.txt";

		// Log message to both Debug output and a file
		private static void Log(string message)
		{
			if (CurrentLogLevel == LogLevel.None)
			{
				return; // Do not log anything
			}

			string timestampedMessage = $"{DateTime.Now}: {message}";

			// Log to Visual Studio's Output window
			Debug.WriteLine(timestampedMessage);

			if (CurrentLogLevel == LogLevel.All)
			{
				// Log to a file
				try
				{
					using (StreamWriter writer = new StreamWriter(logFilePath, true))
					{
						writer.WriteLine(timestampedMessage);
					}
				}
				catch (Exception ex)
				{
					Debug.WriteLine($"Failed to write to log file: {ex.Message}");
				}
			}
		}

		// Log with different severity levels
		public static void LogInfo(string message) => Log($"INFO: {message}");
		public static void LogWarning(string message) => Log($"WARNING: {message}");
		public static void LogError(string message) => Log($"ERROR: {message}");
	}
}
