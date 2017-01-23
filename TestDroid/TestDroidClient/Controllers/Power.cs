using System;
using System.Threading.Tasks;

namespace TestDroidClient
{
	public class Power
	{

		ADBhandler adb = new ADBhandler();

		public async Task<bool> HandlePower(string[] args, int id)
		{
			bool didSucceed = false;

			switch (args.Length)
			{
				// If one argument is sent:
				case 2:
					{
						HandlePowerAction(args, id);
						break;
					}
				// If an unexspected amount of arguments were send, give an error:
				default:
					{
						// First find out if there were no more arguments than the actual command (few) or too many (many):
						string tooFewOrTooManyArgs = (args.Length == 1) ? "few" : "many";

						// Create and print the error:
						string argumentError = string.Format("Error: Too {0} arguments ({1}). Exspected 1. " +
															 "Use power help for more information.",
															 tooFewOrTooManyArgs, (args.Length - 1));
						Console.WriteLine(argumentError);
						didSucceed = false;
						break;
					}
			}

			return didSucceed;
		}

		private async Task<bool> HandlePowerAction(string[] args, int id)
		{
			bool didSucceed = false;

			switch (args[1])
			{
				// If argument is help, we should answer with usage information
				case "help":
					{
						Console.WriteLine("Power usage: power <reboot | off>");
						didSucceed = false;
						break;
					}
				// If argument is off, we want to turn off phone.
				case "off":
					{
						didSucceed = PowerOff();
						break;
					}
				// If argument is reboot, we want to reboot the phone
				case "reboot":
					{
						didSucceed = PowerReboot();
						break;
					}
				// If the argument was not recognized, give an errormessage:
				default:
					{
						string errorMessage = string.Format("Unknown argument: {0}. " +
															"Use power help for more information", args[1]);
						Console.WriteLine(errorMessage);
						didSucceed = false;
						break;
					}
			}
			return didSucceed;
		}

		private bool PowerReboot()
		{
			bool didSucceed = false;
			string rebootCommand;
			string stringSucceed;

			rebootCommand = "-d reboot";

			Console.WriteLine("Rebooting phone...");
			adb.StartProcess(rebootCommand);

			//TODO: Check if we actually rebooted

			return didSucceed;
		}

		private bool PowerOff()
		{
			bool didSucceed = false;
			string offCommand;
			string stringSucceed;

			offCommand = "-d shell reboot -p";

			Console.WriteLine("Turning off phone...");
			adb.StartProcess(offCommand);

			//TODO: Check if phone is actually turned off

			return didSucceed;
		}
	}
}
