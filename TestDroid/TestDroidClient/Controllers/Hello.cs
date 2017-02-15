using System;
using System.Threading;
using System.Threading.Tasks;

namespace TestDroidClient
{
	public class Hello
	{

		ADBhandler adb = new ADBhandler();

		public async Task<bool> HandleHello(int id, string[] args)
		{
			bool didSucceed = false;

			switch (args.Length)
			{
				// If no arguments are sent, we guess the user wants to list all devices
				case 1:
					{
						didSucceed = HelloList();
						break;
					}
				// If one or two arguments are sent, we want to check what the first argument is:
				case 2:
				case 3:
					{
						didSucceed = await HandleHelloAction(id, args);
						break;
					}
				// If an unexspected amount of arguments were send, give an error:
				default:
					{
						// First find out if there were no more arguments than the actual command (few) or too many (many):
						string tooFewOrTooManyArgs = (args.Length == 1) ? "few" : "many";

						// Create and print the error:
						string argumentError = string.Format("Error: Too {0} arguments ({1}). Exspected 1 argument. " +
															 "Use hello help for more information.",
															 tooFewOrTooManyArgs, (args.Length - 1));
						Console.WriteLine(argumentError);
						didSucceed = false;
						break;
					}
			}

			return didSucceed;
		}

		private async Task<bool> HandleHelloAction(int id, string[] args)
		{
			bool didSucceed = false;

			switch (args[1])
			{
				// If argument is help, we should answer with usage information
				case "help":
					{
						Console.WriteLine("Hello usage: hello <phone | list>");
						didSucceed = false;
						break;
					}
				// If argument is phone, we want to ping that phone
				case "phone":
					{
						didSucceed = await HelloPhone(id, args);
						break;
					}
				// If argument is list, we want to list all the phones
				case "list":
					{
						didSucceed = HelloList();
						break;
					}
				// If the argument was not recognized, give an errormessage:
				default:
					{
						string errorMessage = string.Format("Unknown argument: {0}. " +
															"Use hello help for more information", args[1]);
						Console.WriteLine(errorMessage);
						didSucceed = false;
						break;
					}
			}

			return didSucceed;
		}

		private async Task<bool> HelloPhone(int id, string[] args)
		{
			bool didSucceed = false;

			Console.WriteLine("Saying hello to phone...");

			TCPConnection tcp = TCPConnection.GetInstance();

			didSucceed = await tcp.SendCommand(id, args);


			return didSucceed;
		}

		private bool HelloList()
		{
			bool didSucceed = false;
			string output;
			string listPhonesCommand;

			listPhonesCommand = "devices -l";

			adb.StartProcess(listPhonesCommand, false, 2000);

			output = adb.StartProcess(listPhonesCommand);

			Console.WriteLine(output);

			didSucceed = true;

			return didSucceed;
		}

	}
}
