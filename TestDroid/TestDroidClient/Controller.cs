using System;
namespace TestDroidClient
{
	public class Controller
	{
		TCPConnection tcpConnection;
		private Flightmode flightmode;

		public Controller(string[] args)
		{
            try
            {
                tcpConnection = new TCPConnection();
				flightmode = new Flightmode();

                while(!tcpConnection.GotIO)
                { }

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
				case "sendSMS":
					if (tcpConnection.SendCommand(fullCommand))
					{
						Console.WriteLine("Prolihøhar");
					}
					break;
				case "flightmode":
					flightmode.HandleFlightmode(args);
				break;
				default:
					Console.WriteLine("Command not found!");
					break;
			}
		}
	}
}
