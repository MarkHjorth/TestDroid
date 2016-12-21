using System;
namespace TestDroidClient
{
	public class Controller
	{
		TCPConnection tcpConnection;
		private Flightmode flightmode;
		private Call call;
        private ApkInstaller apk;

		public Controller(string[] args)
		{
            try
            {
				flightmode = new Flightmode();
				call = new Call();
                apk = new ApkInstaller();

                if (args.Length >= 1)
                {
                    string command = string.Join(" ", args);
                    ParseCommand(command);
                }
            }
            catch (Exception e)
            {
				Console.WriteLine(e.StackTrace);
            }
		}

		public void ParseCommand(string fullCommand)
		{
			string[] args = fullCommand.Split(' ');
			string command = args[0];

			switch (command)
			{
				case "sendSMS": //Other cases here
                    tcpConnection = TCPConnection.GetInstance();
                    tcpConnection.SendCommand(fullCommand);
					break;
				case "flightmode":
					flightmode.HandleFlightmode(args);
                    break;
				case "call":
					call.HandleCall(args);
					break;
                case "installApk":
                    apk.InstallApk();
				    break;
				default:
					Console.WriteLine("Command not found!");
					break;
			}
		}
	}
}
