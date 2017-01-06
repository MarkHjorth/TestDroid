using System;

namespace TestDroidClient
{
	/// <summary>
	/// Call class, which is used to answer a call or make a new call.
	/// </summary>
	public class Call
	{

		/// <summary>
		/// Handles the call command.
		/// </summary>
		/// <returns><c>true</c>, if call succeded, <c>false</c> otherwise.</returns>
		/// <param name="args">Arguments to handle call. Answer for answering call, dial to make call, or help for help.</param>
		public bool HandleCall(string[] args, int id)
		{
			bool didSucceed = false;


			switch (args.Length)
			{
				// If one argument is sent:
				case 2:
                case 3:
					{
						didSucceed = HandleCallAction(args, id);
						break;
					}
				// If an unexspected amount of arguments were send, give an error:
				default:
					{
						// First find out if there were no more arguments than the actual command (few) or too many (many):
						string tooFewOrTooManyArgs = (args.Length == 1) ? "few" : "many";

						// Create and print the error:
						string argumentError = string.Format("Error: Too {0} arguments ({1}). Exspected 1. " +
															 "Use call help for more information.",
															 tooFewOrTooManyArgs, (args.Length - 1));
						Console.WriteLine(argumentError);
						didSucceed = false;
						break;
					}
			}

			return didSucceed;
		}


		/// <summary>
		/// Handles which function in the call command to excecute.
		/// </summary>
		/// <returns><c>true</c>, if call action was handled, <c>false</c> otherwise.</returns>
		/// <param name="args">Checks to see what to do. answer, dial or display help.</param>
		private bool HandleCallAction(string[] args, int id)
		{
			bool didSucceed = false;

			switch (args[1])
			{
				// If argument is help, we should answer with usage information
				case "help":
					{
						Console.WriteLine("Call usage: call [answer | dial]");
						didSucceed = false;
						break;
					}
				// If argument is answer, we want to answer a phone call
				case "answer":
					{
						didSucceed = CallAnswer();
						break;
					}
				// If argument is dial, we want to make a phone call
				case "dial":
					{
						didSucceed = CallDial(args, id);
						break;
					}
				// If the argument was not recognized, give an errormessage:
				default:
					{
						string errorMessage = string.Format("Unknown argument: {0}. " +
						                                    "Use call help for more information", args[1]);
						Console.WriteLine(errorMessage);
						didSucceed = false;
						break;
					}
			}

			return didSucceed;
		}

		/// <summary>
		/// Method to answer an incomming call
		/// </summary>
		/// <returns><c>true</c>, if call was answered, <c>false</c> otherwise.</returns>
		private bool CallAnswer()
		{
			bool didSucceed = false;
			string output;
			string answerPhoneCallCommand;
			string getPhoneCallStatusCommand;
			string stringSucceed;
			ADBhandler adb = new ADBhandler();


			answerPhoneCallCommand = "-d shell service call phone 6";
			getPhoneCallStatusCommand = "-d shell dumpsys telephony.registry | grep \"mCallState\"";

			Console.WriteLine("Answering call...");
			adb.StartProcess(answerPhoneCallCommand, false, 2500);

			Console.WriteLine("Checking if call was answered...");
			output = adb.StartProcess(getPhoneCallStatusCommand);

			// When a call is active, the getPhoneCallStatusCommand will return: "mCallState=2"
			didSucceed = output.Contains("2");

			stringSucceed = (didSucceed) ? "succeded" : "failed";

			Console.WriteLine("Call answer " + stringSucceed + ".");

			return didSucceed;
		}

		/// <summary>
		/// Method to make a new call
		/// </summary>
		/// <returns><c>true</c>, if call was successfull, <c>false</c> otherwise.</returns>
		/// <param name="args">Arguments.</param>

		private bool CallDial(string[] args, int id)
		{
			bool didSucceed = false;
			string fullCommand = (id + " " + string.Join(" ", args));

			TCPConnection connection = TCPConnection.GetInstance();

			didSucceed = connection.SendCommand(id, fullCommand);

			return didSucceed;
		}
	}
}
