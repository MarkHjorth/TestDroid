using System;
using System.Diagnostics;
using System.IO;

namespace TestDroidClient
{
	/// <summary>
	/// Flightmode class, which is used to change flightmode on the phone.
	/// </summary>
	public class Flightmode
	{
		/// <summary>
		/// Handles the flightmode arguments.
		/// </summary>
		/// <returns><c>true</c>, if flightmode was handled, <c>false</c> otherwise.</returns>
		/// <param name="args">Argument to turn flightmode (on = 1, off = 0) or no argument to toggle.</param>
		public bool HandleFlightmode(string[] args)
		{
			bool didSucceed = false;
			int status;

			// Test argument size
			switch (args.Length)
			{
				case 1:
					// If no arguments has been passed along with the flighmode command, we want to toggle flightmode.
					// We use conditional operator to do this:
					status = (GetFlightModeStatus() == 1) ? status = 0 : status = 1;
					didSucceed = ChangeFlightmode(status);
					break;
				case 2:
					// If there is one argument, do the following:
					try
					{
						// Parse argument string to status int (1 = on, 0 = off)
						status = Int32.Parse(args[1]);
						didSucceed = ChangeFlightmode(status);
					}
					catch
					{
						// if argument is not 1 or 0, check if it is "help"
						if (args[1].ToLower() == "help")
						{
							Console.WriteLine("Flightmode usage: flightmode [<1 | 0>]");
						}
						else
						{
							// argument is not a number or the word help. Give error:
							string parseError = "Error: argument is not a number. " +
								"Use flightmode help for more information.";
							Console.WriteLine(parseError);
							didSucceed = false;
						}
					}
					break;
				default:
					// If there is more than one argument, return an errormessage:
					string argumentError = string.Format("Error: Too many arguments ({0}). Exspected 1 or less. " +
					                                     "Use flightmode help for more information.", (args.Length - 1));
					Console.WriteLine(argumentError);
					didSucceed = false;
					break;
			}

			return didSucceed;
		}

		/// <summary>
		/// Changes the flightmode status.
		/// </summary>
		/// <returns><c>true</c>, if flightmode was changed, <c>false</c> otherwise.</returns>
		/// <param name="status">The status to change flightmode to, as an int.</param>
		private bool ChangeFlightmode(int status)
		{
			bool didSucceed = false;
			string stringSucceed;
			string stringStatus;
			string output;
			ADBhandler adb = new ADBhandler();

			// Converting 1/0 to on/off and print to the user what we are doing.
			stringStatus = (status == 1) ? "on" : "off";
			Console.WriteLine("Turning flightmode " + stringStatus + "...");

			// Create the ADB arguments for turning on airplane mode, and broadcasting to the phone, that we did.
			string flightmode = string.Format("shell settings put global airplane_mode_on {0}", status);
			string broadcast = "shell am broadcast -a android.intent.action.AIRPLANE_MODE";

			// First change flightmode and then tell phone that we changed it.
			adb.startProcess(flightmode);
			output = adb.startProcess(broadcast);


			// Take output from the last command, and split at '=' (output will end with result=0 if it worked)
			var outputArray = output.Split('=');
			output = outputArray[outputArray.Length - 1];
			didSucceed = output.Contains("0");

			// Tell the user if the flightmode change succeded
			stringSucceed = (didSucceed) ? "succeded" : "failed";
			Console.WriteLine("Flightmode change " + stringSucceed + ".");

			return didSucceed;
		}

		/// <summary>
		/// Gets the flight mode status.
		/// </summary>
		/// <returns>The flight mode status (1=on, 0=off).</returns>
		private int GetFlightModeStatus()
		{
			int status = 0;
			string stringStatus;
			string output;
			ADBhandler adb = new ADBhandler();

			// Tell the user that we are checking the flightmode.
			Console.WriteLine("Checking flightmode status...");

			// Set ADB arguments to check flightmode status and execute it:
			string checkAirplanemode = "shell settings get global airplane_mode_on";
			output = adb.startProcess(checkAirplanemode);

			// Parse the string output to an int (1/0)
			try
			{
				status = Int32.Parse(output);
			}
			catch (Exception ex)
			{
				// Somehow the output is not an int, write the exception.
				Console.WriteLine(ex.Message);
				Console.WriteLine("Error: " + output + " is not an int");
			}

			// Conditional operator changes the status to on/off and tells the user.
			stringStatus = (status == 1) ? "on" : "off";
			Console.WriteLine("Flightmode status = " + stringStatus);

			return status;
		}
	}
}
