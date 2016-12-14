using System;
namespace TestDroidClient
{
	public class Controller
	{
		TCPConnection tcpConnection;
		private Flightmode flightmode;

		public Controller()
		{
            try
            {
                tcpConnection = new TCPConnection();
				flightmode = new Flightmode();
            }
            catch (Exception)
            {
                Console.WriteLine("ADB not found!");
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
