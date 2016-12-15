using System;
using System.Diagnostics;
using System.IO;

namespace TestDroidClient
{
	/// <summary>
	/// ADB handler, which finds the users ADB directory and can be used to call ADB commands.
	/// </summary>
	public class ADBhandler
	{
		public static string path;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:TestDroidClient.ADBhandler"/> class.
		/// </summary>
		public ADBhandler()
		{
			// Use Windows path as default.
			path = "C:/Program Files (x86)/Android/android-sdk/platform-tools/adb.exe";
			// Test if the Windows path excists, else it should use mac path:
			if (!File.Exists(path))
			{
				path = "/Users/mark/Library/Developer/Xamarin/android-sdk-macosx/platform-tools/adb";
			}
		}

		/// <summary>
		/// Sends an ADB command with the given arguments, and returns output as string.
		/// </summary>
		/// <param name="arguments">ADB commandline arguments.</param>
		public string startProcess(string arguments)
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo(path, arguments);
			Process process = new Process();
			StreamReader streamOutput;
			string output;
			string failedOutput = "Failed...";

			// Redirect output from execution
			processStartInfo.RedirectStandardOutput = true;
			processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			processStartInfo.UseShellExecute = false;

			process = Process.Start(processStartInfo);
			streamOutput = process.StandardOutput;
			process.WaitForExit(5000);
			if (process.HasExited)
			{
				output = streamOutput.ReadToEnd();
			}
			else
			{
				output = failedOutput;
			}

			return output;
		}
	}
}
